using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
            var files = Directory.GetFiles(fileDirectory, "*.csx");
            FileManager.ExecuteFiles(files);

           
            Console.WriteLine("Done all files");
            Console.Read();

        }

  
    }
}
