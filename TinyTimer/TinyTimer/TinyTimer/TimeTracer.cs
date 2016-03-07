using System;
using System.Collections.Generic;

namespace KNet.TinyTimer
{
    public class TimeTracer
    {
        static TimeTracer()
        {
            Listeners = new List<ITinyTimerTraceListener>();
            IsEnabled = true;
        }
        
        public static IList<ITinyTimerTraceListener> Listeners { get; private set; }
        public static bool IsEnabled { get; set; }
    

        public static TinyTimeTracerWatch Watch(string label, params object[] values)
        {
            var timeTracer = TinyTimerTraceContext.GetCurrent();

            var watch = timeTracer.CreateWatch(label, values);
            return watch;
        }

        private TinyTimeTracerWatch CreateWatch(string label, object[] values)
        {
            return new TinyTimeTracerWatch(AddTraceInfo, label, values);
        }

        private void AddTraceInfo(TinyTimerTraceInfo info)//TODO
        {
            foreach (var listner in Listeners)
            {
                try
                {
                    listner.Take(info);
                }
                catch (Exception)
                {
                    //TODO:..
                    throw;
                }
            }
        }

        public static void Flush()
        {
            foreach (var listener in Listeners)
            {
                listener.Flush();
            }
        }
    }
}
