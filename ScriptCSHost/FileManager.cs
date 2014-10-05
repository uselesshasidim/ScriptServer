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

        #region Private Variables

        const string _FileFilter = "*.csx";
        public List<CSXFile> CSXFiles = new List<CSXFile>();
        private string _HostDirectory = string.Empty;
        private IFileSystem _FileSystem;

        #endregion

        #region Factory

        public FileManager(string hostDirectory, IFileSystem fileSystem)
        {
            _FileSystem = fileSystem;
            _HostDirectory = hostDirectory;

            // Parse files in current directory
            ParseFiles();

            // Execute them once
            ExecuteFiles();

            var watcher = new FileSystemWatcher(hostDirectory, _FileFilter);

            watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size;
            watcher.Changed += watcher_Changed;
            watcher.EnableRaisingEvents = true;

        }
        public FileManager(string hostDirectory):
            this(hostDirectory, new FileSystem())
        {

        }

        #endregion


        public void ParseFiles()
        {

            // Get directory files
            var files = _FileSystem.Directory.GetFiles(_HostDirectory, _FileFilter);
            ParseFiles(files);
        }

        public void ParseFiles(string[] files)
        {
            CSXFiles.Clear();
            foreach (var file in files)
            {
                var csxFile = new CSXFile(new SystemTimer(), _FileSystem, file);
                CSXFiles.Add(csxFile);
            }
        }


        public void ExecuteFiles()
        {
            ExecuteFiles(CSXFiles.Select(f => f.FullPath).ToArray<string>());
        }

        public  void ExecuteFiles(string[] files)
        {
            for (var i = 0; i < files.Length; i++)
            {
                var file = files[i];
                ExecuteFile(file);
            }
        }

        public  void ExecuteFile(string fileToExecute)
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
