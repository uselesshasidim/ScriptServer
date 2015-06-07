using Moq;
using ScriptCSHost;
using ScriptCSHostTests.Stubs;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCSHostTests.Helpers
{
    public class FileHelper
    {
        public const string HostDirectory = @"C:\app\ScriptCSHost\CSX Files\";

        public const string MockCSXFile1Path = @"app.csx";
        public const string MockCSXFile1Data = @"//SCHEDULE-TIME-SPAN: 0 5 0
Console.WriteLine(""Ok"");
";
        public const string MockCSXFile2Path = @"app1.csx";
        public const string MockCSXFile2Data = @"//SCHEDULE-TIME-SPAN: 1 5 0
";

        public const string MockCSXFile3Path = "app3.csx";
        public const string MockCSXFile3Data = @"//SCHEDULE-TIME-SPAN: 0 0 2
";

        internal static IFileSystem GetMockFileSystem()
        {
            return new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {HostDirectory + MockCSXFile1Path,new MockFileData(MockCSXFile1Data)},
                {HostDirectory + MockCSXFile2Path,new MockFileData(MockCSXFile2Data)},
                {HostDirectory + MockCSXFile3Path,new MockFileData(MockCSXFile3Data)}
            });

        }


        internal static FileStub GetFile_0_0_2()
        {
            return GetFile_0_0_2(new SystemTimer());
        }

        internal static FileStub GetFile_0_0_2(ITimer timer)
        {

            return new FileStub(timer, FileHelper.GetMockFileSystem(), FileHelper.HostDirectory + FileHelper.MockCSXFile3Path);
        }

        internal static FileStub GetFile_0_5_0()
        {
            return GetFile_0_5_0(new Mock<ITimer>());
        }

        internal static FileStub GetFile_0_5_0(Mock<ITimer> mockTimer)
        {
            return new FileStub(mockTimer.Object, FileHelper.GetMockFileSystem(), FileHelper.HostDirectory + FileHelper.MockCSXFile1Path);

        }

        internal static FileStub GetFile_1_5_0()
        {
            Mock<ITimer> timer = new Mock<ITimer>();
            return new FileStub(timer.Object, FileHelper.GetMockFileSystem(), FileHelper.HostDirectory + FileHelper.MockCSXFile2Path);
        }
    }
}
