using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCSHost
{
    class Program
    {
        static void Main(string[] args)
        {
            FileManager fileManager = new FileManager(ConfigurationManager.AppSettings["WatchDirectory"]);
            Console.Read();
        }
  
    }
}
