namespace JobLoggerApp.Helpers
{
    using System.Configuration;

    public class LevelLoggerConfigurationSection : ConfigurationSection
    {
        public static LevelLoggerConfigurationSection Settings { get; } = ConfigurationManager.GetSection("LevelLoggerConfigurationSection") as LevelLoggerConfigurationSection;

        [ConfigurationProperty("isMessageTypeAllowed", DefaultValue = false, IsRequired = true)]
        public bool IsMessageTypeAllowed
        {
            get => (bool)this["isMessageTypeAllowed"];
            set => this["isMessageTypeAllowed"] = value;
        }

        [ConfigurationProperty("isWarningTypeAllowed", DefaultValue = false, IsRequired = true)]
        public bool IsWarningTypeAllowed
        {
            get => (bool)this["isWarningTypeAllowed"];
            set => this["isWarningTypeAllowed"] = value;
        }

        [ConfigurationProperty("isErrorTypeAllowed", DefaultValue = false, IsRequired = true)]
        public bool IsErrorTypeAllowed
        {
            get => (bool)this["isErrorTypeAllowed"];
            set => this["isErrorTypeAllowed"] = value;
        }
    }
}
