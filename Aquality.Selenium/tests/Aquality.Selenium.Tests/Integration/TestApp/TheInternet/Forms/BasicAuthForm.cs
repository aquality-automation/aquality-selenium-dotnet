using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class BasicAuthForm : TheInternetForm
    {
        private const string userAndPass = "admin";
        private const string domain = "the-internet.herokuapp.com";
        private readonly ILabel CongratulationsLabel = ElementFactory.GetLabel(By.XPath("//p[contains(., 'Congratulations')]"), "Congratulations");

        public BasicAuthForm() : base(By.Id("content"), "Basic Auth")
        {
        }

        protected override string UrlPart => "basic_auth";

        public static string User => userAndPass;
        
        public static string Password => userAndPass;

        public static string Domain => domain;

        public bool IsCongratulationsPresent => CongratulationsLabel.State.IsDisplayed;
    }
}
