using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using System;

namespace Aquality.Selenium.Tests.Integration.Elements
{
    internal class LinkTests : UITest
    {
        private readonly RedirectorForm redirectorForm = new();

        [SetUp]
        public void BeforeTest()
        {
            redirectorForm.Open();
        }

        [Test]
        public void Should_BePossibleTo_Click()
        {
            var link = redirectorForm.RedirectLink;
            link.Click();
            WaitForRedirect();
            Assert.That(AqualityServices.Browser.CurrentUrl.ToLower(), Is.EqualTo(new StatusCodesForm().Url.ToLower()));
        }

        [Test]
        public void Should_BePossibleTo_GetHref()
        {
            var link = redirectorForm.RedirectLink;
            AqualityServices.Browser.GoTo(link.Href);
            WaitForRedirect();
            Assert.That(AqualityServices.Browser.CurrentUrl.ToLower(), Is.EqualTo(new StatusCodesForm().Url.ToLower()));
        }

        private static void WaitForRedirect()
        {
            AqualityServices.ConditionalWait.WaitFor(() => AqualityServices.Browser.CurrentUrl.Equals(new StatusCodesForm().Url, StringComparison.OrdinalIgnoreCase));
        }
    }
}
