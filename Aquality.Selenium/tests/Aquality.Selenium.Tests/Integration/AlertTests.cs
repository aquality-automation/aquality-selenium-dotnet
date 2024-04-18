using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration
{
    internal class AlertTests : UITest
    {
        private readonly JavaScriptAlertsForm alertsForm = new();

        [SetUp]
        public void BeforeTest()
        {
            alertsForm.Open();
        }

        [Test]
        public void Should_BePossibleTo_AcceptAlert()
        {
            alertsForm.JsAlertButton.Click();
            AqualityServices.Browser.HandleAlert(AlertAction.Accept);
            Assert.That(alertsForm.ResultLabel.GetText(), Is.EqualTo("You successfully clicked an alert"));
        }

        [Test]
        public void Should_BePossibleTo_AcceptConfirmationAlert()
        {
            alertsForm.JsConfirmButton.Click();
            AqualityServices.Browser.HandleAlert(AlertAction.Accept);
            Assert.That(alertsForm.ResultLabel.GetText(), Is.EqualTo("You clicked: Ok"));
        }

        [Test]
        public void Should_BePossibleTo_AcceptConfirmationAlert_InWaitFor()
        {
            alertsForm.JsConfirmButton.Click();
            AqualityServices.ConditionalWait.WaitFor(driver =>
            {
                try
                {
                    AqualityServices.Logger.Debug($"Current url: {driver.Url}");
                    return false;
                }
                catch (UnhandledAlertException e)
                {
                    AqualityServices.Logger.Debug($"Alert appeared: {e.Message}");
                    AqualityServices.Browser.HandleAlert(AlertAction.Accept);
                    return true;
                }
                
            });
            Assert.That(alertsForm.ResultLabel.GetText(), Is.EqualTo("You clicked: Ok"));
        }

        [Test]
        public void Should_BePossibleTo_DeclineConfirmationAlert()
        {
            alertsForm.JsConfirmButton.Click();
            AqualityServices.Browser.HandleAlert(AlertAction.Decline);
            Assert.That(alertsForm.ResultLabel.GetText(), Is.EqualTo("You clicked: Cancel"));
        }

        [Test]
        public void Should_BePossibleTo_AcceptPromptAlertWithText()
        {
            alertsForm.JsPromptButton.Click();
            var text = "accept alert text";
            AqualityServices.Browser.HandleAlert(AlertAction.Accept, text);
            Assert.That(alertsForm.ResultLabel.GetText(), Is.EqualTo($"You entered: {text}"));
        }

        [Test]
        public void Should_BePossibleTo_DeclinePromptAlertWithText()
        {
            alertsForm.JsPromptButton.Click();
            AqualityServices.Browser.HandleAlert(AlertAction.Decline, "decline alert text");
            Assert.That(alertsForm.ResultLabel.GetText(), Is.EqualTo("You entered: null"));
        }

        [Test]
        public void Should_Throw_NoAlertPresentExceptionIfNoAlertPresent()
        {
            Assert.Throws<NoAlertPresentException>(() => AqualityServices.Browser.HandleAlert(AlertAction.Decline));
        }

        [Test]
        public void Should_Throw_NoAlertPresentExceptionIfNoPromptAlertPresent()
        {
            Assert.Throws<NoAlertPresentException>(() => AqualityServices.Browser.HandleAlert(AlertAction.Decline, "Hello"));
        }
    }
}
