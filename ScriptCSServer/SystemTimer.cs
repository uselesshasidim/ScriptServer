using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCSHost
{
    public class SystemTimer : ITimer
    {
        public System.Timers.Timer Timer { get; set; }
        public SystemTimer()
        {
            Timer = new System.Timers.Timer();
            Timer.Elapsed += timer_Elapsed;
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (TimerTicked != null)
            {
                TimerTicked(this, new TimerTickEventArgs() { TickTime = e.SignalTime });
            }
        }

        public void Start()
        {
            Timer.Start();
        }

        public void Stop()
        {
            Timer.Stop();
        }

        public event EventHandler<TimerTickEventArgs> TimerTicked;

        public double Interval
        {
            get
            {
                return Timer.Interval;
            }
            set
            {
                Timer.Interval = value;
            }
        }
    }
}
