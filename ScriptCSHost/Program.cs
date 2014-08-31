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
            // 
            // Setup watcher on directory 
            // When file changes run it again 
           
            const string fileDirectory = @"C:\app\ScriptCSHost\CSX Files\";
            const string fileFilter = "*.csx";
     
            var files = Directory.GetFiles(fileDirectory, fileFilter);
            FileManager.ExecuteFiles(files);

            var watcher = new FileSystemWatcher(fileDirectory, fileFilter);
            
            watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size;
            watcher.Changed += watcher_Changed;
            watcher.EnableRaisingEvents=true;
            Console.WriteLine("Done all files");
            Console.Read();

            watcher.Dispose();


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
