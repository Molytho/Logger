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
        public T[] HiddenTypes { get; }

        [DebuggerHidden]
        public void WriteLogMessage(T logLevel, string message)
        {
            if(!(HiddenTypes is null) && HiddenTypes.Contains(logLevel))
                return;

            LogMessage<T> logMessage = new LogMessage<T>(logLevel, message, ElapsedTime);
            fileHandler.WriteLogMessage(logMessage);
            RaiseEvent(logMessage);
        }
        [DebuggerHidden]
        public async Task WriteLogMessageAsync(T logLevel, string message)
        {
            if(!(HiddenTypes is null) && HiddenTypes.Contains(logLevel))
                return;

            LogMessage<T> logMessage = new LogMessage<T>(logLevel, message, ElapsedTime);
            await fileHandler.WriteLogMessageAsync(logMessage);
            RaiseEvent(logMessage);
        }
    }
}
