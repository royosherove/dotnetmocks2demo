using MyBillingProduct;
using NSubstitute;
using NUnit.Framework;
using ILogger = MyBillingProduct.ILogger;
using LoggerException = MyBillingProduct.LoggerException;

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

            fake.Received().Write(Arg.Is<string>(s => s.Contains("login ok: user: u")));

        }

        [Test]
        public void Person_Properties_AreRecursivelyFake()
        {
            Person p = Substitute.For<Person>();

            Assert.IsNotNull(p.Manager.Manager);
        }

        public class Person
        {
            public virtual Person Manager { get; set; } 
        }


        [Test]
        public void IsLoginOK_LoggerError_CallsWs()
        {
            IWebService svc = Substitute.For<IWebService>();
            ILogger fake = Substitute.For<ILogger>();
            fake.When(logger => logger.Write(Arg.Any<string>()))
                .Do(info =>
                        {
                            throw new LoggerException("fake exception");
                        });

            var lm = new LoginManagerWithMockAndStub(fake, svc);
            lm.IsLoginOK("", "");

            svc.Received().Write(Arg.Any<string>());
        }

        
    }
}