using System;
using System.Transactions;
using MyBillingProduct;
using NUnit.Framework;
using Step1Mocks;
using TypeMock;
using TypeMock.ArrangeActAssert;


namespace MyBilingProduct.tests
{
    [TestFixture]
    public class LoginManagerWithStaticsTestsDemoIsolator
    {
        private TransactionScope scope = null;
        [SetUp]
        public void setup()
        {
            scope = new TransactionScope(TransactionScopeOption.Required);
        }

        [TearDown]
        public void aftereachtest()
        {
            scope.Dispose();
        }

        //typemock.com
        [Test, Isolated]
        public void IsLoginOK_WhenCalled_WritesToLog()
        {
            Isolate.Fake.StaticMethods(typeof(StaticLogger));
            var lm = new LoginManagerWithStatics();

            lm.IsLoginOK("a", "b");

            Isolate.Verify
                .WasCalledWithAnyArguments(() =>  StaticLogger.Write(""));
        }    

        [Test, Isolated]
        public void IsLoginOK_StaticLoggerThrowsException_CallsStaticWebService()
        {
            Isolate.Fake.StaticMethods<StaticLogger>();
            Isolate.Fake.StaticMethods<StaticWebService>();
            Isolate
                .WhenCalled(() => StaticLogger.Write("anything"))
                .WillThrow(new LoggerException("fake exception"));


            var lm = new LoginManagerWithStatics();
            lm.IsLoginOK("a", "b");

            Isolate.Verify.WasCalledWithAnyArguments(() => 
                                StaticWebService.Write(""));
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


            StringAssert.Contains("fake exception",textWrittenToWebService);
        }    
    
    }
}
