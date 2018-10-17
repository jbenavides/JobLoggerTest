namespace JobLoggerApp.Implementations
{
    using Helpers;
    using Interfaces;

    public class LevelLoggerSettings : ILevelLoggerSettings
    {
        public bool IsMessageTypeAllowed => LevelLoggerConfigurationSection.Settings.IsMessageTypeAllowed;
        public bool IsWarningTypeAllowed => LevelLoggerConfigurationSection.Settings.IsWarningTypeAllowed;
        public bool IsErrorTypeAllowed => LevelLoggerConfigurationSection.Settings.IsErrorTypeAllowed;
    }
}
