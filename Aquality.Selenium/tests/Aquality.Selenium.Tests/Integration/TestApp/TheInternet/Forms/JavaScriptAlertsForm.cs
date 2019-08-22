using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class JavaScriptAlertsForm : TheInternetForm
    {
        public JavaScriptAlertsForm() : base(By.Id("content"), "JavaScript Alerts")
        {
        }

        public IButton JsAlertButton => ElementFactory.GetButton(By.XPath("//button[@onclick='jsAlert()']"), "JS Alert");

        public IButton JsConfirmButton => ElementFactory.GetButton(By.XPath("//button[@onclick='jsConfirm()']"), "JS Confirm");

        public IButton JsPromptButton => ElementFactory.GetButton(By.XPath("//button[@onclick='jsPrompt()']"), "JS Prompt");

        public ILabel ResultLabel => ElementFactory.GetLabel(By.Id("result"), "Result");

        protected override string UrlPart => "javascript_alerts";
    }
}
