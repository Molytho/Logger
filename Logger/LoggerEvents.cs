using System;
using System.Collections.Generic;
using System.Text;

namespace Molytho.Logger
{
    public partial class Logger
    {
        #region LevelEvents
        private readonly Dictionary<string, Action<LogMessage>> logEvents;

        public void AddEvent(object enumType, Action<LogMessage> action)
        {
            logEvents[Enum.GetName(eLogLevel.GetType(), enumType)] += action;
        }
        public void RemoveEvent(object enumType, Action<LogMessage> action)
        {
            logEvents[Enum.GetName(eLogLevel.GetType(), enumType)] -= action;
        }
        #endregion
        #region Global
        public event Action<LogMessage> LogMessageAdded;
        #endregion

        private void RaiseEvent(LogMessage message)
        {
            logEvents[message.Type]?.Invoke(message);

            LogMessageAdded?.Invoke(message);
        }
    }
}
