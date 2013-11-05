using MyBillingProduct;
using NUnit.Framework;
using Rhino.Mocks;


namespace MyBilingProduct.tests
{
    [TestFixture]
    public class LoginManagerTests
    {
        [Test]
        public void IsLoginOK_WhenCalled_WritesToLog_AAA()
        {
            ILogger mockLog = MockRepository.GenerateMock<ILogger>();
            
            LoginManagerWithMock lm = new LoginManagerWithMock(mockLog);
            lm.IsLoginOK("a", "b");
            
            mockLog
                .AssertWasCalled(logger => logger.Write("login ok: user: u"));
        }
        
        
        [Test]

        public void IsLoginOK_LogError_WritesToWebService()
        {
            var stublog = MockRepository.GenerateStub<ILogger>();
            stublog
                .Expect(logger => logger.Write("yo"))
                .Throw(new LoggerException("fake exception"));

            var mockService = MockRepository.GenerateMock<IWebService>();
            
            LoginManagerWithMockAndStub lm = 
                new LoginManagerWithMockAndStub(stublog,mockService);
            lm.IsLoginOK("a", "b") ;

            mockService.AssertWasCalled(service => service.Write("got exception"));
        }
        
      
    }

    
}
