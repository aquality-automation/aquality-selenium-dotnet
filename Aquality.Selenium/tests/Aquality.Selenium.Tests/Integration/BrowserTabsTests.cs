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
        public void Should_BePossibleTo_GetTabName()
        {
            var browser = AqualityServices.Browser;
            var tabName = browser.TabNavigation().GetTabName();
            Assert.IsNotEmpty(tabName, "Tab name should not be empty");
        }

        [Test]
        public void Should_BePossibleTo_GetTabNames()
        {
            var browser = AqualityServices.Browser;
            var tabNames = browser.TabNavigation().GetTabNames();
            Assert.AreEqual(1, tabNames.Count, "Tab number should be correct");
            Assert.IsNotEmpty(tabNames.First(), "Tab name should not be empty");
        }

        [Test]
        public void Should_BePossibleTo_OpenNewTab()
        {
            var browser = AqualityServices.Browser;
            var tabName = browser.TabNavigation().GetTabName();

            browser.TabNavigation().OpenNewTab();
            var newTabName = browser.TabNavigation().GetTabName();
            Assert.AreEqual(2, browser.TabNavigation().GetTabNames().Count, "New tab should be opened");
            Assert.AreNotEqual(tabName, newTabName, "Browser should be switched to new tab");

            browser.TabNavigation().OpenNewTab(false);
            Assert.AreEqual(3, browser.TabNavigation().GetTabNames().Count, "New tab should be opened");
            Assert.AreEqual(newTabName, browser.TabNavigation().GetTabName(), "Browser should not be switched to new tab");
        }

        [Test]
        public void Should_BePossibleTo_CloseTab()
        {
            var browser = AqualityServices.Browser;
            WelcomeForm.ClickElementalSelenium();
            Assert.AreEqual(2, browser.TabNavigation().GetTabNames().Count, "New tab should be opened");
            browser.TabNavigation().CloseTab();
            Assert.AreEqual(1, browser.TabNavigation().GetTabNames().Count, "New tab should be closed");
        }

        [Test]
        public void Should_BePossibleTo_SwitchToNewTab()
        {
            CheckSwitchingBy(2, () =>
            {
                AqualityServices.Browser.TabNavigation().SwitchToNewTab();
            });
        }

        [Test]
        public void Should_BePossibleTo_SwitchToNewTab_AndClose()
        {
            CheckSwitchingBy(1, () =>
            {
                AqualityServices.Browser.TabNavigation().SwitchToNewTab(true);
            });
        }

        [Test]
        public void Should_BePossibleTo_SwitchToNewTabByName()
        {
            CheckSwitchingBy(3, () =>
            {
                var browser = AqualityServices.Browser;
                var newTab = browser.TabNavigation().GetTabNames().Last();
                browser.TabNavigation().OpenNewTab(false);
                browser.TabNavigation().SwitchToTab(newTab);
            });
        }

        [Test]
        public void Should_BePossibleTo_SwitchToNewTabByName_AndClose()
        {
            CheckSwitchingBy(2, () =>
            {
                var browser = AqualityServices.Browser;
                var newTab = browser.TabNavigation().GetTabNames().Last();
                browser.TabNavigation().OpenNewTab(false);
                browser.TabNavigation().SwitchToTab(newTab, true);
            });
        }

        [Test]
        public void Should_BePossibleTo_SwitchToNewTabByIndex()
        {
            CheckSwitchingBy(3, () =>
            {
                AqualityServices.Browser.TabNavigation().OpenNewTab(false);
                AqualityServices.Browser.TabNavigation().SwitchToTab(1);
            });
        }

        [Test]
        public void Should_BePossibleTo_SwitchToNewTabByIndex_AndClose()
        {
            CheckSwitchingBy(2, () =>
            {
                AqualityServices.Browser.TabNavigation().OpenNewTab(false);
                AqualityServices.Browser.TabNavigation().SwitchToTab(1, true);
            });
        }

        [Test]
        public void Should_BeThrow_IfSwitchToNewTab_ByIncorrectIndex()
        {
            Assert.Throws(typeof(IndexOutOfRangeException), () => { AqualityServices.Browser.TabNavigation().SwitchToTab(10, true); });
        }

        private void CheckSwitchingBy(int expectedTabCount, Action switchMethod)
        {
            var browser = AqualityServices.Browser;
            var tabName = browser.TabNavigation().GetTabName();
            WelcomeForm.ClickElementalSelenium();
            var newTab = browser.TabNavigation().GetTabNames().Last();
            switchMethod.Invoke();
            Assert.AreEqual(newTab, browser.TabNavigation().GetTabName(), "Browser should be switched by name to correct tab");
            Assert.AreEqual(expectedTabCount, browser.TabNavigation().GetTabNames().Count, "Number of tabs should be correct");
        }

        private void CheckSwitching(int expectedTabCount, Action switchMethod)
        {
            var browser = AqualityServices.Browser;
            var tabName = browser.TabNavigation().GetTabName();
            WelcomeForm.ClickElementalSelenium();
            switchMethod.Invoke();
            var newTab = browser.TabNavigation().GetTabName();
            Assert.AreNotEqual(tabName, newTab, "Browser should be switched to new tab");
            Assert.AreEqual(expectedTabCount, browser.TabNavigation().GetTabNames().Count, "Number of tabs should be correct");
        }
    }
}
