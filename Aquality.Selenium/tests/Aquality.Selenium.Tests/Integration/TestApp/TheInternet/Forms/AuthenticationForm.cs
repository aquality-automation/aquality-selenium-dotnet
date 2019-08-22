using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class AuthenticationForm : TheInternetForm
    {
        private static readonly string LoginLblXpath = "//form[@id='login']";

        public AuthenticationForm() : base(By.XPath(LoginLblXpath), "login")
        {
        }

        public ITextBox UserNameTextBox => ElementFactory.GetTextBox(By.Id("username"), "username");

        public ITextBox PasswordTextBox => ElementFactory.GetTextBox(By.Id("password"), "password");

        public ILabel LoginLabel => ElementFactory.GetLabel(By.XPath(LoginLblXpath), "Login");

        public ITextBox NotExistTextBox => ElementFactory.GetTextBox(By.XPath("//div[@class='not exist element']"), "not exist element");

        protected override string UrlPart => "login";

        public ILabel GetCustomElementBasedOnLoginLabel(string childXpath)
        {
            return ElementFactory.GetLabel(By.XPath(LoginLblXpath + childXpath), "Custom Element Based On Login");
        }
    }
}
