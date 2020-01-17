using System;
using System.Collections.Generic;
using System.Text;

namespace Molytho.Logger
{
    public partial class Logger<T> where T : System.Enum
    {
        #region LogLevel
        public T DebugLogLevel { get; }
        #endregion

        private void WriteLogMessage(LogMessage<T> message)
        {
            fileHandler.WriteLogMessage(message);
            RaiseEvent(message);
        }
        private void WriteDebugLogMessage(string message)
        {
            LogMessage<T> logMessage = new LogMessage<T>(DebugLogLevel, message, ElapsedTime);
            WriteLogMessage(logMessage);
        }
        public void WriteLogMessage(T logLevel, string message)
        {
            if(logLevel.Equals(DebugLogLevel))
            {
                WriteDebugLogMessage(message);
                return;
            }
            LogMessage<T> logMessage = new LogMessage<T>(logLevel, message, ElapsedTime);
            WriteLogMessage(logMessage);
        }
    }
}
