﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Molytho.Logger
{
    public partial class Logger<T> where T : System.Enum
    {
        #region LevelEvents
        private readonly Dictionary<T, Action<LogMessage<T>>> logEvents;

        public void AddEvent(T enumType, Action<LogMessage<T>> action)
        {
            logEvents[enumType] += action;
        }
        public void RemoveEvent(T enumType, Action<LogMessage<T>> action)
        {
            logEvents[enumType] -= action;
        }
        #endregion
        #region Global
        public event Action<LogMessage<T>> LogMessageAdded;
        #endregion

        private void RaiseEvent(LogMessage<T> message)
        {
            logEvents[message.Type]?.Invoke(message);

            LogMessageAdded?.Invoke(message);
        }
    }
}
