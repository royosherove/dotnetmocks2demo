using System;
using System.IO;
using NUnit.Framework;
using ProductinWithInheritance;

namespace ProductionWithInheritance.UnitTests
{
    [TestFixture]
    public class CsvLoggerTests
    {
        [Test]
        public void SetTarget_Unset_Throws()
        {
            CsvLogger logger = new CsvLogger();

            Assert.Throws<Exception>(delegate
                                         {
                                             logger.Write(new LogMessage());
                                         });
            
        }

        [Test]
        public void Write_EmptyMessage_AppearsInFile()
        {
            CsvLogger logger = new CsvLogger();
            logger.SetTarget(".\\file.txt");

            logger.Write(new LogMessage());
            string text = File.ReadAllText(".\\file.txt");

            Assert.AreEqual("",text.Trim());
        }
    }
}
