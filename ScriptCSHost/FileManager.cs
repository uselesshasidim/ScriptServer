using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCSHost
{
    public class FileManager
    {
        internal static void ExecuteFiles(string[] files)
        {
            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i];
                ExecuteFile(file);
            }
        }
        internal static void ExecuteFile(string fileToExecute)
        {
            Console.WriteLine("Executing " + fileToExecute);
            string output = string.Empty;
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/c scriptcs \"" + fileToExecute + "\"";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.Start();
            output = p.StandardOutput.ReadToEnd();


            Console.Write(output);

            Console.WriteLine("Done executing " + fileToExecute);
        }
    }
}
