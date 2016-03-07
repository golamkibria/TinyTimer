using KNet.TinyTimer.Contexts;
using System;
using System.Web;

namespace KNet.TinyTimer.Web
{
    public class TinyTimerTraceHttpContext : TinyTimerTraceContextBase, IHttpModule
    {
        protected override TimeTracer GetCurrentInner()
        {
            if (HttpContext.Current.Items[this.ContextKey] == null)
            {
                HttpContext.Current.Items[this.ContextKey] = new TimeTracer();
            }

            return (TimeTracer)HttpContext.Current.Items[this.ContextKey];
        }

        public void Init(HttpApplication context)
        {
            context.EndRequest += (s, e) => UnloadContext();
        }
        
        public void Dispose()
        {
        }
    }
}
