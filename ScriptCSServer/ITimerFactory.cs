using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCSHost
{
    public interface ITimerFactory
    {
        ITimer GetTimer();
    }
}
