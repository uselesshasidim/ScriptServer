using Moq;
using NUnit.Framework;
using ScriptCSHost;
using System;
using System.Collections.Generic;
using System.IO;
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

        #region Private Methods
        private FileStub GetFile()
        {
            return GetFile(new Mock<ITimer>());
        }

        private FileStub GetFile(Mock<ITimer> mockTimer)
        {
            return new FileStub(mockTimer.Object, DataHelper.GetFileSystem(), DataHelper.HostDirectory + DataHelper.MockCSXFile1Path);

        }


        private CSXFile GetFile_1_5_0()
        {
            Mock<ITimer> timer = new Mock<ITimer>();
            return new CSXFile(timer.Object, DataHelper.GetFileSystem(), DataHelper.HostDirectory + DataHelper.MockCSXFile2Path);
        }
        #endregion


        [Test]
        public void WHEN_File_IsLoaded_AND_First_Line_Has_Value_0_5_0_THEN_Its_Schedule_Is_Set_To_Run_Every_5_Minutes()
        {
            var file = GetFile();
            Assert.AreEqual(5, file.Schedule.RunEvery.Minutes);
            Assert.AreEqual(0, file.Schedule.RunEvery.Hours);         
        }

        [Test]
        public void WHEN_File_IsLoaded_AND_First_Line_Has_Value_1_5_0_THEN_Its_Schedule_Is_Set_To_Run_Every_1_Hour_And_5_Minutes()
        {
            var file = GetFile_1_5_0();
            Assert.AreEqual(5, file.Schedule.RunEvery.Minutes);
            Assert.AreEqual(1, file.Schedule.RunEvery.Hours);
        }

        [Test]
        public void WHEN_File_Is_Loaded_THEN_Timer_Is_Setup_According_To_The_Schedule()
        {
            var timer = new Mock<ITimer>();
            timer.SetupProperty(p => p.Interval);

            var file = GetFile(timer);
            
            Assert.AreEqual(file.Schedule.RunEvery.TotalMilliseconds, timer.Object.Interval);

        }

        [Test]
        public void WHEN_Timer_Ticked_The_File_Is_Executed()
        {
            // Load file with 5 seconds schedule
            var timer = new Mock<ITimer>();
            var file = GetFile(timer);
            

            // Mock like a tick was raised
            timer.Raise(e => e.TimerTicked += null, new TimerTickEventArgs());


            Assert.IsTrue(file.FileExecuted);

        }

        public class FileStub : CSXFile
        {
            public bool FileExecuted { get; set; }
            public FileStub(string fullPath):base(fullPath)
            {

            }

            public FileStub(ITimer timer, IFileSystem fileSystem, string fullPath):base(timer, fileSystem,fullPath)
            {

            }
            public override System.Diagnostics.Process Execute()
            {
                var p =  base.Execute();
                FileExecuted = true;
                return p;
            }
        }

        [Test]
        public void WHEN_Execute_Is_Called_THEN_The_CSX_File_Is_Exected()
        {

		    // Arrange file
			var file = GetFile();

			// Act
			var p = file.Execute();
				
			// Assert that the file is executed and the console window output is 'Ok'
			var expected = "ok\r\n";
			var actual = p.StandardOutput.ReadToEnd();
			Assert.AreEqual(expected, actual);
		   
        }

        [Test]
        public void WHEN_Execute_Is_Called_AND_File_Completes_Executing_THEN_Its_Process_Is_Destroyed()
        {
            var file = GetFile();
            var p = file.Execute();

            System.Threading.Thread.Sleep(6000);
            Assert.IsTrue(p.HasExited);
        }

    }
}
