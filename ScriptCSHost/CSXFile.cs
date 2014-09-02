using System;
using System.Collections.Generic;
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
        public string FullPath { get; set; }
        public Schedule Schedule { get; set; }
        public Timer Timer { get; set; }

        public CSXFile(IFileSystem fileSystem, string fullPath)
        {
            _FileSystem = fileSystem;
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

        }
        public CSXFile(string fullPath):
            this(new FileSystem(), fullPath)
        {
            
        }

    }
}
