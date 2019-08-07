using Aquality.Selenium.Browsers;
using NUnit.Framework;
using System;

namespace Aquality.Selenium.Tests.Unit.JavaScripts
{
    public class BrowserManagerTests
    {
        [Test]
        public void Should_BeAbleGetBrowser()
        {
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
