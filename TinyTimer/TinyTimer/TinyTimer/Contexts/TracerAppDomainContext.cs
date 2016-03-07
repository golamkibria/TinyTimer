using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KNet.TinyTimer.Contexts
{
    public class TracerAppDomainContext : TinyTimerTraceContextBase
    {
        public TracerAppDomainContext():base()
        {
            AppDomain.CurrentDomain.ProcessExit += (s, e) => UnloadContext();
            //AppDomain.CurrentDomain.DomainUnload += (s,e) => UnloadContext();
        }

        protected override TimeTracer GetCurrentInner()
        {
            if (AppDomain.CurrentDomain.GetData(this.ContextKey) == null)
            {
                AppDomain.CurrentDomain.SetData(this.ContextKey, new TimeTracer());
            }

            return (TimeTracer)AppDomain.CurrentDomain.GetData(this.ContextKey);
        }        
    }
}
