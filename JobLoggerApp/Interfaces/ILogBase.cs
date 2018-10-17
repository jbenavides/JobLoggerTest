using JobLoggerApp.Helpers;

namespace JobLoggerApp.Interfaces
{
    public interface ILogBase
    {
        void LogMessage(MessageType messageType, string message);
    }
}
