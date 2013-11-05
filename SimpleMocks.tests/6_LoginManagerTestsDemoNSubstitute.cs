namespace MyBilingProduct.tests
{
    [TestFixture]
    public class LoginManagerTestdNSubstitute
    {

        [Test]
        public void IsLoginOK_WhenCalled_WritesToLog_AAA()
        {
            ILogger fake = Substitute.For<ILogger>();
            
            LoginManagerWithMock lm = new LoginManagerWithMock(fake);
            lm.IsLoginOK("", "");

            fake.Received().Write(Arg.Is<string>(s => s.Contains("a")));

        }
        
        
        [Test]
        public void IsLoginOK_LoggerError_CallsWs()
        {
            IWebService svc = Substitute.For<IWebService>();
            ILogger fake = Substitute.For<ILogger>();
            fake.When(logger => logger.Write(Arg.Any<string>()))
                .Do(info => 
                        {
                            throw new Exception("fake exception");
                        });
                        
            var lm = new LoginManagerWithMockAndStub(fake,svc);
            lm.IsLoginOK("", "");

            svc.Received().Write(Arg.Any<string>());
        }
    }
}