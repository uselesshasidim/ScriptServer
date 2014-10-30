using System;
using System.Collections.Generic;
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

            const string fileDirectory = @"C:\app\ScriptCSHost\CSX Files\";

            FileManager fileManager = new FileManager(fileDirectory);
     
            Console.Read();
           

        }

  
    }
}
