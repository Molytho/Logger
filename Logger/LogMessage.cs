﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Molytho.Logger
{
    public struct LogMessage<T> where T : System.Enum
    {
        public LogMessage(T type, string message, DateTime time)
        {
            Type = type;
            Message = message;
            Time = time;
        }
        public T Type { get; }
        public string Message { get; }
        public DateTime Time { get; }

        public override string ToString()
        {
            return String.Format("[{0}][{1}] {2}", Type.GetName(), Time.ToShortTimeString(), Message);
        }
    }
}
