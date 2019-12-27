using System;
using System.Collections.Generic;
using System.Text;

namespace Molytho.Logger
{
    public partial class Logger
    {
        #region LogLevel
        private readonly Enum eLogLevel;
        private string[] aLogLevelNames;
        public string[] LogLevelNames
        {
            get => aLogLevelNames ?? GenerateLogLevelNames();
        }
        public string GetLogLevelName(object enumType)
        {
            return Enum.GetName(eLogLevel.GetType(), enumType);
        }
        private string[] GenerateLogLevelNames()
        {
            return aLogLevelNames = Enum.GetNames(eLogLevel.GetType());
        }
        #endregion

        private void WriteLogMessage(LogMessage message)
        {
            fileHandler.WriteLogMessage(message);
            RaiseEvent(message);
        }
        public void WriteLogMessage(object logLevel, string message)
        {
            string logLevelName = Enum.GetName(eLogLevel.GetType(), logLevel);
            LogMessage logMessage = new LogMessage(logLevelName, message, DateTime.Now);
            WriteLogMessage(logMessage);
        }
        [System.Diagnostics.Conditional("DEBUG")]
        public void WriteDebugLogMessage(string message)
        {
            LogMessage logMessage = new LogMessage("DEBUG", message, DateTime.Now);
            WriteLogMessage(logMessage);
        }
    }
}
