using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using NUnit.Framework;
using System;

namespace Aquality.Selenium.Tests.Unit
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal class AqualityServicesTests
    {
        [Test]
        public void Should_BeAbleGetBrowser()
        {
            Assert.DoesNotThrow(() => AqualityServices.Browser.WaitForPageToLoad());
        }

        [Test]
        public void Should_BeAbleCheck_IsBrowserNotStarted()
        {
            Assert.IsFalse(AqualityServices.IsBrowserStarted, "Browser is not started");
        }

        [Test]
        public void Should_BeAbleCheck_IsBrowserStarted()
        {
            AqualityServices.Browser.WaitForPageToLoad();
            Assert.IsTrue(AqualityServices.IsBrowserStarted, "Browser is started");
        }

        [Test]
        public void Should_BeAbleToGet_Logger()
        {
            Assert.DoesNotThrow(() => AqualityServices.Logger.Info("message"), "Logger should not be null");
        }

        [Test]
        public void Should_BeAbleToGet_ConditionalWait()
        {
            Assert.DoesNotThrow(() => AqualityServices.ConditionalWait.WaitForTrue(() => true), "ConditionalWait should not be null");
        }

        [Test]
        public void Should_BeAbleToGet_LocalizedLogger()
        {
            Assert.DoesNotThrow(() => AqualityServices.LocalizedLogger.Info("test"), "LocalizedLogger should not be null");
        }

        [Test]
        public void Should_BeAbleToGet_ServiceProvider()
        {
            Assert.DoesNotThrow(() => AqualityServices.Get<IServiceProvider>(), "ServiceProvider should not be null");
        }

        [TestCase(null)]
        [TestCase("--headless, --disable-infobars")]
        [TestCase("a")]
        public void Should_BeAbleGetBrowser_WithStartArguments(string startArguments)
        {
            var currentBrowser = AqualityServices.Get<IBrowserProfile>().BrowserName;
            Environment.SetEnvironmentVariable("isRemote", "false");
            Environment.SetEnvironmentVariable($"driverSettings.{currentBrowser.ToString().ToLowerInvariant()}.startArguments", startArguments);
            Assert.DoesNotThrow(() => AqualityServices.Browser.WaitForPageToLoad());
        }

        [Ignore("Not all browsers are supported in Azure CICD pipeline")]
        [TestCase(BrowserName.IExplorer)]
        [TestCase(BrowserName.Firefox)]
        [TestCase(BrowserName.Chrome)]
        [TestCase(BrowserName.Edge)]
        public void Should_BeAbleToCreateLocalBrowser(BrowserName browserName)
        {
            Environment.SetEnvironmentVariable("browserName", browserName.ToString());
            Environment.SetEnvironmentVariable("isRemote", "false");
            Assert.DoesNotThrow(() => AqualityServices.Browser.WaitForPageToLoad());
            Assert.AreEqual(AqualityServices.Browser.BrowserName, browserName);
        }

        [TearDown]
        public void CleanUp()
        {
            AqualityServices.Browser.Quit();
        }
    }
}
