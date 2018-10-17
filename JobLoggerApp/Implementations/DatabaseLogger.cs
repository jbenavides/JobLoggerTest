namespace JobLoggerApp.Implementations
{
    using System.Linq;
    using Helpers;
    using Interfaces;

    public interface IDatabaseLogger : ILogBase { }

    public class DatabaseLogger : IDatabaseLogger
    {
        private readonly ILevelLogger _levelLogger;
        private readonly IDatabaseLoggerHelper _databaseLoggerHelper;

        public DatabaseLogger(ILevelLogger levelLogger, IDatabaseLoggerHelper databaseLoggerHelper)
        {
            _levelLogger = levelLogger;
            _databaseLoggerHelper = databaseLoggerHelper;
        }

        public void LogMessage(MessageType messageType, string message)
        {
            var allowedLevels = _levelLogger.GetAllowedLevels();

            if (allowedLevels.Contains(messageType))
                _databaseLoggerHelper.SaveLog(messageType, message);
            else
                throw new MessageTypeNotAllowedException($"Message type: {messageType} is not allowed, please enable it in the settings file.");
        }
    }
}
