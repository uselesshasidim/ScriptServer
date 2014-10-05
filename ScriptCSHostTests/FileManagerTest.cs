using NUnit.Framework;
using ScriptCSHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptCSHostTests
{
    [TestFixture]
    public class FileManagerTest
    {
        [Test]
        public void RunningParseFilesFillsItsInternalListWithAllFilesInTheBaseDirectory()
        {
            // Arrange
            var fileSystem = DataHelper.GetFileSystem();
            FileManager fileManager = new FileManager(DataHelper.HostDirectory, fileSystem);

            // Act
            fileManager.ParseFiles();

            // Assert
            // The internal list of files are filled up with the two files in the mock directory
            Assert.AreEqual(2, fileManager.CSXFiles.Count);
        }

        [Test]
        public void RunningInitializeWillParse2FilesAndSetup2Timers()
        {

        }

    }
}
