using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Molytho.Logger
{
    public partial class Logger<T> : IDisposable, IAsyncDisposable
        where T : Enum
    {
        private bool disposedValue = false; // Dient zur Erkennung redundanter Aufrufe.

        protected virtual void Dispose(bool disposing)
        {
            if(!disposedValue)
            {
                if(disposing)
                {
                    fileHandler.Dispose();
                }

                disposedValue = true;
            }
        }
        [DebuggerHidden]
        public void Dispose()
        {
            Dispose(true);
        }
        [DebuggerHidden]
        public ValueTask DisposeAsync()
        {
            disposedValue = true;
            return ((IAsyncDisposable)fileHandler).DisposeAsync();
        }
    }
}
