using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCSHostTests
{
    public class DataHelper
    {
        public const string HostDirectory = @"C:\app\ScriptCSHost\CSX Files\";
        public const string MockCSXFile1Path = @"app.csx";
        public const string MockCSXFile1Data = @"//SCHEDULE-TIME-SPAN: 0 5 0
Console.WriteLine(""Ok"");
";
        public const string MockCSXFile2Path = @"app1.csx";
        public const string MockCSXFile2Data = @"//SCHEDULE-TIME-SPAN: 1 5 0
";

        internal static IFileSystem GetFileSystem()
        {
            return new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {HostDirectory + MockCSXFile1Path,new MockFileData(MockCSXFile1Data)},
                {HostDirectory + MockCSXFile2Path,new MockFileData(MockCSXFile2Data)}
            });

        }
    }
}
