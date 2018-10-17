namespace JobLoggerApp.Interfaces
{
    public interface ILevelLoggerSettings
    {
        bool IsMessageTypeAllowed { get; }
        bool IsWarningTypeAllowed { get; }
        bool IsErrorTypeAllowed { get; }
    }
}
