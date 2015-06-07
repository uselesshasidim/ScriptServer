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
using ScriptCSHostTests.Helpers;

namespace ScriptCSHostTests{    

    [TestFixture]
    public class CSXFileTest
    {

        [Test]
        public void WHEN_File_IsLoaded_AND_First_Line_Has_Value_0_0_2_THEN_Its_Schedule_Is_Set_To_Run_Every_2_Seconds()
        {
            var file = FileHelper.GetFile_0_0_2(new Mock<ITimer>().Object);
            Assert.AreEqual(2, file.Schedule.RunEvery.Seconds);
            Assert.AreEqual(0, file.Schedule.RunEvery.Minutes);
            Assert.AreEqual(0, file.Schedule.RunEvery.Hours);
        }

        [Test]
        public void WHEN_File_IsLoaded_AND_First_Line_Has_Value_0_5_0_THEN_Its_Schedule_Is_Set_To_Run_Every_5_Minutes()
        {
            var file = FileHelper.GetFile_0_5_0();
            Assert.AreEqual(5, file.Schedule.RunEvery.Minutes);
            Assert.AreEqual(0, file.Schedule.RunEvery.Hours);         
        }

        [Test]
        public void WHEN_File_IsLoaded_AND_First_Line_Has_Value_1_5_0_THEN_Its_Schedule_Is_Set_To_Run_Every_1_Hour_And_5_Minutes()
        {
            var file = FileHelper.GetFile_1_5_0();
            Assert.AreEqual(5, file.Schedule.RunEvery.Minutes);
            Assert.AreEqual(1, file.Schedule.RunEvery.Hours);
        }

        [Test]
        public void WHEN_File_Is_Loaded_THEN_Timer_Is_Setup_According_To_The_Schedule()
        {
            var timer = new Mock<ITimer>();
            timer.SetupProperty(p => p.Interval);

            var file = FileHelper.GetFile_0_5_0(timer);
            
            Assert.AreEqual(file.Schedule.RunEvery.TotalMilliseconds, timer.Object.Interval);

        }


        [Test]
        public void WHEN_Timer_Ticked_The_File_Is_Executed()
        {
            // Load file with 5 seconds schedule
            var timer = new Mock<ITimer>();
            var file = FileHelper.GetFile_0_5_0(timer);
            

            // Mock like a tick was raised
            timer.Raise(e => e.TimerTicked += null, new TimerTickEventArgs());


            Assert.IsTrue(file.FileExecuted);

        }

        [Test]
        public void WHEN_File_Is_Loaded_THEN_Timer_Is_Started()
        {
            var timer = new Mock<ITimer>();

            var file = FileHelper.GetFile_0_0_2(timer.Object);

            timer.Verify(e => e.Start());
        }

        [Test]
        public void WHEN_File_Loaded_AND_Schedule_Is_0_0_2_THEN_File_Is_Executed_Twice_In_5_Seconds()
        {
            var timer = new SystemTimer();
            

            var file = FileHelper.GetFile_0_0_2(timer);

            System.Threading.Thread.Sleep(5000);
            var expectedCount = 2;
            var actual = file.ExecutionCount;

            Assert.AreEqual(expectedCount, actual);
        }

     

        [Test]
        public void WHEN_Execute_Is_Called_THEN_The_CSX_File_Is_Exected()
        {

		    // Arrange file
            var file = FileHelper.GetFile_0_5_0();

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
            var file = FileHelper.GetFile_0_5_0();
            var p = file.Execute();

            System.Threading.Thread.Sleep(6000);
            Assert.IsTrue(p.HasExited);
        }

    }
}
