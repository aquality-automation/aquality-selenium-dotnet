using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using Aquality.Selenium.Waitings;
using NUnit.Framework;
using System;

namespace Aquality.Selenium.Tests.Integration.Elements
{
    internal class LinkTests : UITest
    {
        private readonly RedirectorForm redirectorForm = new RedirectorForm();

        [SetUp]
        public void BeforeTest()
        {
            redirectorForm.Open();
        }

        [Test]
        public void Should_BePossibleTo_Click()
        {
            var link = redirectorForm.RedirectLnk;
            link.Click();
            WaitForRedirect();
            Assert.AreEqual(new StatusCodesForm().Url.ToLower(), BrowserManager.Browser.CurrentUrl.ToLower());
        }

        [Test]
        public void Should_BePossibleTo_GetHref()
        {
            var link = redirectorForm.RedirectLnk;
            BrowserManager.Browser.GoTo(link.Href);
            WaitForRedirect();
            Assert.AreEqual(new StatusCodesForm().Url.ToLower(), BrowserManager.Browser.CurrentUrl.ToLower());
        }

        private void WaitForRedirect()
        {
            ConditionalWait.WaitFor(() => BrowserManager.Browser.CurrentUrl.Equals(new StatusCodesForm().Url, StringComparison.OrdinalIgnoreCase));
        }
    }
}
