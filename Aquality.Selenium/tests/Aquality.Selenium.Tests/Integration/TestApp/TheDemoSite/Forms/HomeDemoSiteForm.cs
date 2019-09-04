using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class HomeDemoSiteForm : TheDemoSiteForm
    {
        public HomeDemoSiteForm() : base(By.XPath("//strong[contains(.,'1. Home ')]"), "Home")
        {
        }

        public ILabel FirstScrollableExample => ElementFactory.GetLabel(
            By.XPath("//div[@align='center']//tr[.//strong[contains(.,'index.php')]]//div[@align='left']"), "First example");

        protected override string UrlPart => string.Empty;
    }
}
