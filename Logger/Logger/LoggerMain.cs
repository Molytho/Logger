using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Molytho.Logger
{
    public partial class Logger<T>
        where T : Enum
    {
        public T MinLogLevel { get; set; }

        [DebuggerHidden]
        public void WriteLogMessage(T logLevel, string message, object formatProviderData = null)
        {
            if(logLevel.CompareTo(MinLogLevel) < 0)
                return;

            LogMessage<T> logMessage = new LogMessage<T>(logLevel, message, ElapsedTime);
            fileHandler.WriteLogMessage(logMessage, formatProviderData);
            RaiseEvent(logMessage);
        }
        [DebuggerHidden]
        public async Task WriteLogMessageAsync(T logLevel, string message, object formatProviderData = null)
        {
            if(logLevel.CompareTo(MinLogLevel) < 0)
                return;

            LogMessage<T> logMessage = new LogMessage<T>(logLevel, message, ElapsedTime);
            await fileHandler.WriteLogMessageAsync(logMessage, formatProviderData);
            RaiseEvent(logMessage);
        }
    }
}
