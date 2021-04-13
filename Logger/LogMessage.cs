using System;
using System.Collections.Generic;
using System.Text;

namespace Molytho.Logger
{
    public record LogMessage<T>(T Type, string Message, TimeSpan Time)
        where T : Enum
    {
        public override string ToString()
            => ToString(DefaultLogMessageFormater<T>.Instance, null);
        public string ToString(ILogMessageFormater<T> formater, object formatProviderData)
            => formater.FormateLogMessage(this, formatProviderData);
    }
}
