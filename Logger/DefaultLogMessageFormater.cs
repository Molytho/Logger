using System;

namespace Molytho.Logger
{
    public class DefaultLogMessageFormater<T> : ILogMessageFormater<T>
        where T : Enum
    {
        private static DefaultLogMessageFormater<T> instance = null;
        public static DefaultLogMessageFormater<T> Instance => instance ??= new DefaultLogMessageFormater<T>();
        private DefaultLogMessageFormater() { }

        private static readonly int MAX_NAME_SIZE = EnumExtensions.GetMaxNameLength<T>();
        private static readonly string FORMAT_STRING = $"[{{0,-{MAX_NAME_SIZE}}}][{{1}}] {{2}}";

        public string FormateLogMessage(LogMessage<T> logMessage, object formatProviderData)
        {
            T level = logMessage.Type;
            string message = logMessage.Message;
            TimeSpan timestamp = logMessage.Time;

            return string.Format(FORMAT_STRING, level.GetName(), timestamp.ToString(), message);
        }
    }
}
