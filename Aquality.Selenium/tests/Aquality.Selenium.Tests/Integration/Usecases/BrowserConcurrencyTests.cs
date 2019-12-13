﻿using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using System.Threading;

namespace Aquality.Selenium.Tests.Integration.Usecases
{
    internal class BrowserConcurrencyTests
    {
        [Test]
        public void Should_BePossibleTo_CreateDifferentBrowsersInDifferentThreads()
        {
            var thread01 = new Thread(() => 
            {
                var checkBoxesForm = new CheckBoxesForm();
                checkBoxesForm.Open();
                Assert.AreEqual(checkBoxesForm.Url, AqualityServices.Browser.CurrentUrl);
                AqualityServices.Browser.Quit();
            });
            var thread02 = new Thread(() =>
            {
                var authForm = new AuthenticationForm();
                authForm.Open();
                Assert.AreEqual(authForm.Url, AqualityServices.Browser.CurrentUrl);
                AqualityServices.Browser.Quit();
            });

            thread01.Start();
            thread02.Start();

            thread01.Join();
            thread02.Join();

            thread01.Interrupt();
            thread02.Interrupt();
        }
    }
}
