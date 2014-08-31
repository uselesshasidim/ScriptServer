using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCSHost
{
    public class FileManager
    {

        internal static List<CSXFile> CSXFiles = new List<CSXFile>();
        const string fileFilter = "*.csx";
        private static string _HostDirectory = string.Empty;

        internal static void Initialize(string hostDirectory)
        {
            // set internal variables
            _HostDirectory = hostDirectory;

            // Get directory files
            var files = Directory.GetFiles(hostDirectory, fileFilter);

            // Parse them
            FileManager.ParseFiles(files);

            // Execute them once
            ExecuteFiles();

            var watcher = new FileSystemWatcher(hostDirectory, fileFilter);

            watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size;
            watcher.Changed += watcher_Changed;
            watcher.EnableRaisingEvents = true;
        }

        internal static void ParseFiles(string[] files)
        {
            foreach (var file in files)
            {
                var csxFile = CSXFile.LoadFile(file);
                CSXFiles.Add(csxFile);
            }
        }

        internal static void ExecuteFiles()
        {
            ExecuteFiles(CSXFiles.Select(f => f.FullPath).ToArray<string>());
        }

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

        static void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            var attr = File.GetAttributes(e.FullPath);
            if (attr.HasFlag(FileAttributes.Directory))
            {
                return;
            }
            Console.WriteLine(e.FullPath + " changed");
            FileManager.ExecuteFile(e.FullPath);
        }
     
    }
}
