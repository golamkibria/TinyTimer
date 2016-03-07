using System;
using System.Collections.Generic;

namespace KNet.TinyTimer
{
    public interface ITinyTimerTraceListener
    {
        void Take(params TinyTimerTraceInfo[] infoItems);
        void Flush();
    }
}