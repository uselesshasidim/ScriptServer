using NUnit.Framework;
using ScriptCSHost;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScriptCSHostTests{    

    [TestFixture]
    public class CSXFileTest
    {

        #region Setup

        const string MockCSXFile1Path = @"C:\app\ScriptCSHost\CSX Files\app.csx";
        const string MockCSXFile1Data = @"//SCHEDULE-TIME-SPAN: 0 5 0
";
        const string MockCSXFile2Path = @"C:\app\ScriptCSHost\CSX Files\app1.csx";
        const string MockCSXFile2Data = @"//SCHEDULE-TIME-SPAN: 1 5 0
";

        private IFileSystem GetFileSystem()
        {
            return new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {MockCSXFile1Path,new MockFileData(MockCSXFile1Data)},
                {MockCSXFile2Path,new MockFileData(MockCSXFile2Data)}
            });
            
        }
        #endregion

        [Test]
        public void First_Line_With_Value_0_5_0_Returns_5_Minutes_Only()
        {      
            // Mock file system 
            var fileSystem = GetFileSystem();

            CSXFile file = new CSXFile(fileSystem, MockCSXFile1Path);
            Assert.AreEqual(5, file.Schedule.RunEvery.Minutes);
            Assert.AreEqual(0, file.Schedule.RunEvery.Hours);         
        }

        [Test]
        public void First_Line_With_Value_1_5_0_Returns_1_Hour_And_5_Minutes()
        {
            // Mock file system 
            var fileSystem = GetFileSystem();

            CSXFile file = new CSXFile(fileSystem, MockCSXFile2Path);
            Assert.AreEqual(5, file.Schedule.RunEvery.Minutes);
            Assert.AreEqual(1, file.Schedule.RunEvery.Hours);
        }
    }
}
