using System;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using System.Linq;

namespace Aquality.Selenium.Tests.Integration
{
    internal class BrowserTabsTests : UITest
    {
        private readonly WelcomeForm WelcomeForm = new();

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
            Assert.That(browser.Tabs().TabHandles.Count, Is.EqualTo(2));
            Assert.That(url, Is.EqualTo(browser.Driver.Url));
        }

        [Test]
        public void Should_BePossibleTo_OpenUrlInNewTab_ViaJs()
        {
            var url = new WelcomeForm().Url;
            var browser = AqualityServices.Browser;
            browser.Tabs().OpenInNewTabViaJs(url);
            Assert.That(browser.Tabs().TabHandles.Count, Is.EqualTo(2));
            Assert.That(url, Is.EqualTo(browser.Driver.Url));
        }

        [Test]
        public void Should_BePossibleTo_OpenUriInNewTab()
        {
            var url = new Uri(new WelcomeForm().Url);
            var browser = AqualityServices.Browser;
            browser.Tabs().OpenInNewTab(url);
            Assert.That(browser.Tabs().TabHandles.Count, Is.EqualTo(2));
            Assert.That(url, Is.EqualTo(new Uri(browser.Driver.Url)));
        }
        
        [Test]
        public void Should_BePossibleTo_HandleTab()
        {
            var browser = AqualityServices.Browser;
            var tabHandle = browser.Tabs().CurrentTabHandle;
            Assert.That(tabHandle, Is.Not.Empty, "Tab name should not be empty");
        }

        [Test]
        public void Should_BePossibleTo_GetTabHandles()
        {
            var browser = AqualityServices.Browser;
            var tabHandles = browser.Tabs().TabHandles;
            Assert.That(tabHandles.Count, Is.EqualTo(1), "Tab number should be correct");
            Assert.That(tabHandles.First(), Is.Not.Empty, "Tab handle should not be empty");
        }

        [Test]
        public void Should_BePossibleTo_OpenNewTab()
        {
            var browser = AqualityServices.Browser;
            var tabHandle = browser.Tabs().CurrentTabHandle;

            browser.Tabs().OpenNewTab();
            var newTabHandle = browser.Tabs().CurrentTabHandle;
            Assert.That(browser.Tabs().TabHandles.Count, Is.EqualTo(2), "New tab should be opened");
            Assert.That(newTabHandle, Is.Not.EqualTo(tabHandle), "Browser should be switched to new tab");

            browser.Tabs().OpenNewTab(false);
            Assert.That(browser.Tabs().TabHandles.Count, Is.EqualTo(3), "New tab should be opened");
            Assert.That(browser.Tabs().CurrentTabHandle, Is.EqualTo(newTabHandle), "Browser should not be switched to new tab");
        }

        [Test]
        public void Should_BePossibleTo_OpenNewTab_ViaJs()
        {
            var browser = AqualityServices.Browser;
            var tabHandle = browser.Tabs().CurrentTabHandle;

            browser.Tabs().OpenNewTabViaJs();
            var newTabHandle = browser.Tabs().CurrentTabHandle;
            Assert.That(browser.Tabs().TabHandles.Count, Is.EqualTo(2), "New tab should be opened");
            Assert.That(newTabHandle, Is.Not.EqualTo(tabHandle), "Browser should be switched to new tab");

            browser.Tabs().OpenNewTabViaJs(false);
            Assert.That(browser.Tabs().TabHandles.Count, Is.EqualTo(3), "New tab should be opened");
            Assert.That(browser.Tabs().CurrentTabHandle, Is.EqualTo(newTabHandle), "Browser should not be switched to new tab");
        }

        [Test]
        public void Should_BePossibleTo_CloseTab()
        {
            var browser = AqualityServices.Browser;
            WelcomeForm.ClickElementalSelenium();
            Assert.That(browser.Tabs().TabHandles.Count, Is.EqualTo(2), "New tab should be opened");
            browser.Tabs().CloseTab();
            Assert.That(browser.Tabs().TabHandles.Count, Is.EqualTo(1), "New tab should be closed");
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
            Assert.That(tabHandle, Is.Not.Empty);
            WelcomeForm.ClickElementalSelenium();
            var newTabHandle = browser.Tabs().TabHandles.Last();
            switchMethod.Invoke();
            Assert.That(browser.Tabs().CurrentTabHandle, Is.EqualTo(newTabHandle), "Browser should be switched to correct tab");
            Assert.That(browser.Tabs().TabHandles.Count, Is.EqualTo(expectedTabCount), "Number of tabs should be correct");
        }
    }
}
