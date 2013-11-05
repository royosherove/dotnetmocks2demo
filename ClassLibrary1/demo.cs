using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using NUnit.Framework;
using White.Core;
using White.Core.UIItems.WindowItems;

namespace ClassLibrary1
{
    [TestFixture]
    public class Class1
    {
        private TransactionScope _scope = null;
        [SetUp]
        public void setup()
        {
            _scope = new TransactionScope(TransactionScopeOption.Required);
        }

        [Test]
        public void withrollback()
        {
           
        }

        [TearDown]
        public void teardown()
        {
            _scope.Dispose();
        }







        [Test]
        public void test()
        {
            string path =
               @"Z:\Dropbox\repos\course-tdd-3days\demos\WindowsFormsApplication1\bin\Debug\windowsformsapplication1.exe";
            Application app = Application.Launch(path);
            Window window = app.GetWindow("Form1");
            Assert.IsNotNull(window);

            app.Kill();

        }
    }
}
