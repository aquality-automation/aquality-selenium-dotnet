using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.ManyTools.Forms
{
    internal class BrowserLanguageForm : ManyToolsForm<BrowserLanguageForm>
    {
        public BrowserLanguageForm() : base(By.Id("maincontent"), "Browser language")
        {
        }

        protected override string UrlPart => "http-html-text/browser-language/";
    }
}
