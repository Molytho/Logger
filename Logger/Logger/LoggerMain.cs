using System;
using System.Collections.Generic;
using System.Text;

namespace Molytho.Logger
{
    public partial class Logger<T> where T : System.Enum
    {
        #region DebugLog
        public T DebugLogLevel { get; }
        public bool IsDebugLogEnabled { get; }
        #endregion

        private void WriteLogMessage(in LogMessage<T> message)
        {
            fileHandler.WriteLogMessage(message);
            RaiseEvent(message);
        }
        public void WriteLogMessage(T logLevel, string message)
        {
            if(!IsDebugLogEnabled && Equals(logLevel, DebugLogLevel))
                return;

            LogMessage<T> logMessage = new LogMessage<T>(logLevel, message, ElapsedTime);
            WriteLogMessage(logMessage);
        }
    }
}
