using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCSHost
{
    
    public interface ITimer
    {
        void Start();
        void Stop();
        event EventHandler<TimerTickEventArgs> TimerTicked;


        double Interval { get; set; }
    }

    public class TimerTickEventArgs : EventArgs
    {
        public DateTime TickTime { get; set; }
    }

    
}
