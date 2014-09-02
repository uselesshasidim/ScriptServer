using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCSHost
{
    public class FileManager
    {

        const string fileFilter = "*.csx";
        internal List<CSXFile> CSXFiles = new List<CSXFile>();
        private string _HostDirectory = string.Empty;

        private IFileSystem _FileSystem;
        public FileManager(string hostDirectory, IFileSystem fileSystem)
        {
            _FileSystem = fileSystem;
            _HostDirectory = hostDirectory;


            // Get directory files
            var files = _FileSystem.Directory.GetFiles(hostDirectory, fileFilter);

            // Parse them
            ParseFiles(files);

            // Execute them once
            ExecuteFiles();

            var watcher = new FileSystemWatcher(hostDirectory, fileFilter);

            watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size;
            watcher.Changed += watcher_Changed;
            watcher.EnableRaisingEvents = true;

        }
        public FileManager(string hostDirectory):
            this(hostDirectory, new FileSystem())
        {

        }

  
        internal void ParseFiles(string[] files)
        {
            foreach (var file in files)
            {
                var csxFile = new CSXFile(_FileSystem, file);
                CSXFiles.Add(csxFile);
            }
        }

        internal void ExecuteFiles()
        {
            ExecuteFiles(CSXFiles.Select(f => f.FullPath).ToArray<string>());
        }

        internal  void ExecuteFiles(string[] files)
        {
            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i];
                ExecuteFile(file);
            }
        }

        internal  void ExecuteFile(string fileToExecute)
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

        void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            var attr = _FileSystem.File.GetAttributes(e.FullPath);
            if (attr.HasFlag(FileAttributes.Directory))
            {
                return;
            }
            Console.WriteLine(e.FullPath + " changed");
            ExecuteFile(e.FullPath);
        }
     
    }
}
