using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Linq;

namespace Aquality.Selenium.Tests.Integration
{
    internal class JavaScriptHandlingTests : UITest
    {
        private static IJavaScriptEngine JavaScriptEngine => AqualityServices.Browser.JavaScriptEngine;

        [Test]
        public void Should_BePossibleTo_SubscribeToDomMutationEvent_AndUnsubscribeFromIt()
        {
            const string attributeName = "cheese";
            const string attributeValue = "Gouda";
            var welcomeForm = new WelcomeForm();

            var attributeValueChanges = new List<DomMutationData>();
            void eventHandler(object sender, DomMutatedEventArgs e) => attributeValueChanges.Add(e.AttributeData);
            JavaScriptEngine.DomMutated += eventHandler;
            Assert.DoesNotThrowAsync(() => JavaScriptEngine.StartEventMonitoring(), "Should be possible to start event monitoring");

            welcomeForm.Open();

            Assert.DoesNotThrowAsync(() => JavaScriptEngine.EnableDomMutationMonitoring(), "Should be possible to enable DOM mutation monitoring");
            welcomeForm.SubTitleLabel.JsActions.SetAttribute(attributeName, attributeValue);
            AqualityServices.ConditionalWait.WaitForTrue(() => attributeValueChanges.Count > 0, 
                message: "Some mutation events should be found, should be possible to subscribe to DOM mutation event");
            Assert.AreEqual(1, attributeValueChanges.Count, "Exactly one change in DOM is expected");
            var record = attributeValueChanges.Single();
            Assert.AreEqual(attributeName, record.AttributeName, "Attribute name should match to expected");
            Assert.AreEqual(attributeValue, record.AttributeValue, "Attribute value should match to expected");
            JavaScriptEngine.DomMutated -= eventHandler;
            welcomeForm.SubTitleLabel.JsActions.SetAttribute(attributeName, attributeName);
            AqualityServices.ConditionalWait.WaitFor(() => attributeValueChanges.Count > 1, timeout: AqualityServices.Get<ITimeoutConfiguration>().Script);
            Assert.AreEqual(1, attributeValueChanges.Count, "No more changes in DOM is expected, should be possible to unsubscribe from DOM mutation event");

            Assert.DoesNotThrowAsync(() => JavaScriptEngine.DisableDomMutationMonitoring(), "Should be possible to disable DOM mutation monitoring");
            Assert.DoesNotThrow(() => JavaScriptEngine.StopEventMonitoring(), "Should be possible to stop event monitoring");
        }

        [Test]
        public void Should_BePossibleTo_PinScript_AndUnpinIt()
        {
            var script = JavaScript.GetElementXPath.GetScript();
            var welcomeForm = new WelcomeForm();
            var pinnedScript = JavaScriptEngine.PinScript(script).Result;

            welcomeForm.Open();

            var xpath = pinnedScript.ExecuteScript<string>(welcomeForm.SubTitleLabel);
            Assert.IsNotEmpty(xpath, "Pinned script should be possible to execute");
            var expectedValue = welcomeForm.SubTitleLabel.JsActions.GetXPath();
            Assert.AreEqual(expectedValue, xpath, "Pinned script should return the same value");

            Assert.DoesNotThrowAsync(() => JavaScriptEngine.UnpinScript(pinnedScript), "Should be possible to unpin the script");
            Assert.Throws<JavaScriptException>(
                () => pinnedScript.ExecuteScript<string>(welcomeForm.SubTitleLabel), 
                "Unpinned script should not return the value");
        }

        [Test]
        public void Should_BePossibleTo_PinScript_WithoutReturnedValue_AndUnpinIt()
        {
            const string text = "text";
            var script = JavaScript.SetValue.GetScript();
            var pinnedScript = JavaScriptEngine.PinScript(script).Result;

            var keyPressesForm = new KeyPressesForm();
            keyPressesForm.Open();

            Assert.DoesNotThrow(() => pinnedScript.ExecuteScript(keyPressesForm.InputTextBox, text), "Should be possible to execute pinned script without return value");

            var actualText = keyPressesForm.InputTextBox.Value;
            Assert.AreEqual(text, actualText, $"Text should be '{text}' after setting value via pinned JS");

            Assert.DoesNotThrowAsync(() => JavaScriptEngine.UnpinScript(pinnedScript), "Should be possible to unpin the script");
            Assert.Throws<JavaScriptException>(() => pinnedScript.ExecuteScript(keyPressesForm.InputTextBox, text),  "Unpinned script should not be executed");
        }
    }
}
