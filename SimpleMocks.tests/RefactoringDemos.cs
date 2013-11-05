using MyBillingProduct;
using NUnit.Framework;

namespace SimpleMocks.tests
{
    class TestableLoginManagerWithStatics : LoginManagerWithStatics
    {
        public string LoggerText;
        public string MachineNameWillBe;

        protected override void CallLogger(string text)
        {
            LoggerText = text;
        }
        protected override string GetMachineName()
        {
            return MachineNameWillBe;
        }

    }
    
    [TestFixture]
    public class RefactoringDemos
    {
        [Test]
        public void IsLoginOK_WhenCalled_CallsLogger()
        {
            TestableLoginManagerWithStatics lm = new TestableLoginManagerWithStatics();
            lm.IsLoginOK("a", "b");

            StringAssert.Contains("a",lm.LoggerText);
        }


    }
}
