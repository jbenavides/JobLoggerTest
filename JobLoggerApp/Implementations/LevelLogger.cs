namespace JobLoggerApp.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using Helpers;
    using Interfaces;

    public class LevelLogger : ILevelLogger
    {
        private readonly ILevelLoggerSettings _levelLoggerSettings;

        public LevelLogger(ILevelLoggerSettings levelLoggerSettings)
        {
            _levelLoggerSettings = levelLoggerSettings;
        }

        public IEnumerable<MessageType> GetAllowedLevels()
        {
            var result =  new List<MessageType>();

            if (_levelLoggerSettings.IsMessageTypeAllowed)
                result.Add(MessageType.Message);
            if(_levelLoggerSettings.IsWarningTypeAllowed)
                result.Add(MessageType.Warning);
            if(_levelLoggerSettings.IsErrorTypeAllowed)
                result.Add(MessageType.Error);

            if(!result.Any())
                throw new MessageTypeNullException($"Must enable at least one message type ({MessageType.Message}, {MessageType.Warning}, {MessageType.Error}).");

            return result;
        }
    }
}
