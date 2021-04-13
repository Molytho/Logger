using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Molytho.Logger
{
    public partial class Logger<T>
        where T : notnull, Enum
    {
        //T is ValueType and notnull so default should never be null
        [DebuggerHidden]
        public Logger(bool toSTDOUT = true, T minLogLevel = default!, ILogMessageFormater<T>? messageFormater = null, params string[] logFilePath)
        {
            ILogMessageFormater<T> formater = messageFormater ?? DefaultLogMessageFormater<T>.Instance;
            TextWriter[] logFiles = new TextWriter[logFilePath.Length + (toSTDOUT ? 1 : 0)];

            for(int i = 0; i < logFilePath.Length; i++)
                logFiles[i] = new StreamWriter(logFilePath[i], true);
            if(toSTDOUT)
                logFiles[logFilePath.Length] = Console.Out;

            fileHandler = new LogfileHandler<T>(formater, logFiles);
            startTime = Now;
            MinLogLevel = minLogLevel;
            T[] enumTypes = (T[])Enum.GetValues(typeof(T));
            logEvents = new Dictionary<T, Action<LogMessage<T>>?>(enumTypes.Length);
            foreach(var item in enumTypes)
                logEvents.Add(item, default);
        }
        //T is ValueType and notnull so default should never be null
        [DebuggerHidden]
        public Logger(T minLogLevel = default!, ILogMessageFormater<T>? messageFormater = null, params TextWriter[] pWriterStreams)
        {
            ILogMessageFormater<T> formater = messageFormater ?? DefaultLogMessageFormater<T>.Instance;

            fileHandler = new LogfileHandler<T>(formater, pWriterStreams);
            startTime = Now;
            MinLogLevel = minLogLevel;
            T[] enumTypes = (T[])Enum.GetValues(typeof(T));
            logEvents = new Dictionary<T, Action<LogMessage<T>>?>(enumTypes.Length);
            fileHandler = new LogfileHandler<T>(formater, pWriterStreams);
            foreach(var item in enumTypes)
                logEvents.Add(item, default);
        }
    }
}
