using System;
using MyBillingProduct;
using NUnit.Framework;
using NUnit.Mocks;


namespace MyBilingProduct.tests
{

    class TestableLoginManagerWithStatics : LoginManagerWithStatics
    {
        public string Written;

        protected override void callLogger(string text)
        {
            Written = text;
        }
    }

    [TestFixture]
    public class LoginManagerWithStaticsTests
    {
        [Test]
        public void IsLoginOK_WhenCalled_CallsStaticLogger()
        {
           var lm = new TestableLoginManagerWithStatics();

            lm.IsLoginOK("a", "b");

            StringAssert.Contains("Yo",lm.Written);
        }
 
    }














    [TestFixture]
    public class LoginManagerTestsManual
    {
        class FakeLogger : ILogger
        {
            public string Text;

            public void Write(string text )
            {
                this.Text = text;
            }
        }

        [Test]
        public void IsLoginOK_WhenLoginOK_CallsLogger()
        {
            FakeLogger mockLog = new FakeLogger();

            var loginManager = new LoginManagerWithMock(mockLog);
            loginManager.AddUser("u","p");

            loginManager.IsLoginOK("u", "p");

            StringAssert.Contains("login ok: user: u", mockLog.Text);
        }

        [Test]
        public void IsLoginOK_LoggerThrowsException_CallsToWebService()
        {
            FakeLogger2 stubLog = new FakeLogger2();
            stubLog.WillThrow = new LoggerException("fake exception");

            FakeWebService mockService = new FakeWebService();


            LoginManagerWithMockAndStub loginManager =
                new LoginManagerWithMockAndStub(stubLog, mockService);
            loginManager.IsLoginOK("", "");

            StringAssert.Contains("got exception", mockService.Message);
        }



        class FakeLogger2 : ILogger
        {
            public Exception WillThrow;
            public string Text;

            public void Write(string text)
            {
                this.Text = text;
                if (WillThrow != null)
                {
                    throw WillThrow;
                }
            }
        }

        class FakeWebService : IWebService
        {
            public string Message;

            public void Write(string message)
            {
                this.Message = message;
            }
        }


        [Test]
        public void IsLoginOK_WhenCalled_CallsLogger()
        {
            var lm = new TestableLoginManagerWithStatics();

            lm.IsLoginOK("user", "somepass");

            StringAssert.Contains("ok",lm.LogText);

        }
        [Test]
        public void IsLoginOK_LoggerFailes_CallsWSWithMachineName()
        {
            var lm = new TestableLoginManagerWithStatics();
            lm.MachineNameWillBe = "machine";
            SystemTime.Set(new DateTime(2000, 1, 1));


            lm.IsLoginOK("user", "somepass");

            StringAssert.Contains("ok",lm.LogText);

        }

        [TearDown]
        public void afterEachTest()
        {
            //SystemTime.Reset();
        }

        //SOLID design patterns
//        class TestableLoginManagerWithStatics : LoginManagerWithStatics
//        {
//            protected override void CallWS(string text)
//            {
//                WSText = text;
//            }
//            public string LogText;
//            public string WSText;
//            public string MachineNameWillBe;
//
//            protected override string GetMachineName()
//            {
//                return MachineNameWillBe;
//            }
//            protected override void CallTheLogger(string text)
//            {
//                LogText = text;
//            }
//        }

    }

    public class SystemTime
    {
        private static DateTime fakeTime;

        public static void Set(DateTime dateTime)
        {
            fakeTime = dateTime;
        }
    }
}
