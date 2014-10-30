using NUnit.Framework;
using ScriptCSHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScriptCSHostTests.Helpers;

namespace ScriptCSHostTests
{
    [TestFixture]
    public class FileManagerTest
    {


        [Test]
        public void GIVEN_Three_Files_In_The_Host_Directory_WHEN_Initialized_THEN_The_File_Count_Is_Three()
        {
         
            var fileSystem = FileHelper.GetMockFileSystem();

            FileManager fileManager = new FileManager(FileHelper.HostDirectory, fileSystem);

            Assert.AreEqual(3, fileManager.CSXFiles.Count);

        }


    }
}
