using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using NUnit.Framework;
using System;

namespace Aquality.Selenium.Tests.Unit
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal class BrowserManagerTests
    {
        [Test]
        public void Should_BeAbleGetBrowser()
        {
            Assert.DoesNotThrow(() => AqualityServices.Browser.WaitForPageToLoad());
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
