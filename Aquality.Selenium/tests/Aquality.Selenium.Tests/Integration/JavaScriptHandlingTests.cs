using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Aquality.Selenium.Tests.Integration
{
    internal class JavaScriptHandlingTests : UITest
    {
        private static readonly TimeSpan NegativeConditionTimeout = TimeSpan.FromSeconds(5);
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
            Assert.DoesNotThrowAsync(async() => await JavaScriptEngine.StartEventMonitoring(), "Should be possible to start event monitoring");

            welcomeForm.Open();

            Assert.DoesNotThrowAsync(async () => await JavaScriptEngine.EnableDomMutationMonitoring(), "Should be possible to enable DOM mutation monitoring");

            welcomeForm.SubTitleLabel.JsActions.SetAttribute(attributeName, attributeValue);
            AqualityServices.ConditionalWait.WaitForTrue(() => attributeValueChanges.Count > 0, 
                message: "Some mutation events should be found, should be possible to subscribe to DOM mutation event");
            Assert.That(attributeValueChanges.Count, Is.EqualTo(1), "Exactly one change in DOM is expected");
            var record = attributeValueChanges.Single();
            Assert.That(record.AttributeName, Is.EqualTo(attributeName), "Attribute name should match to expected");
            Assert.That(record.AttributeValue, Is.EqualTo(attributeValue), "Attribute value should match to expected");

            JavaScriptEngine.DomMutated -= eventHandler;
            welcomeForm.SubTitleLabel.JsActions.SetAttribute(attributeName, attributeName);
            AqualityServices.ConditionalWait.WaitFor(() => attributeValueChanges.Count > 1, timeout: NegativeConditionTimeout);
            Assert.That(attributeValueChanges.Count, Is.EqualTo(1), "No more changes in DOM is expected, should be possible to unsubscribe from DOM mutation event");

            Assert.DoesNotThrowAsync(async () => await JavaScriptEngine.DisableDomMutationMonitoring(), "Should be possible to disable DOM mutation monitoring");
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
            Assert.That(xpath, Is.Not.Empty, "Pinned script should be possible to execute");
            var expectedValue = welcomeForm.SubTitleLabel.JsActions.GetXPath();
            Assert.That(xpath, Is.EqualTo(expectedValue), "Pinned script should return the same value");

            Assert.DoesNotThrowAsync(async () => await JavaScriptEngine.UnpinScript(pinnedScript), "Should be possible to unpin the script");
            Assert.Throws<JavaScriptException>(
                () => pinnedScript.ExecuteScript<string>(welcomeForm.SubTitleLabel), 
                "Unpinned script should not return the value");
            Assert.DoesNotThrowAsync(async () => await JavaScriptEngine.Reset(), "Should be possible to reset JavaScript monitoring");
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
            Assert.That(actualText, Is.EqualTo(text), $"Text should be '{text}' after setting value via pinned JS");

            Assert.DoesNotThrowAsync(async () => await JavaScriptEngine.UnpinScript(pinnedScript), "Should be possible to unpin the script");
            Assert.Throws<JavaScriptException>(() => pinnedScript.ExecuteScript(keyPressesForm.InputTextBox, text),  "Unpinned script should not be executed");
        }

        [Test]
        public void Should_BePossibleTo_SubscribeToJavaScriptConsoleApiCalledEvent_AndUnsubscribeFromIt()
        {
            const string consoleApiScript = "console.log('Hello world!')";
            var apiCalledMessages = new List<string>();
            void eventHandler(object sender, JavaScriptConsoleApiCalledEventArgs e) => apiCalledMessages.Add(e.MessageContent);
            JavaScriptEngine.JavaScriptConsoleApiCalled += eventHandler;
            Assert.DoesNotThrowAsync(async () => await JavaScriptEngine.StartEventMonitoring(), "Should be possible to start event monitoring");

            AqualityServices.Browser.ExecuteScript(consoleApiScript);

            var hasCountIncreased = AqualityServices.ConditionalWait.WaitFor(() => apiCalledMessages.Count > 0);
            Assert.That(hasCountIncreased, "Some JS console API events should have been recorded, should be possible to subscribe to JS Console API called event");
            
            var previousCount = apiCalledMessages.Count;
            JavaScriptEngine.JavaScriptConsoleApiCalled -= eventHandler;
            AqualityServices.Browser.ExecuteScript(consoleApiScript);
            AqualityServices.ConditionalWait.WaitFor(() => apiCalledMessages.Count > previousCount, timeout: NegativeConditionTimeout);
            Assert.That(apiCalledMessages.Count, Is.EqualTo(previousCount), "No more JS console API events should be recorded, should be possible to unsubscribe from JS Console API called event");
        }

        [Test]
        public void Should_BePossibleTo_SubscribeToJavaScriptExceptionThrownEvent_AndUnsubscribeFromIt()
        {
            var welcomeForm = new WelcomeForm();
            var errorMessages = new List<string>();
            void eventHandler(object sender, JavaScriptExceptionThrownEventArgs e) => errorMessages.Add(e.Message);
            JavaScriptEngine.JavaScriptExceptionThrown += eventHandler;
            Assert.DoesNotThrowAsync(async () => await JavaScriptEngine.StartEventMonitoring(), "Should be possible to start event monitoring");
            welcomeForm.Open();
            welcomeForm.SubTitleLabel.JsActions.SetAttribute("onclick", "throw new Error('Hello, world!')");
            welcomeForm.SubTitleLabel.Click();
            AqualityServices.ConditionalWait.WaitFor(() => errorMessages.Count > 0);
            Assert.That(errorMessages, Has.Count.GreaterThan(0), "Some JS exceptions events should have been recorded, should be possible to subscribe to JS Exceptions thrown event");
            
            var previousCount = errorMessages.Count;
            JavaScriptEngine.JavaScriptExceptionThrown -= eventHandler;
            welcomeForm.SubTitleLabel.Click();
            AqualityServices.ConditionalWait.WaitFor(() => errorMessages.Count > previousCount, timeout: NegativeConditionTimeout);
            Assert.That(errorMessages.Count, Is.EqualTo(previousCount), "No more JS exceptions should be recorded, should be possible to unsubscribe from JS Exceptions thrown event");
        }

        [Test]
        public void Should_BePossibleTo_AddInitializationScript_GetIt_ThenRemove_OrClear()
        {
            const string script = "alert('Hello world')";
            const string name = "alert";
            InitializationScript initScript = JavaScriptEngine.AddInitializationScript(name, script).Result;
            Assert.That(initScript, Is.Not.Null, "Some initialization script model should be returned");
            Assert.That(initScript.ScriptSource, Is.EqualTo(script), "Saved script source should match to expected");
            Assert.That(initScript.ScriptName, Is.EqualTo(name), "Saved script name should match to expected");

            JavaScriptEngine.StartEventMonitoring().GetAwaiter().GetResult();
            AqualityServices.Browser.Refresh();
            Assert.DoesNotThrow(() => AqualityServices.Browser.HandleAlert(AlertAction.Accept), "Alert should appear and be possible to handle");
            Assert.DoesNotThrow(() => AqualityServices.Browser.RefreshPageWithAlert(AlertAction.Accept), "Alert should appear after the refresh and be possible to handle");

            Assert.That(JavaScriptEngine.InitializationScripts, Has.Member(initScript), "Should be possible to read initialization scripts");

            JavaScriptEngine.RemoveInitializationScript(name).GetAwaiter().GetResult();
            AqualityServices.Browser.Refresh();
            Assert.Throws<NoAlertPresentException>(() => AqualityServices.Browser.HandleAlert(AlertAction.Accept), "Initialization script should not be executed after the remove");
            Assert.That(JavaScriptEngine.InitializationScripts, Is.Empty, "Should be possible to read initialization scripts after remove");

            JavaScriptEngine.AddInitializationScript(name, script).GetAwaiter().GetResult();
            Assert.DoesNotThrow(() => AqualityServices.Browser.RefreshPageWithAlert(AlertAction.Accept), "Alert should appear and be possible to handle");
            Assert.That(JavaScriptEngine.InitializationScripts, Has.One.Items, "Exactly one script should be among initialization scripts");

            JavaScriptEngine.ClearInitializationScripts().GetAwaiter().GetResult();
            Assert.Throws<NoAlertPresentException>(() => AqualityServices.Browser.RefreshPageWithAlert(AlertAction.Accept), "Initialization script should not be executed after the clear");
            Assert.That(JavaScriptEngine.InitializationScripts, Is.Empty, "Should be possible to read initialization scripts after clear");


            JavaScriptEngine.AddInitializationScript(name, script).GetAwaiter().GetResult();
            Assert.DoesNotThrow(() => AqualityServices.Browser.RefreshPageWithAlert(AlertAction.Accept), "Alert should appear and be possible to handle");
            JavaScriptEngine.ClearAll().GetAwaiter().GetResult(); 
            Assert.Throws<NoAlertPresentException>(() => AqualityServices.Browser.RefreshPageWithAlert(AlertAction.Accept), "Initialization script should not be executed after the clear all");
            Assert.That(JavaScriptEngine.InitializationScripts, Is.Empty, "Should be possible to read initialization scripts after clear all");

        }

        [Test]
        public void Should_BePossibleTo_AddScriptCallbackBinding_SubscribeAndUnsubscribe_GetIt_ThenRemove_OrClear()
        {
            const string script = "alert('Hello world')";
            const string scriptName = "alert";

            var executedBindings = new List<string>();
            void eventHandler(object sender, JavaScriptCallbackExecutedEventArgs e) => executedBindings.Add(e.BindingName);            
            JavaScriptEngine.JavaScriptCallbackExecuted += eventHandler;
            Assert.DoesNotThrowAsync(async () => await JavaScriptEngine.AddInitializationScript(scriptName, script), "Should be possible to add initialization script");            
            Assert.DoesNotThrowAsync(async () => await JavaScriptEngine.StartEventMonitoring(), "Should be possible to start event monitoring");
            Assert.DoesNotThrow(() => AqualityServices.Browser.RefreshPageWithAlert(AlertAction.Accept), "Alert should appear and be possible to handle");

            Assert.DoesNotThrowAsync(async () => await JavaScriptEngine.AddScriptCallbackBinding(scriptName), "Should be possible to add script callback binding");
            Assert.Throws<NoAlertPresentException>(() => AqualityServices.Browser.RefreshPageWithAlert(AlertAction.Accept), 
                "Callback binding should prevent from initialization script execution");
            AqualityServices.ConditionalWait.WaitForTrue(() => executedBindings.Contains(scriptName), message: "Subscription to JavaScriptCallbackExecuted event should work");
            var oldCount = executedBindings.Count;
            AqualityServices.Browser.Refresh();
            Assert.That(executedBindings, Has.Count.GreaterThan(oldCount), "Another event should be noticed");
            Assert.That(JavaScriptEngine.ScriptCallbackBindings, Has.Member(scriptName), "Should be possible to read script callback bindings");
            oldCount = executedBindings.Count;

            Assert.DoesNotThrowAsync(async () => await JavaScriptEngine.RemoveScriptCallbackBinding(scriptName), "Should be possible to remove script callback binding");
            Assert.That(JavaScriptEngine.ScriptCallbackBindings, Is.Empty, "Should be possible to read script callback bindings after remove");
            Assert.DoesNotThrowAsync(async () => await JavaScriptEngine.AddScriptCallbackBinding(scriptName), "Should be possible to add script callback binding again");
            Assert.That(JavaScriptEngine.ScriptCallbackBindings, Has.Member(scriptName), "Should be possible to read script callback bindings");
            Assert.DoesNotThrowAsync(async () => await JavaScriptEngine.ClearScriptCallbackBindings(), "Should be possible to clear script callback bindings");
            Assert.That(JavaScriptEngine.ScriptCallbackBindings, Is.Empty, "Should be possible to read script callback bindings after remove");

            JavaScriptEngine.JavaScriptCallbackExecuted -= eventHandler;
            AqualityServices.Browser.Refresh();
            Assert.That(executedBindings, Has.Count.EqualTo(oldCount), "Another event should not be noticed, should be possible to unsubscribe from JavaScriptCallbackExecuted event");
        }
    }
}
