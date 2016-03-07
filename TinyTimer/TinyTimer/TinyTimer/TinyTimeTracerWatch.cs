using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace KNet.TinyTimer
{
    public class TinyTimeTracerWatch : IDisposable
    {
        private TinyTimerTraceInfo _tracerInfo;

        private Stopwatch _stopwatch;
        private Action<TinyTimerTraceInfo> _logger;

        public TinyTimeTracerWatch(Action<TinyTimerTraceInfo> logger, string label, params object[] values)
        {
            this._tracerInfo = new TinyTimerTraceInfo
            {
                Label = label,
                Values = values
            };
            
            this._stopwatch = Stopwatch.StartNew();
            this._logger = logger ?? DefaultLogger;
        }

        private static void DefaultLogger(TinyTimerTraceInfo info)
        {
            Debug.WriteLine(info.ToString());
        }

        private void LogTime()
        {
            this._stopwatch.Stop();
            this._tracerInfo.EndedAt = DateTime.Now;
            this._tracerInfo.Elapsed = this._stopwatch.Elapsed;

            this._logger(this._tracerInfo);
        }
        
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls        
        

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    LogTime();
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