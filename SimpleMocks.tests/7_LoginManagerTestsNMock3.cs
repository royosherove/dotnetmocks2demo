using System;
using FakeItEasy;
using MyBillingProduct;
using NMock;
using NMock.Syntax;
using NSubstitute;
using NUnit.Framework;
using Rhino.Mocks;
using ILogger = MyBillingProduct.ILogger;
using LoggerException = MyBillingProduct.LoggerException;


namespace MyBilingProduct.tests
{
    [TestFixture]
    public class LoginManagerTestsNMock3
    {

        [Test]
        public void IsLoginOK_WhenCalled_WritesToLog()
        {
            var factory = new MockFactory();
            Mock<ILogger> logger = factory.CreateMock<ILogger>();
            logger.Expects.One.Method(_ => _.Write(null)).WithAnyArguments();

            var lm = new LoginManagerWithMock(logger.MockObject);
            lm.IsLoginOK("a", "b");

            logger.VerifyAllExpectations();
        }

        [Test]
        public void IsLoginOK_WhenLoggerThrows_CallsWebService()
        {
            var fac = new MockFactory();
            var webservice = fac.CreateMock<IWebService>();
            webservice.Expects.AtLeastOne.Method(_ => _.Write(null)).With(NMock.Is.StringContaining("s"));

            var logger = fac.CreateMock<ILogger>();
            logger.Stub.Out.Method(_ => _.Write(null))
                .WithAnyArguments()
                .Will(Throw.Exception(new Exception("dude this is fake")));

            var lm = 
                new LoginManagerWithMockAndStub(logger.MockObject,webservice.MockObject);
            lm.IsLoginOK("a", "b");


            webservice.VerifyAllExpectations();

        }























        //[Test]
        //public void IsLoginOK_LogError_WritesToWebService()
        //{
        //    ILogger stubLogger = A.Fake<ILogger>();
        //    IWebService mockService = A.Fake<IWebService>();
            
        //    Mock<IWebService> mockService = new Mock<IWebService>();
            
        //    LoginManagerWithMockAndStub lm = 
        //        new LoginManagerWithMockAndStub(stubLog.Object,mockService.Object);
        //    lm.IsLoginOK("", "");

        //    mockService.Verify(svc=>svc.Write("got exception"));
        //    mockService.Verify(
        //        svc=>svc.Write(
        //            It.Is<LoggerException>(
        //                ex=>ex.Message.Contains("bla"))));
        //}
        
//        
//        [Test]
//        public void OverrideVirtualMethod()
//        {
//            Mock<LoginManagerWithVirtualMethod> lm = new Mock<LoginManagerWithVirtualMethod>();
//            
//            lm.Protected().Setup("WriteToLog","yo").Verifiable();
//            
//            lm.Object.IsLoginOK("", "");
//
//            lm.Verify();
//        }
    }

    
}
