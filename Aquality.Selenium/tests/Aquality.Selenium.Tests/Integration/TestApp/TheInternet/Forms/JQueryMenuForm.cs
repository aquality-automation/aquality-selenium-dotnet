using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class JQueryMenuForm : TheInternetForm
    {
        public JQueryMenuForm() : base(By.Id("menu"), "JQueryUI menu")
        {
        }

        public static IButton EnabledButton => ElementFactory.GetButton(By.XPath("//*[@id='ui-id-2' or @id='ui-id-3']"), "Enabled");

        public static bool IsEnabledButtonFocused => EnabledButton.GetAttribute("class").Contains("focus");

        protected override string UrlPart => "jqueryui/menu";
    }
}
