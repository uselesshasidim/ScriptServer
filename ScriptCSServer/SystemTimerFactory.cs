using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptCSHost
{
    public class SystemTimerFactory:ITimerFactory
    {
        public ITimer GetTimer()
        {
            return new SystemTimer();
        }
    }
}
