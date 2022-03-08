using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using OpenQA.Selenium;
using System.IO;
using System.Linq;

namespace Aquality.Selenium.Tests.Integration
{
    internal class NetworkHandlingTests : UITest
    {
        private const string LogPath = "../../../Log/log.log";

        [Test]
        public void Should_BePossibleTo_SetBasicAuthentication()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            var basicAuthForm = new BasicAuthForm();
            Assert.DoesNotThrowAsync(() => AqualityServices.Browser.RegisterBasicAuthenticationAndStartMonitoring(BasicAuthForm.Domain, BasicAuthForm.User, BasicAuthForm.Password),
                "Should be possible to set basic authentication async");
            basicAuthForm.Open();
            Assert.IsTrue(basicAuthForm.IsCongratulationsPresent, "Basic authentication should work");
        }

        [Test]
        public void Should_BePossibleTo_ClearBasicAuthentication()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            var basicAuthForm = new BasicAuthForm();
            Assert.DoesNotThrowAsync(() => AqualityServices.Browser.RegisterBasicAuthenticationAndStartMonitoring(BasicAuthForm.Domain, BasicAuthForm.User, BasicAuthForm.Password),
                "Should be possible to set basic authentication async");
            AqualityServices.Browser.Network.ClearAuthenticationHandlers();
            basicAuthForm.Open();            
            Assert.IsFalse(basicAuthForm.IsCongratulationsPresent, "Basic authentication should not work after the handler is cleared");
            Assert.DoesNotThrowAsync(() => AqualityServices.Browser.Network.StopMonitoring(), "Should be possible to stop network monitoring");
        }

        [Test]
        public void Should_BePossibleTo_AddAndClearRequestHandler()
        {
            const string somePhrase = "delicious cheese!";
            AqualityServices.Browser.Network.AddRequestHandler(
                new NetworkRequestHandler 
                { 
                    RequestMatcher = req => true,
                    ResponseSupplier = req => new HttpResponseData { Body = somePhrase, StatusCode = 200 }
                });
            Assert.DoesNotThrowAsync(() => AqualityServices.Browser.Network.StartMonitoring());
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            StringAssert.Contains(somePhrase, AqualityServices.Browser.Driver.PageSource, "Request should be intercepted");
            AqualityServices.Browser.Network.ClearRequestHandlers();
            welcomeForm.Open();
            StringAssert.DoesNotContain(somePhrase, AqualityServices.Browser.Driver.PageSource, "Request should not be intercepted");
        }

        [Test]
        public void Should_BePossibleTo_AddAndClearResponseHandler()
        {
            const string somePhrase = "delicious cheese!";
            AqualityServices.Browser.Network.AddResponseHandler(
                new NetworkResponseHandler 
                { 
                    ResponseMatcher = res => true,
                    ResponseTransformer = res => new HttpResponseData { Body = somePhrase, StatusCode = 200 }
                });
            Assert.DoesNotThrowAsync(() => AqualityServices.Browser.Network.StartMonitoring());
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            StringAssert.Contains(somePhrase, AqualityServices.Browser.Driver.PageSource, "Response should be intercepted");
            AqualityServices.Browser.Network.ClearResponseHandlers();
            welcomeForm.Open();
            StringAssert.DoesNotContain(somePhrase, AqualityServices.Browser.Driver.PageSource, "Response should not be intercepted");
        }

        [Test]
        public void Should_BePossibleTo_SubscribeToRequestSentEvent_AndUnsubscribeFromIt()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            var counter = 0;
            void eventHandler(object sender, NetworkRequestSentEventArgs args) => counter++;
            AqualityServices.Browser.Network.NetworkRequestSent += eventHandler;
            Assert.DoesNotThrowAsync(() => AqualityServices.Browser.Network.StartMonitoring());
            welcomeForm.Open();
            Assert.That(counter, Is.GreaterThan(0), "Should be possible to subscribe to Request Sent event");
            var oldValue = counter;
            AqualityServices.Browser.Network.NetworkRequestSent -= eventHandler;
            welcomeForm.Open();
            Assert.AreEqual(oldValue, counter, "Should be possible to unsubscribe from Request Sent event");
        }

        [Test]
        public void Should_BePossibleTo_SubscribeToResponseReceivedEvent_AndUnsubscribeFromIt()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            var counter = 0;
            void eventHandler(object sender, NetworkResponseReceivedEventArgs args) => counter++;
            AqualityServices.Browser.Network.NetworkResponseReceived += eventHandler;
            Assert.DoesNotThrowAsync(() => AqualityServices.Browser.Network.StartMonitoring());
            welcomeForm.Open();
            Assert.That(counter, Is.GreaterThan(0), "Should be possible to subscribe to Response Received event");
            var oldValue = counter;
            AqualityServices.Browser.Network.NetworkResponseReceived -= eventHandler;
            welcomeForm.Open();
            Assert.AreEqual(oldValue, counter, "Should be possible to unsubscribe from Response Received event");
        }

        [Test]
        [Parallelizable(ParallelScope.None)]
        public void Should_BePossibleTo_EnableHttpExchangeLogging_AndDisableIt()
        {
            var someForm = new DropdownForm();
            someForm.Open();
            var logMessage1 = File.ReadAllLines(LogPath).LastOrDefault();
            Assert.IsFalse(string.IsNullOrEmpty(logMessage1), "Some message should appear in log file and should not be empty");
            Assert.DoesNotThrowAsync(() => AqualityServices.Browser.EnableHttpExchangeLoggingAndStartMonitoring(), "Should be possible to enable HTTP exchange logging");
            AqualityServices.Browser.Driver.Navigate().Refresh();
            var logMessage2 = File.ReadAllLines(LogPath).LastOrDefault();
            Assert.IsFalse(string.IsNullOrEmpty(logMessage2), "Some message should appear in log file and should not be empty");
            Assert.AreNotEqual(logMessage1, logMessage2, "HTTP logging message should be in file, although no Aquality-actions performed");
        }
    }
}
