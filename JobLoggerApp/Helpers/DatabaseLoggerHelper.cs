namespace JobLoggerApp.Helpers
{
    using System;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    public interface IDatabaseLoggerHelper
    {
        void SaveLog(MessageType messageType, string message);
    }

    public class DatabaseLoggerHelper : IDatabaseLoggerHelper
    {
        public void SaveLog(MessageType messageType, string message)
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
    }
}
