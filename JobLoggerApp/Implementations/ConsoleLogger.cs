namespace JobLoggerApp.Implementations
{
    using System;
    using System.Linq;
    using Helpers;
    using Interfaces;
    public interface IConsoleLogger : ILogBase { }

    public class ConsoleLogger : IConsoleLogger
    {
        private readonly ILevelLogger _levelLogger;

        public ConsoleLogger(ILevelLogger levelLogger)
        {
            _levelLogger = levelLogger;
        }

        public void LogMessage(MessageType messageType, string message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;

           var allowedLevels = _levelLogger.GetAllowedLevels();

            if(allowedLevels.Contains(messageType))
                Console.WriteLine($"Date: {DateTime.Now}, MessageType: {messageType}, Message: {message}");
            else
                throw new MessageTypeNotAllowedException($"Message type: {messageType} is not allowed, please enable it in the settings file.");
        }
    }
}
