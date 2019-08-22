using Aquality.Selenium.Browsers;
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
            alertsForm.Open();
        }

        [Test]
        public void Should_BePossibleTo_AcceptAlert()
        {
            alertsForm.JsAlertButton.Click();
            BrowserManager.Browser.HandleAlert(AlertAction.Accept);
            Assert.AreEqual("You successfuly clicked an alert", alertsForm.ResultLabel.GetText());
        }

        [Test]
        public void Should_BePossibleTo_AcceptConfirmationAlert()
        {
            alertsForm.JsConfirmButton.Click();
            BrowserManager.Browser.HandleAlert(AlertAction.Accept);
            Assert.AreEqual("You clicked: Ok", alertsForm.ResultLabel.GetText());
        }

        [Test]
        public void Should_BePossibleTo_DeclineConfirmationAlert()
        {
            alertsForm.JsConfirmButton.Click();
            BrowserManager.Browser.HandleAlert(AlertAction.Decline);
            Assert.AreEqual("You clicked: Cancel", alertsForm.ResultLabel.GetText());
        }

        [Test]
        public void Should_BePossibleTo_AcceptPromptAlertWithText()
        {
            alertsForm.JsPromptButton.Click();
            var text = "accept alert text";
            BrowserManager.Browser.HandleAlert(AlertAction.Accept, text);
            Assert.AreEqual($"You entered: {text}", alertsForm.ResultLabel.GetText());
        }

        [Test]
        public void Should_BePossibleTo_DeclinePromptAlertWithText()
        {
            alertsForm.JsPromptButton.Click();
            BrowserManager.Browser.HandleAlert(AlertAction.Decline, "decline alert text");
            Assert.AreEqual("You entered: null", alertsForm.ResultLabel.GetText());
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
