using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Molytho.Logger
{
    public partial class Logger<T>
        where T : Enum
    {

        [DebuggerHidden]
        public Logger(bool toSTDOUT = true, ILogMessageFormater<T> messageFormater = null, params string[] logFilePath)
        {
            ILogMessageFormater<T> formater = messageFormater ?? DefaultLogMessageFormater<T>.Instance;
            TextWriter[] logFiles = new TextWriter[logFilePath.Length + (toSTDOUT ? 1 : 0)];

            for(int i = 0; i < logFilePath.Length; i++)
                logFiles[i] = new StreamWriter(logFilePath[i], true);
            if(toSTDOUT)
                logFiles[logFilePath.Length] = Console.Out;

            fileHandler = new LogfileHandler<T>(formater, logFiles);
            startTime = Now;
            T[] enumTypes = (T[])Enum.GetValues(typeof(T));
            logEvents = new Dictionary<T, Action<LogMessage<T>>>(enumTypes.Length);
            foreach(var item in enumTypes)
                logEvents.Add(item, default);
        }

        [DebuggerHidden]
        public Logger(ILogMessageFormater<T> messageFormater = null, params TextWriter[] pWriterStreams)
        {
            ILogMessageFormater<T> formater = messageFormater ?? DefaultLogMessageFormater<T>.Instance;

            fileHandler = new LogfileHandler<T>(formater, pWriterStreams);
            startTime = Now;
            T[] enumTypes = (T[])Enum.GetValues(typeof(T));
            logEvents = new Dictionary<T, Action<LogMessage<T>>>(enumTypes.Length);
            foreach(var item in enumTypes)
                logEvents.Add(item, default);
        }
    }
}
