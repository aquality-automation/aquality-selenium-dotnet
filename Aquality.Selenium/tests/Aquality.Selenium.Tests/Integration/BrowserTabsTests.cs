﻿using System;
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
        public void Should_BePossibleTo_OpenUrlInNewTab()
        {
            var url = new WelcomeForm().Url;
            var browser = AqualityServices.Browser;
            browser.Tabs().OpenInNewTab(url);
            browser.Tabs().SwitchToLastTab();
            Assert.AreEqual(2, browser.Tabs().TabHandles.Count);
            Assert.AreEqual(browser.Driver.Url, url);
        }
        
        [Test]
        public void Should_BePossibleTo_HandleTab()
        {
            var browser = AqualityServices.Browser;
            var tabHandle = browser.Tabs().CurrentTabHandle;
            Assert.IsNotEmpty(tabHandle, "Tab name should not be empty");
        }

        [Test]
        public void Should_BePossibleTo_GetTabHandles()
        {
            var browser = AqualityServices.Browser;
            var tabHandles = browser.Tabs().TabHandles;
            Assert.AreEqual(1, tabHandles.Count, "Tab number should be correct");
            Assert.IsNotEmpty(tabHandles.First(), "Tab handle should not be empty");
        }

        [Test]
        public void Should_BePossibleTo_OpenNewTab()
        {
            var browser = AqualityServices.Browser;
            var tabHandle = browser.Tabs().CurrentTabHandle;

            browser.Tabs().OpenNewTab();
            var newTabHandle = browser.Tabs().CurrentTabHandle;
            Assert.AreEqual(2, browser.Tabs().TabHandles.Count, "New tab should be opened");
            Assert.AreNotEqual(tabHandle, newTabHandle, "Browser should be switched to new tab");

            browser.Tabs().OpenNewTab(false);
            Assert.AreEqual(3, browser.Tabs().TabHandles.Count, "New tab should be opened");
            Assert.AreEqual(newTabHandle, browser.Tabs().CurrentTabHandle, "Browser should not be switched to new tab");
        }

        [Test]
        public void Should_BePossibleTo_CloseTab()
        {
            var browser = AqualityServices.Browser;
            WelcomeForm.ClickElementalSelenium();
            Assert.AreEqual(2, browser.Tabs().TabHandles.Count, "New tab should be opened");
            browser.Tabs().CloseTab();
            Assert.AreEqual(1, browser.Tabs().TabHandles.Count, "New tab should be closed");
        }

        [Test]
        public void Should_BePossibleTo_SwitchToNewTab()
        {
            CheckSwitchingBy(2, () =>
            {
                AqualityServices.Browser.Tabs().SwitchToLastTab();
            });
        }

        [Test]
        public void Should_BePossibleTo_SwitchToNewTab_AndClose()
        {
            CheckSwitchingBy(1, () =>
            {
                AqualityServices.Browser.Tabs().SwitchToLastTab(true);
            });
        }

        [Test]
        public void Should_BePossibleTo_SwitchToNewTabByHandle()
        {
            CheckSwitchingBy(3, () =>
            {
                var browser = AqualityServices.Browser;
                var tabHandle = browser.Tabs().TabHandles.Last();
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
                var tabHandle = browser.Tabs().TabHandles.Last();
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
            Assert.Throws<ArgumentOutOfRangeException>(() => { AqualityServices.Browser.Tabs().SwitchToTab(10, true); });
        }

        private void CheckSwitchingBy(int expectedTabCount, Action switchMethod)
        {
            var browser = AqualityServices.Browser;
            var tabHandle = browser.Tabs().CurrentTabHandle;
            WelcomeForm.ClickElementalSelenium();
            var newTabHandle = browser.Tabs().TabHandles.Last();
            switchMethod.Invoke();
            Assert.AreEqual(newTabHandle, browser.Tabs().CurrentTabHandle, "Browser should be switched to correct tab");
            Assert.AreEqual(expectedTabCount, browser.Tabs().TabHandles.Count, "Number of tabs should be correct");
        }

        private void CheckSwitching(int expectedTabCount, Action switchMethod)
        {
            var browser = AqualityServices.Browser;
            var tabHandle = browser.Tabs().CurrentTabHandle;
            WelcomeForm.ClickElementalSelenium();
            switchMethod.Invoke();
            var newTabHandle = browser.Tabs().CurrentTabHandle;
            Assert.AreNotEqual(tabHandle, newTabHandle, "Browser should be switched to new tab");
            Assert.AreEqual(expectedTabCount, browser.Tabs().TabHandles.Count, "Number of tabs should be correct");
        }
    }
}
