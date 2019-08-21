using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Integration.TestApp;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration
{
    internal class AlertTests : UITest
    {
        private readonly JavaScriptAlertsForm alertsForm = new JavaScriptAlertsForm();

        [SetUp]
        public void BeforeTest()
        {
            BrowserManager.Browser.GoTo(TheInternetPage.JavaScriptAlerts);
        }

        [Test]
        public void Should_BePossibleTo_AcceptAlert()
        {
            alertsForm.JsAlertBtn.Click();
            BrowserManager.Browser.HandleAlert(AlertAction.Accept);
            Assert.AreEqual("You successfuly clicked an alert", alertsForm.ResultLbl.GetText());
        }

        [Test]
        public void Should_BePossibleTo_AcceptConfirmationAlert()
        {
            alertsForm.JsConfirmBtn.Click();
            BrowserManager.Browser.HandleAlert(AlertAction.Accept);
            Assert.AreEqual("You clicked: Ok", alertsForm.ResultLbl.GetText());
        }

        [Test]
        public void Should_BePossibleTo_DeclineConfirmationAlert()
        {
            alertsForm.JsConfirmBtn.Click();
            BrowserManager.Browser.HandleAlert(AlertAction.Decline);
            Assert.AreEqual("You clicked: Cancel", alertsForm.ResultLbl.GetText());
        }

        [Test]
        public void Should_BePossibleTo_AcceptPromptAlertWithText()
        {
            alertsForm.JsPromptBtn.Click();
            var text = "accept alert text";
            BrowserManager.Browser.HandleAlert(AlertAction.Accept, text);
            Assert.AreEqual($"You entered: {text}", alertsForm.ResultLbl.GetText());
        }

        [Test]
        public void Should_BePossibleTo_DeclinePromptAlertWithText()
        {
            alertsForm.JsPromptBtn.Click();
            BrowserManager.Browser.HandleAlert(AlertAction.Decline, "decline alert text");
            Assert.AreEqual("You entered: null", alertsForm.ResultLbl.GetText());
        }

        [Test]
        public void Should_Throw_NoAlertPresentExceptionIfNoAlertPresent()
        {
            Assert.Throws<NoAlertPresentException>(() => BrowserManager.Browser.HandleAlert(AlertAction.Decline));
        }

        [Test]
        public void Should_Throw_NoAlertPresentExceptionIfNoPromptAlertPresent()
        {
            Assert.Throws<NoAlertPresentException>(() => BrowserManager.Browser.HandleAlert(AlertAction.Decline, "Hello"));
        }
    }
}
