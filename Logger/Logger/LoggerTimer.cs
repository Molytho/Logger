using System;
using System.Collections.Generic;
using System.Text;

namespace Molytho.Logger
{
    public partial class Logger<T>
    {
        private readonly DateTime startTime;
        private DateTime Now => DateTime.Now;
        private TimeSpan ElapsedTime
        {
            get
            {
                TimeSpan ret = Now - startTime;
                return ret;
            }
        }
    }
}
