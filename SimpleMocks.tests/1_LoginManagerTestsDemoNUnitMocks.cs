using MyBillingProduct;
using NUnit.Framework;
using NUnit.Mocks;


namespace MyBilingProduct.tests
{
    [TestFixture]
    public class LoginManagerTestsNunitMocks
    {

        [Test]
        public void IsLoginOK_WhenCalled_WritesToLog()
        {
            DynamicMock mockLog = new DynamicMock(typeof(ILogger));
            mockLog.Expect("Write","login ok: user: u");
            
            var loginManager = new LoginManagerWithMock((ILogger)mockLog.MockInstance);
            loginManager.IsLoginOK("", "");

            mockLog.Verify();
        }
        

        [Test]
        public void IsLoginOK_LoggerThrowsException_WritesToWebService()
        {
            DynamicMock stubLog = new DynamicMock(typeof(ILogger));
            DynamicMock mockService = new DynamicMock(typeof(IWebService));
            
            stubLog.ExpectAndThrow("Write",new LoggerException("fake exception"),"yo" );
            mockService.Expect("Write","got exception");
            
            var loginManager = 
                new LoginManagerWithMockAndStub((ILogger)stubLog.MockInstance,
                                                (IWebService) mockService.MockInstance);
            loginManager.IsLoginOK("", "");

            mockService.Verify();
        }
        
        
    }

    
}
