using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class JavaScriptAlertsForm : TheInternetForm
    {
        public JavaScriptAlertsForm() : base(By.Id("content"), "JavaScript Alerts")
        {
        }

        public IButton JsAlertBtn => ElementFactory.GetButton(By.XPath("//button[@onclick='jsAlert()']"), "JS Alert");

        public IButton JsConfirmBtn => ElementFactory.GetButton(By.XPath("//button[@onclick='jsConfirm()']"), "JS Confirm");

        public IButton JsPromptBtn => ElementFactory.GetButton(By.XPath("//button[@onclick='jsPrompt()']"), "JS Prompt");

        public ILabel ResultLbl => ElementFactory.GetLabel(By.Id("result"), "Result");

        protected override string UrlPart => "javascript_alerts";
    }
}
