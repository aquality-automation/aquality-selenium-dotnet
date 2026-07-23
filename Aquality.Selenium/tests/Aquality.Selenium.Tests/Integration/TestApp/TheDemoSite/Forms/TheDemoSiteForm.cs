using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal abstract class TheDemoSiteForm : TheInternetForm
    {
        private const string BaseUrl = "http://eprint.com.hr/demo/index.php";

        protected TheDemoSiteForm(By locator, string name) : base(locator, name)
        {
        }

        public override string Url => BaseUrl + UrlPart;
    }
}
