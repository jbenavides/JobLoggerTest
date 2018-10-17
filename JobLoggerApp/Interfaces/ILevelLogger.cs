using System.Collections.Generic;
using JobLoggerApp.Helpers;

namespace JobLoggerApp.Interfaces
{
    public interface ILevelLogger
    {
        IEnumerable<MessageType> GetAllowedLevels();
    }
}
