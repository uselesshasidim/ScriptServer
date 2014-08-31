using NUnit.Framework;
using ScriptCSHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ScriptCSHostTests{    

    [TestFixture]
    public class CSXFileTest
    {
        const string CSXFilePath = @"C:\app\ScriptCSHost\CSX Files\app.csx";

        [Test]
        public void First_Line_With_Value_0_5_0_Returns_5_Minutes()
        {                       
            CSXFile file = CSXFile.LoadFile(CSXFilePath);
            Assert.AreEqual(5, file.Schedule.RunEvery.Minutes);                       
        }
        
    }
}
