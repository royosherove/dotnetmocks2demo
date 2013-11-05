using MyBillingProduct;
using NUnit.Framework;
using Moq;
using Moq.Protected;


namespace MyBilingProduct.tests
{
    [TestFixture]
    public class LoginManagerTestsMoq
    {
        [Test]
        public void IsLoginOK_WhenCalled_WritesToLog_AAA()
        {
            Mock<ILogger> mockLog = new Mock<ILogger>();
            
            LoginManagerWithMock lm = new LoginManagerWithMock(mockLog.Object);
            lm.IsLoginOK("", "");

            mockLog.Verify(log => log.Write(It.IsAny<string>()));
        }
        
        [Test]
        public void IsLoginOK_LogError_WritesToWebService()
        {
            Mock<ILogger> stubLog = new Mock<ILogger>();
            stubLog.Setup(x => x.Write(It.IsAny<string>()))
                .Throws(new LoggerException("fake exception"));
            

            Mock<IWebService> mockService = new Mock<IWebService>();
            
            LoginManagerWithMockAndStub lm = 
                new LoginManagerWithMockAndStub(stubLog.Object,mockService.Object);
            lm.IsLoginOK("", "");

            mockService.Verify(svc=>svc.Write(
                                It.Is<string>(s => s.Contains("a"))));
        }
        
        
        [Test]
        public void OverrideVirtualMethod()
        {
            Mock<LoginManagerWithVirtualMethod> lm = new Mock<LoginManagerWithVirtualMethod>();
            
            lm.Protected().Setup("WriteToLog","yo").Verifiable();
            
            lm.Object.IsLoginOK("", "");

            lm.Verify();
        }
    }

    
}
