using System.Web;

namespace KNet.TinyTimer.Web
{

    public class TinyTimerTraceContextHttpModuleStarter
    {        
        public static void Start()
        {
            HttpApplication.RegisterModule(typeof(TinyTimerTraceHttpContext));
        }
    }
}
