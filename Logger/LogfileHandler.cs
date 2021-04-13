using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Threading;

namespace Molytho.Logger
{
    class LogfileHandler : IDisposable, IAsyncDisposable
    {
        public LogfileHandler(params TextWriter[] pOutputStreams)
        {
            if(pOutputStreams is null || pOutputStreams.Length == 0)
                throw new ArgumentException("A minimum of one output stream must be specified", nameof(pOutputStreams));
            outputStreams = pOutputStreams;
            outputStreamSynchronisation = new SemaphoreSlim(1, 1);
        }
        private readonly TextWriter[] outputStreams;
        private readonly SemaphoreSlim outputStreamSynchronisation;

        public void WriteLogMessage<T>(in LogMessage<T> message, bool shouldFlush = false)
            where T : Enum
        {
            string printMessage = message.ToString();
            try
            {
                outputStreamSynchronisation.Wait();
                foreach(TextWriter item in outputStreams)
                {
                    item.WriteLine(printMessage);
                    if(shouldFlush)
                        item.Flush();
                }
            }
            finally
            {
                outputStreamSynchronisation.Release();
            }
        }
        public async Task WriteLogMessageAsync<T>(LogMessage<T> message, bool shouldFlush = false)
            where T : Enum
        {
            string printMessage = message.ToString();
            try
            {
                await outputStreamSynchronisation.WaitAsync();
                List<Task> writeTasks = new List<Task>(outputStreams.Length);
                foreach(TextWriter item in outputStreams)
                {
                    Task writeTask = item.WriteLineAsync(printMessage);
                    if(shouldFlush)
                        writeTasks.Add(writeTask.ContinueWith(_ => item.Flush()));
                }
                await Task.WhenAll(writeTasks);
            }
            finally
            {
                outputStreamSynchronisation.Release();
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // Dient zur Erkennung redundanter Aufrufe.

        protected virtual void Dispose(bool disposing)
        {
            if(!disposedValue)
            {
                if(disposing)
                {
                    outputStreamSynchronisation.Wait();
                    for(int i = 0; i < outputStreams.Length; i++)
                        outputStreams[i].Dispose();
                    outputStreamSynchronisation.Dispose();
                }

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }

        public ValueTask DisposeAsync()
        {
            if(!disposedValue)
            {
                Task @lock = outputStreamSynchronisation.WaitAsync();
                if(!@lock.IsCompletedSuccessfully)
                    return AwaitLock(@lock);
                for(int i = outputStreams.Length - 1; i > 0; i--)
                {
                    ValueTask result = outputStreams[i].DisposeAsync();
                    if(!result.IsCompletedSuccessfully)
                    {
                        return Await(i, result);
                    }
                    result.GetAwaiter().GetResult();
                }
                outputStreamSynchronisation.Dispose();


                disposedValue = true;
            }
            return default;

            async ValueTask AwaitLock(Task pLock)
            {
                await pLock;
                for(int i = outputStreams.Length - 1; i > 0; i--)
                {
                    await outputStreams[i].DisposeAsync();
                }
                disposedValue = true;
                outputStreamSynchronisation.Dispose();
            }
            async ValueTask Await(int i, ValueTask task)
            {
                await task;
                i--;
                for(; i > 0; i--)
                {
                    await outputStreams[i].DisposeAsync();
                }
                disposedValue = true;
                outputStreamSynchronisation.Dispose();
            }
        }
        #endregion
    }
}
