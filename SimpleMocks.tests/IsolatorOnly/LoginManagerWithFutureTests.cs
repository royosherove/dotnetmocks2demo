using System;
using MyBillingProduct;
using NUnit.Framework;
using Step1Mocks;
using TypeMock.ArrangeActAssert;

// FakeItEasy, NSubstitute, unconstrained: Typemock or JustMock
// Mockito or Powermock (Mockito Plus powermock)

namespace MyBilingProduct.tests
{
    [TestFixture, Isolated]
    public class LoginManagerWithFutureTests
    {

        [Test, Isolated]
        public void IsLoginOK_WhenCalled_WritesToLog()
        {
            var fakeLogger = Isolate.Fake.Instance<RealLogger>();
            Isolate.Swap.AllInstances<RealLogger>().With(fakeLogger);

            var lm = new LoginManagerWithFutureObject();
            lm.IsLoginOK("a", "b");

            Isolate.Verify.WasCalledWithAnyArguments(() =>
                fakeLogger.Write(""));
        }

        [Test, Isolated]
        public void IsLoginOK_StaticLoggerThrowsException_CallsStaticWebService()
        {
            var fakeLogger = Isolate.Fake.Instance<RealLogger>();
            Isolate
                .WhenCalled(() => fakeLogger.Write(""))
                .WillThrow(new LoggerException("fake exception"));
            Isolate.Swap.NextInstance<RealLogger>().With(fakeLogger);

            var fakeWebService = Isolate.Fake.Instance<WebService>();
            Isolate.Swap.AllInstances<WebService>().With(fakeWebService);


            var lm = new LoginManagerWithFutureObject();
            lm.IsLoginOK("a", "b");

            Isolate.Verify.WasCalledWithAnyArguments(() => 
                fakeWebService.Write(""));
        }

        [Test]
        public void privatestuff()
        {
            Isolate.NonPublic.WhenCalled<RealLogger>("somemethodthatisprivate")
                .WillReturn(3);
        }


        [Test, Isolated]
        public void IsLoginOK_StaticLoggerThrowsException_CallsStaticWebServiceWithCorrectText()
        {
            string textWrittenToWebService = null;
            Isolate.Fake.StaticMethods<StaticLogger>();
            Isolate.Fake.StaticMethods<StaticWebService>();
            Isolate
                .WhenCalled(() => StaticLogger.Write(""))
                .WillThrow(new LoggerException("fake exception"));

            Isolate
                .WhenCalled(() => StaticWebService.Write(""))
                .DoInstead(context => 
                    textWrittenToWebService =(string) context.Parameters[0]);

            var lm = new LoginManagerWithStatics();
            lm.IsLoginOK("a", "b");


            //remove dependency on machine name
            StringAssert.Contains("fake exception",textWrittenToWebService);
        }

    }
}
