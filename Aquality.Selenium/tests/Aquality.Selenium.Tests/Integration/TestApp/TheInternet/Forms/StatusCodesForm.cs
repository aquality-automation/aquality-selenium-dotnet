using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class StatusCodesForm : TheInternetForm
    {
        public StatusCodesForm() : base(By.Id("content"), "Status Codes")
        {
        }

        protected override string UrlPart => "status_codes";
    }
}
