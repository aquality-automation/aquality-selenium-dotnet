using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class JQueryMenuForm : TheInternetForm
    {
        public JQueryMenuForm() : base(By.Id("menu"), "JQueryUI menu")
        {
        }

        public IButton EnabledButton => ElementFactory.GetButton(By.Id("ui-id-2"), "Enabled");

        public bool IsEnabledButtonFocused => EnabledButton.GetAttribute("class").Contains("focus");

        protected override string UrlPart => "jqueryui/menu";
    }
}
