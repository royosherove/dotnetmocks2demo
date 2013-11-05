using System;
using MyBillingProduct;
using NUnit.Framework;
using TypeMock.ArrangeActAssert;


namespace MyBilingProduct.tests
{
    [TestFixture, Isolated]
    public class LoginManagerTestsIsolator
    {

        [Test]
        public void IsLoginOK_WhenCalled_WritesToLog()
        {
            ILogger fakeILogger = Isolate.Fake.Instance<ILogger>();

            var lm = new LoginManagerWithMock(fakeILogger);
            lm.IsLoginOK("a", "b");

            Isolate.Verify
                .WasCalledWithExactArguments(() => fakeILogger.Write("yo"));
        }

        [Test]
        public void IsLoginOK_LoggerThrowsException_CallsWebService()
        {
            ILogger fakeLogger = Isolate.Fake.Instance<ILogger>();
            Isolate
                .WhenCalled(() => fakeLogger.Write(""))
                .WillThrow(new LoggerException());

            IWebService fakeWebService = Isolate.Fake.Instance<IWebService>();

            LoginManagerWithMockAndStub lm =
                new LoginManagerWithMockAndStub(fakeLogger, fakeWebService);
            lm.IsLoginOK("", "");

            Isolate.Verify
                .WasCalledWithAnyArguments(() => fakeWebService.Write(""));
        }



    }
}
