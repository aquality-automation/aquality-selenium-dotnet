using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class KeyPressesForm : TheInternetForm
    {
        public KeyPressesForm() : base(By.XPath("//h3[contains(.,'Key Presses')]"), "Key Presses")
        {
        }

        protected override string UrlPart => "key_presses";

        public ITextBox InputTextBox => ElementFactory.GetTextBox(By.Id("target"), "Input");
    }
}
