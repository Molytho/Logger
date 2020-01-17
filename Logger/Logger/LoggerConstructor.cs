using System;
using System.Collections.Generic;
using System.Text;

namespace Molytho.Logger
{
    public partial class Logger<T> where T : System.Enum
    {
        public Logger(T debugEnumType, string logFilePath = "./latest.log", bool toSTDOUT = true)
        {
            DebugLogLevel = debugEnumType;
            startTime = Now;

            T[] enumTypes = (T[])Enum.GetValues(typeof(T));
            logEvents = new Dictionary<T, Action<LogMessage<T>>>(enumTypes.Length);
            foreach(var item in enumTypes)
                logEvents.Add(item, default);

            fileHandler = new LogfileHandler(logFilePath,toSTDOUT);
        }
    }
}
