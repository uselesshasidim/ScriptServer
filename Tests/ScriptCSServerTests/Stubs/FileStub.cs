using ScriptCSHost;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCSHostTests.Stubs
{
    public class FileStub : CSXFile
    {
        public bool FileExecuted { get; set; }
        public int ExecutionCount { get; set; }

       
        public FileStub(string fullPath)
            : base(fullPath)
        {
            ExecutionCount = 0;
        }

        public FileStub(ITimer timer, IFileSystem fileSystem, string fullPath)
            : base(timer, fileSystem, fullPath)
        {
            ExecutionCount = 0;
        }
        public override System.Diagnostics.Process Execute()
        {
            var p = base.Execute();
            FileExecuted = true;
            ExecutionCount += 1;
            return p;
        }
    }
}
