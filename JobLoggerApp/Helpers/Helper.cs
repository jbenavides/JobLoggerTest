namespace JobLoggerApp.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Helper
    {
        public static List<int> GetMessageTypesEnumValues()
        {
            return Enum.GetValues(typeof(MessageType)).Cast<int>().ToList();
        }
    }
}
