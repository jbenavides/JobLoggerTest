namespace JobLoggerApp.Implementations
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using Helpers;
    using Interfaces;

    public interface IDatabaseLogger : ILogBase { }

    public class DatabaseLogger : IDatabaseLogger
    {
        private readonly ILevelLogger _levelLogger;

        public DatabaseLogger(ILevelLogger levelLogger)
        {
            _levelLogger = levelLogger;
        }

        public void LogMessage(MessageType messageType, string message)
        {
            var allowedLevels = _levelLogger.GetAllowedLevels();

            if (allowedLevels.Contains(messageType))
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["JobLoggerConnectionString"].ConnectionString))
                {
                    using (var command = new SqlCommand("[dbo].[usp_InsertLog]", connection))
                    {
                        connection.Open();

                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@pMessageType", messageType.ToString()));
                        command.Parameters.Add(new SqlParameter("@pMessage", message));

                        command.ExecuteScalar();

                        connection.Close();

                        #region consoleMessage
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"Message was added to the Database with connection string: {connection.ConnectionString}");
                        #endregion

                    }
                }
            }
            else
                throw new MessageTypeNotAllowedException($"Message type: {messageType} is not allowed, please enable it in the settings file.");

        }
    }
}
