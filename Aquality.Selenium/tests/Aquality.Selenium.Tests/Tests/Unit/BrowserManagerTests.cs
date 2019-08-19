using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using NUnit.Framework;
using System;

namespace Aquality.Selenium.Tests.Tests.Unit
{
    internal class BrowserManagerTests
    {
        [Test]
        public void Should_BeAbleGetBrowser()
        {
            Assert.DoesNotThrow(() => BrowserManager.Browser.WaitForPageToLoad());
        }

        [TestCase(null)]
        [TestCase("--headless, --disable-infobars")]
        [TestCase("a")]
        public void Should_BeAbleGetBrowser_WithStartArguments(string startArguments)
        {
            var currentBrowser = Configuration.Instance.BrowserProfile.BrowserName;
            Environment.SetEnvironmentVariable("isRemote", "false");
            Environment.SetEnvironmentVariable($"driverSettings.{currentBrowser.ToString().ToLowerInvariant()}.startArguments", startArguments);
            Assert.DoesNotThrow(() => BrowserManager.Browser.WaitForPageToLoad());
        }

        [TestCase(BrowserName.IExplorer)]
        [TestCase(BrowserName.Firefox)]
        [TestCase(BrowserName.Chrome)]
        [TestCase(BrowserName.Edge)]
        public void Should_BeAbleToCreateLocalBrowser(BrowserName browserName)
        {
            Environment.SetEnvironmentVariable("browserName", browserName.ToString());
            Environment.SetEnvironmentVariable("isRemote", "false");
            Assert.DoesNotThrow(() => BrowserManager.Browser.WaitForPageToLoad());
            Assert.AreEqual(BrowserManager.Browser.BrowserName, browserName);
        }

        [TearDown]
        public void CleanUp()
        {
            BrowserManager.Browser.Driver.Quit();
        }
    }
}
