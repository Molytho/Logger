﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Molytho.Logger
{
    public partial class Logger<T> : IDisposable
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
    }
}
