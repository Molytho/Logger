using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Molytho.Logger
{
    class LogfileHandler : IDisposable
    {
        public LogfileHandler(string path, bool toSTDOUT)
        {
            bToSTDOUT = toSTDOUT;
            if(!string.IsNullOrEmpty(path))
            {
                File = new FileInfo(path);
                FileStream = !File.Exists ? File.Create() : File.Open(FileMode.Create);
                StreamWriter = new StreamWriter(FileStream);
            }
            else if(!toSTDOUT)
                throw new ArgumentException("One log way needs to be chosen");
        }

        private FileInfo File { get; }
        private FileStream FileStream { get; }
        private StreamWriter StreamWriter { get; }

        private readonly bool bToSTDOUT;

        public void WriteLogMessage(LogMessage message)
        {
            StreamWriter?.WriteLine(message);
            if(bToSTDOUT)
                Console.Out.WriteLine(message);
        }

        #region IDisposable Support
        private bool disposedValue = false; // Dient zur Erkennung redundanter Aufrufe.

        protected virtual void Dispose(bool disposing)
        {
            if(!disposedValue)
            {
                if(disposing)
                {
                    StreamWriter.Dispose();
                    FileStream.Dispose();
                }

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
