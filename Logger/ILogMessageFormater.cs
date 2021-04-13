using System;

namespace Molytho.Logger
{
    public interface ILogMessageFormater<T>
        where T : Enum
    {
        public string FormateLogMessage(LogMessage<T> logMessage);
    }
}
