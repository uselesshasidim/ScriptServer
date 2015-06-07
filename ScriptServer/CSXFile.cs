using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;

namespace ScriptCSHost
{
    public class CSXFile
    {
        private IFileSystem _FileSystem;
        public Schedule Schedule { get; private set; }
        public ITimer Timer { get; private set; }
        public string FullPath { get; private set; }

        public CSXFile(ITimer timer,  IFileSystem fileSystem, string fullPath)
        {
            _FileSystem = fileSystem;
            Timer = timer;
            Schedule = new Schedule();

            FullPath = fullPath;

            // Extract schedule
            var firstLine = _FileSystem.File.OpenText(fullPath).ReadLine();
            var pattern = @"^\/\/SCHEDULE-TIME-SPAN: (\d+) (\d+) (\d+)";

            var match = Regex.Match(firstLine, pattern);

            int hour = int.Parse(match.Groups[1].Value);
            int minute = int.Parse(match.Groups[2].Value);
            int sec = int.Parse(match.Groups[3].Value);
            Schedule.RunEvery = new TimeSpan(hour, minute, sec);

            // setup timer
            Timer.Interval = Schedule.RunEvery.TotalMilliseconds;
            Timer.TimerTicked += Timer_TimerTicked;
            Timer.Start();

        }

        void Timer_TimerTicked(object sender, TimerTickEventArgs e)
        {
            Execute();
        }
        public CSXFile(string fullPath):
            this(new SystemTimer(), new FileSystem(), fullPath)
        {
            
        }

            
        public virtual Process Execute()
        {
            Process p = new Process();
            //p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/c scriptcs \"" + FullPath + "\"";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            return p;
        }
    }
}
