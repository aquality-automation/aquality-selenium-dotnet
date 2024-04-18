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
            Assert.That(AqualityServices.IsBrowserStarted, Is.False, "Browser is not started");
        }

        [Test]
        public void Should_BeAbleCheck_IsBrowserStarted()
        {
            AqualityServices.Browser.WaitForPageToLoad();
            Assert.That(AqualityServices.IsBrowserStarted, "Browser is started");
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

        [TestCase(null)]
        [TestCase("enable-automation")]
        [TestCase("a")]
        public void Should_BeAbleGetBrowser_WithExcludedArguments(string excludedArguments)
        {
            var currentBrowser = AqualityServices.Get<IBrowserProfile>().BrowserName;
            Environment.SetEnvironmentVariable($"driverSettings.{currentBrowser.ToString().ToLowerInvariant()}.excludedArguments", excludedArguments);
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
            Assert.That(browserName, Is.EqualTo(AqualityServices.Browser.BrowserName));
        }

        [TearDown]
        public void CleanUp()
        {
            AqualityServices.Browser.Quit();
        }
    }
}
