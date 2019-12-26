using Aquality.Selenium.Browsers;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal abstract class TheInternetForm : Form
    {
        private const string BaseUrl = "http://the-internet.herokuapp.com/";

        protected TheInternetForm(By locator, string name) : base(locator, name)
        {
        }

        protected abstract string UrlPart { get; }

        public string Url => BaseUrl + UrlPart;

        public void Open()
        {
            AqualityServices.Browser.GoTo(Url);
            AqualityServices.Browser.WaitForPageToLoad();
        }
    }
}
