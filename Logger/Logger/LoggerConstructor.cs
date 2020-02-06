using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Molytho.Logger
{
    public partial class Logger<T> where T : System.Enum
    {
        [DebuggerHidden]
        public Logger(T debugEnumType, bool debugLogEnabled = true, string logFilePath = "./latest.log", bool toSTDOUT = true) : this(logFilePath, toSTDOUT)
        {
            DebugLogLevel = debugEnumType;
            IsDebugLogEnabled = debugLogEnabled;
        }

        [DebuggerHidden]
        public Logger(string logFilePath = "./latest.log", bool toSTDOUT = true)
        {
            startTime = Now;

            T[] enumTypes = (T[])Enum.GetValues(typeof(T));
            logEvents = new Dictionary<T, Action<LogMessage<T>>>(enumTypes.Length);
            foreach(var item in enumTypes)
                logEvents.Add(item, default);

            fileHandler = new LogfileHandler(logFilePath, toSTDOUT);
        }
    }
}
