using System;
using Molytho.Logger;

namespace TestConsole
{
    class Program
    {
        static Logger<LogLevel> Logger = new Logger<LogLevel>(LogLevel.Debug);
        static void Main(string[] args)
        {
            Logger.AddEvent(LogLevel.Debug, x);
            Logger.RemoveEvent(LogLevel.Debug, x);
            Logger.WriteLogMessage(LogLevel.Debug,"test");

            static void x(LogMessage<LogLevel> a) => Console.WriteLine("[EVENT] " + a);
        }
    }
    enum LogLevel
    {
        Information,
        Warning,
        Error,
        Debug,
    }
}
