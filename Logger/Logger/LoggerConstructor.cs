using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Molytho.Logger
{
    public partial class Logger<T> where T : System.Enum
    {
        [DebuggerHidden]
        public Logger(T debugEnumType, bool debugLogEnabled = true, bool toSTDOUT = true, params string[] logFilePath) : this(toSTDOUT, logFilePath)
        {
            DebugLogLevel = debugEnumType;
            IsDebugLogEnabled = debugLogEnabled;
        }

        [DebuggerHidden]
        public Logger(bool toSTDOUT = true, params string[] logFilePath) : this()
        {
            TextWriter[] logFiles = new TextWriter[logFilePath.Length + (toSTDOUT ? 1 : 0)];
            for(int i = 0; i < logFilePath.Length; i++)
            {
                logFiles[i] = new StreamWriter(logFilePath[i], true);
            }

            if(toSTDOUT)
                logFiles[logFilePath.Length] = Console.Out;
            fileHandler = new LogfileHandler(logFiles);
        }
        [DebuggerHidden]
        public Logger(params TextWriter[] pWriterStreams) : this()
        {
            fileHandler = new LogfileHandler(pWriterStreams);
        }

        [DebuggerHidden]
        private Logger()
        {
            startTime = Now;

            T[] enumTypes = (T[])Enum.GetValues(typeof(T));
            logEvents = new Dictionary<T, Action<LogMessage<T>>>(enumTypes.Length);
            foreach(var item in enumTypes)
                logEvents.Add(item, default);
        }
    }
}
