using System;
using System.Collections.Generic;
using System.Text;

namespace Molytho.Logger
{
    public readonly struct LogMessage<T>
        where T : Enum
    {
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
            => ToString(DefaultLogMessageFormater<T>.Instance);
        public readonly string ToString(ILogMessageFormater<T> formater)
            => formater.FormateLogMessage(this);
    }
}
