using System;
using NUnit.Framework;
using Step1Mocks;

namespace SimpleMocks.tests
{
    [TestFixture]
    public class StringCalculatorTests
    {
        [Test]
        public void Add_EmptyString_ReturnsZero()
        {
            StringCalculator sc = MakeCalc();

            int result = sc.Add("");

            Assert.AreEqual(0,result);
        }

        private static StringCalculator MakeCalc()
        {
            return new StringCalculator();
        }

        [Test]
        public void Add_NegativeNumbers_Throws()
        {
            StringCalculator calc = MakeCalc();

            int SOME_POSITIVE = 1;
            Assert.Throws<ArgumentException>(delegate
                {
                    calc.Add(-1, SOME_POSITIVE);
                });


        }

        [TestCase("1",1)]
        [TestCase("2",2)]
        [TestCase("3",3)]
        [TestCase("30",30)]
        public void Add_SingleNumber_ReturnsThatNumber(string numbers, int expected)
        {
            StringCalculator sc = MakeCalc();

            int result = sc.Add(numbers);

            Assert.AreEqual(expected,result);
        }

        [TestCase("1,2", 3)]
        [TestCase("1,3", 4)]
        [TestCase("10,30",40)]
        public void Add_SeveralNumbers_SumsThemUp(string numbers, int expected)
        {
            StringCalculator stringCalculator = MakeCalc();

            int result = stringCalculator.Add(numbers);

            Assert.AreEqual(expected,result);
        }

        //[Test]
        //public void Add_WhenCalled_CallsLogger()
        //{
        //    FakeLogger mockLog = new FakeLogger();
        //    StringCalculator sc = new StringCalculator(mockLog);

        //    sc.Add("1");

        //    StringAssert.Contains("1",mockLog.LastLog);
        //}

        //[Test]
        //public void Add_LoggerThrows_CallsWebService()
        //{
        //    FakeLogger log = new FakeLogger();
        //    log.Willthrow = new Exception("fake");


        //}

    }
}
