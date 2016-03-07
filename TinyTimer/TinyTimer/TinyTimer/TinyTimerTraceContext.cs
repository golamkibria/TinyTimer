using KNet.TinyTimer.Contexts;
using System;

namespace KNet.TinyTimer
{
    public interface ITinyTimerTraceContext
    {
        TimeTracer GetCurrent();      
    }

    public class TinyTimerTraceContext
    {
        static TinyTimerTraceContext()
        {
            Context = new TracerAppDomainContext();
        }

        public static ITinyTimerTraceContext Context { get; set; }

        public static TimeTracer GetCurrent()
        {
            return Context.GetCurrent();
        }
    }  
}