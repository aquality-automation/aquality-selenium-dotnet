using Aquality.Selenium.Browsers;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.a1qa.Forms
{
    internal abstract class TheA1qaForm : Form
    {
        private const string BaseUrl = "https://www.a1qa.com/";

        protected TheA1qaForm(By locator, string name) : base(locator, name)
        {
        }

        protected abstract string UrlPart { get; }

        public string Url => BaseUrl + UrlPart;

        public void Open()
        {
            AqualityServices.Browser.GoTo(Url);
            AqualityServices.Browser.WaitForPageToLoad();
            AqualityServices.Browser.Maximize();
        }
    }
}
