using NUnit.Framework;
using ScriptCSHost;
using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScriptCSHostTests{    

    [TestFixture]
    public class CSXFileTest
    {
        const string MockCSXFile1Path = @"C:\app\ScriptCSHost\CSX Files\app.csx";
        const string MockCSXFile1Data = @"//SCHEDULE-TIME-SPAN: 0 5 0
";

        [Test]
        public void First_Line_With_Value_0_5_0_Returns_5_Minutes()
        {      
            // Mock file system 
            var fileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                {MockCSXFile1Path,new MockFileData(MockCSXFile1Data)}
            });

            CSXFile file = new CSXFile(fileSystem, MockCSXFile1Path);
            Assert.AreEqual(5, file.Schedule.RunEvery.Minutes);                       
        }
        
    }
}
