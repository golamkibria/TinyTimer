using System.Diagnostics;

namespace KNet.TinyTimer.Contexts
{
    public abstract class TinyTimerTraceContextBase : ITinyTimerTraceContext
    {
        protected TinyTimerTraceContextBase()
        {
            this.ContextKey = this.GetType().FullName;
        }

        public string ContextKey { get; private set; }

        public TimeTracer GetCurrent()
        {
            return GetCurrentInner();
        }

        protected abstract TimeTracer GetCurrentInner();

        protected void UnloadContext()
        {
            Debug.WriteLine("UnloadContext: "+ this.ContextKey);

            TimeTracer.Flush();
        }
    }
}
