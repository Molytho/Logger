using System;
using System.Collections.Generic;
using System.Text;

namespace Molytho.Logger
{
    public partial class Logger
    {
        public Logger(Enum logLevel, string logFilePath = "./latest.log", bool toSTDOUT = true)
        {
            eLogLevel = logLevel;
            bToSTDOUT = toSTDOUT;

            logEvents = new Dictionary<string, List<Action<LogMessage>>>(LogLevelNames.Length);
            foreach(var item in LogLevelNames)
                logEvents.Add(item, new List<Action<LogMessage>>());

            fileHandler = new LogfileHandler(logFilePath);
        }
    }
}
