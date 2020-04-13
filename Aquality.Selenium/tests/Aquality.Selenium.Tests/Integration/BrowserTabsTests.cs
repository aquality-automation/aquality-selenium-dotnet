using System;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using System.Linq;

namespace Aquality.Selenium.Tests.Integration
{
    internal class BrowserTabsTests : UITest
    {
        private readonly WelcomeForm WelcomeForm = new WelcomeForm();

        [SetUp]
        public void Before()
        {
            AqualityServices.Browser.GoTo(WelcomeForm.Url);
        }

        [Test]
        public void Should_BePossibleTo_HandleTab()
        {
            var browser = AqualityServices.Browser;
            var tabHandle = browser.Tabs().GetTabHandle();
            Assert.IsNotEmpty(tabHandle, "Tab name should not be empty");
        }

        [Test]
        public void Should_BePossibleTo_GetTabHandles()
        {
            var browser = AqualityServices.Browser;
            var tabHandles = browser.Tabs().GetTabHandles();
            Assert.AreEqual(1, tabHandles.Count, "Tab number should be correct");
            Assert.IsNotEmpty(tabHandles.First(), "Tab handle should not be empty");
        }

        [Test]
        public void Should_BePossibleTo_OpenNewTab()
        {
            var browser = AqualityServices.Browser;
            var tabHandle = browser.Tabs().GetTabHandle();

            browser.Tabs().OpenNewTab();
            var newTabHandle = browser.Tabs().GetTabHandle();
            Assert.AreEqual(2, browser.Tabs().GetTabHandles().Count, "New tab should be opened");
            Assert.AreNotEqual(tabHandle, newTabHandle, "Browser should be switched to new tab");

            browser.Tabs().OpenNewTab(false);
            Assert.AreEqual(3, browser.Tabs().GetTabHandles().Count, "New tab should be opened");
            Assert.AreEqual(newTabHandle, browser.Tabs().GetTabHandle(), "Browser should not be switched to new tab");
        }

        [Test]
        public void Should_BePossibleTo_CloseTab()
        {
            var browser = AqualityServices.Browser;
            WelcomeForm.ClickElementalSelenium();
            Assert.AreEqual(2, browser.Tabs().GetTabHandles().Count, "New tab should be opened");
            browser.Tabs().CloseTab();
            Assert.AreEqual(1, browser.Tabs().GetTabHandles().Count, "New tab should be closed");
        }

        [Test]
        public void Should_BePossibleTo_SwitchToNewTab()
        {
            CheckSwitchingBy(2, () =>
            {
                AqualityServices.Browser.Tabs().SwitchToNewTab();
            });
        }

        [Test]
        public void Should_BePossibleTo_SwitchToNewTab_AndClose()
        {
            CheckSwitchingBy(1, () =>
            {
                AqualityServices.Browser.Tabs().SwitchToNewTab(true);
            });
        }

        [Test]
        public void Should_BePossibleTo_SwitchToNewTabByHandle()
        {
            CheckSwitchingBy(3, () =>
            {
                var browser = AqualityServices.Browser;
                var tabHandle = browser.Tabs().GetTabHandles().Last();
                browser.Tabs().OpenNewTab(false);
                browser.Tabs().SwitchToTab(tabHandle);
            });
        }

        [Test]
        public void Should_BePossibleTo_SwitchToNewTabByHandle_AndClose()
        {
            CheckSwitchingBy(2, () =>
            {
                var browser = AqualityServices.Browser;
                var tabHandle = browser.Tabs().GetTabHandles().Last();
                browser.Tabs().OpenNewTab(false);
                browser.Tabs().SwitchToTab(tabHandle, true);
            });
        }

        [Test]
        public void Should_BePossibleTo_SwitchToNewTabByIndex()
        {
            CheckSwitchingBy(3, () =>
            {
                AqualityServices.Browser.Tabs().OpenNewTab(false);
                AqualityServices.Browser.Tabs().SwitchToTab(1);
            });
        }

        [Test]
        public void Should_BePossibleTo_SwitchToNewTabByIndex_AndClose()
        {
            CheckSwitchingBy(2, () =>
            {
                AqualityServices.Browser.Tabs().OpenNewTab(false);
                AqualityServices.Browser.Tabs().SwitchToTab(1, true);
            });
        }

        [Test]
        public void Should_BeThrow_IfSwitchToNewTab_ByIncorrectIndex()
        {
            Assert.Throws(typeof(IndexOutOfRangeException), () => { AqualityServices.Browser.Tabs().SwitchToTab(10, true); });
        }

        private void CheckSwitchingBy(int expectedTabCount, Action switchMethod)
        {
            var browser = AqualityServices.Browser;
            var tabHandle = browser.Tabs().GetTabHandle();
            WelcomeForm.ClickElementalSelenium();
            var newTabHandle = browser.Tabs().GetTabHandles().Last();
            switchMethod.Invoke();
            Assert.AreEqual(newTabHandle, browser.Tabs().GetTabHandle(), "Browser should be switched to correct tab");
            Assert.AreEqual(expectedTabCount, browser.Tabs().GetTabHandles().Count, "Number of tabs should be correct");
        }

        private void CheckSwitching(int expectedTabCount, Action switchMethod)
        {
            var browser = AqualityServices.Browser;
            var tabHandle = browser.Tabs().GetTabHandle();
            WelcomeForm.ClickElementalSelenium();
            switchMethod.Invoke();
            var newTabHandle = browser.Tabs().GetTabHandle();
            Assert.AreNotEqual(tabHandle, newTabHandle, "Browser should be switched to new tab");
            Assert.AreEqual(expectedTabCount, browser.Tabs().GetTabHandles().Count, "Number of tabs should be correct");
        }
    }
}
