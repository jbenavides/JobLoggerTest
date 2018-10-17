namespace JobLoggerApp.Implementations
{
    using System;
    using System.IO;
    using System.Linq;
    using Helpers;
    using Interfaces;

    public interface IFileLogger : ILogBase { }

    public class FileLogger : IFileLogger
    {
        private readonly ILevelLogger _levelLogger;
        private string Path => Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public FileLogger(ILevelLogger levelLogger)
        {
            _levelLogger = levelLogger;
        }

        public void LogMessage(MessageType messageType, string message)
        {
            var allowedLevels = _levelLogger.GetAllowedLevels();

            if (allowedLevels.Contains(messageType))
            {
                var filePath = $"{Path}/{DateTime.Now:yyyyMMdd}.log";
                
                using (var sw = new StreamWriter(filePath, true))
                {
                    sw.WriteLine($"Date: {DateTime.Now}, MessageType: {messageType}, Message: {message}");

                    #region consoleMessage
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"Message was added to the file: {filePath}");
                    #endregion
                }
            }
            else
                throw new MessageTypeNotAllowedException($"Message type: {messageType} is not allowed, please enable it in the settings file.");
        }
    }
}
