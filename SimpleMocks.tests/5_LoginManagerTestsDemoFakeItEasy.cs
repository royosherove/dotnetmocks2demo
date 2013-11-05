using FakeItEasy;
using MyBillingProduct;
using NUnit.Framework;
using ILogger = MyBillingProduct.ILogger;
using LoggerException = MyBillingProduct.LoggerException;


namespace MyBilingProduct.tests
{
    [TestFixture]
    public class LoginManagerTestsFakeItEasy
    {

        [Test]
        public void IsLoginOK_WhenCalled_WritesToLog()
        {
            ILogger fake = A.Fake<ILogger>(); 

            LoginManagerWithMock lm = new LoginManagerWithMock(fake);
            lm.IsLoginOK("", "");

            A.CallTo(()=> fake.Write(A<string>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void IsLoginOK_WhenLoggerThrows_CallsWebService()
        {
            ILogger fakeLog = A.Fake<ILogger>();
            IWebService fakeWs = A.Fake<IWebService>();
            A.CallTo(() => fakeLog.Write(A<string>.Ignored))
                .Throws(new LoggerException());

            
            LoginManagerWithMockAndStub lm = 
                new LoginManagerWithMockAndStub(fakeLog, fakeWs);
            lm.IsLoginOK("", "");

            A.CallTo(()=> fakeWs.Write(
                                    A<string>.That.Contains("got exception")))
                                    .MustHaveHappened();
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
