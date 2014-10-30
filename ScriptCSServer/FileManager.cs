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

            LoadFiles();


            return;
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


        public void LoadFiles()
        {

            // Get directory files
            var files = _FileSystem.Directory.GetFiles(_HostDirectory, _FileFilter);

            CSXFiles.Clear();
            foreach (var file in files)
            {
                var csxFile = new CSXFile(new SystemTimer(), _FileSystem, file);
                CSXFiles.Add(csxFile);
            }
        }

        void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            var attr = _FileSystem.File.GetAttributes(e.FullPath);
            if (attr.HasFlag(FileAttributes.Directory))
            {
                return;
            }

            var csxFile = CSXFiles.SingleOrDefault(f => f.FullPath == e.FullPath);

            if (csxFile != null)
            {
                csxFile.Execute();
            }
        }
     
    }
}
