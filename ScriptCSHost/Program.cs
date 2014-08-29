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
            string[] files = new string[] { @"C:\app\app.csx", @"C:\app\app2.csx" };

            ExecuteFiles(files);

           
            Console.WriteLine("Done all files");
            Console.Read();

        }

        private static void ExecuteFiles(string[] files){
            for (var i=0;i<files.Length;i++)
            {
                var file = files[i];
                ExecuteFile(file);
            }
        }
        private static void ExecuteFile(string fileToExecute)
        {
            Console.WriteLine("Executing " + fileToExecute);
            string output = string.Empty;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = @"/c scriptcs " + fileToExecute;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            output = p.StandardOutput.ReadToEnd();

           
            Console.Write(output);

            Console.WriteLine("Done executing " + fileToExecute);
        }
    }
}
