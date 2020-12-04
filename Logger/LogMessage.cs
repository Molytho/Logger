using System;
using System.Collections.Generic;
using System.Text;

namespace Molytho.Logger
{
    public readonly struct LogMessage<T> where T : System.Enum
    {
        private static readonly int MAX_NAME_SIZE = EnumExtensions.GetMaxNameLength<T>();
        private static readonly string FORMAT_STRING = $"[{{0,-{MAX_NAME_SIZE}}}][{{1}}] {{2}}";

        public LogMessage(T type, string message, TimeSpan time)
        {
            Type = type;
            Message = message;
            Time = time;
        }
        public T Type { get; }
        public string Message { get; }
        public TimeSpan Time { get; }

        public readonly override string ToString()
        {
            return string.Format(FORMAT_STRING, Type.GetName(), Time.ToString(), Message);
        }
    }
}
