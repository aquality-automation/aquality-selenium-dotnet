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

        protected virtual IBrowserWindowNavigation Tabs => AqualityServices.Browser.Tabs();

        [SetUp]
        public void Before()
        {
            AqualityServices.Browser.GoTo(WelcomeForm.Url);
        }

        [Test]
        public void Should_BePossibleTo_OpenUrlInNew()
        {
            var url = new WelcomeForm().Url;
            var browser = AqualityServices.Browser;
            Tabs.OpenInNew(url);
            Assert.That(Tabs.Handles.Count, Is.EqualTo(2));
            Assert.That(url, Is.EqualTo(browser.Driver.Url));
        }

        [Test]
        public void Should_BePossibleTo_OpenUrlInNew_ViaJs()
        {
            var url = new WelcomeForm().Url;
            var browser = AqualityServices.Browser;
            Tabs.OpenInNewViaJs(url);
            Assert.That(Tabs.Handles.Count, Is.EqualTo(2));
            Assert.That(url, Is.EqualTo(browser.Driver.Url));
        }

        [Test]
        public void Should_BePossibleTo_OpenUriInNew()
        {
            var url = new Uri(new WelcomeForm().Url);
            var browser = AqualityServices.Browser;
            Tabs.OpenInNew(url);
            Assert.That(Tabs.Handles.Count, Is.EqualTo(2));
            Assert.That(url, Is.EqualTo(new Uri(browser.Driver.Url)));
        }
        
        [Test]
        public void Should_BePossibleTo_HandleTab()
        {
            var tabHandle = Tabs.CurrentHandle;
            Assert.That(tabHandle, Is.Not.Empty, "Tab name should not be empty");
        }

        [Test]
        public void Should_BePossibleTo_GetTabHandles()
        {
            var tabHandles = Tabs.Handles;
            Assert.That(tabHandles.Count, Is.EqualTo(1), "Tab number should be correct");
            Assert.That(tabHandles.First(), Is.Not.Empty, "Tab handle should not be empty");
        }

        [Test]
        public void Should_BePossibleTo_OpenNew()
        {
            var tabHandle = Tabs.CurrentHandle;

            Tabs.OpenNew();
            var newTabHandle = Tabs.CurrentHandle;
            Assert.That(Tabs.Handles.Count, Is.EqualTo(2), "New tab should be opened");
            Assert.That(newTabHandle, Is.Not.EqualTo(tabHandle), "Browser should be switched to new tab");

            Tabs.OpenNew(false);
            Assert.That(Tabs.Handles.Count, Is.EqualTo(3), "New tab should be opened");
            Assert.That(Tabs.CurrentHandle, Is.EqualTo(newTabHandle), "Browser should not be switched to new tab");
        }

        [Test]
        public void Should_BePossibleTo_OpenNew_ViaJs()
        {
            var tabHandle = Tabs.CurrentHandle;

            Tabs.OpenNewViaJs();
            var newTabHandle = Tabs.CurrentHandle;
            Assert.That(Tabs.Handles.Count, Is.EqualTo(2), "New tab should be opened");
            Assert.That(newTabHandle, Is.Not.EqualTo(tabHandle), "Browser should be switched to new tab");

            Tabs.OpenNewViaJs(false);
            Assert.That(Tabs.Handles.Count, Is.EqualTo(3), "New tab should be opened");
            Assert.That(Tabs.CurrentHandle, Is.EqualTo(newTabHandle), "Browser should not be switched to new tab");
        }

        [Test]
        public void Should_BePossibleTo_CloseTab()
        {
            WelcomeForm.ClickElementalSelenium();
            Assert.That(Tabs.Handles.Count, Is.EqualTo(2), "New tab should be opened");
            Tabs.Close();
            Assert.That(Tabs.Handles.Count, Is.EqualTo(1), "New tab should be closed");
        }

        [Test]
        public void Should_BePossibleTo_SwitchToNew()
        {
            CheckSwitchingBy(2, () =>
            {
                Tabs.SwitchToLast();
            });
        }

        [Test]
        public void Should_BePossibleTo_SwitchToNew_AndClose()
        {
            CheckSwitchingBy(1, () =>
            {
                Tabs.SwitchToLast(true);
            });
        }

        [Test]
        public void Should_BePossibleTo_SwitchToNewByHandle()
        {
            CheckSwitchingBy(3, () =>
            {
                var tabHandle = Tabs.Handles.Last();
                Tabs.OpenNew(false);
                Tabs.SwitchTo(tabHandle);
            });
        }

        [Test]
        public void Should_BePossibleTo_SwitchToNewByHandle_AndClose()
        {
            CheckSwitchingBy(2, () =>
            {
                var tabHandle = Tabs.Handles.Last();
                Tabs.OpenNew(false);
                Tabs.SwitchTo(tabHandle, true);
            });
        }

        [Test, Category(RetriesGroup), Retry(RetriesCount)]
        public void Should_BePossibleTo_SwitchToNewByIndex()
        {
            CheckSwitchingBy(3, () =>
            {
                Tabs.OpenNew(false);
                Tabs.SwitchTo(1);
            });
        }

        [Test]
        public void Should_BePossibleTo_SwitchToNewByIndex_AndClose()
        {
            CheckSwitchingBy(2, () =>
            {
                Tabs.OpenNew(false);
                Tabs.SwitchTo(1, true);
            });
        }

        [Test]
        public void Should_BeThrow_IfSwitchToNew_ByIncorrectIndex()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { Tabs.SwitchTo(10, true); });
        }

        private void CheckSwitchingBy(int expectedTabCount, Action switchMethod)
        {
            var tabHandle = Tabs.CurrentHandle;
            Assert.That(tabHandle, Is.Not.Empty);
            WelcomeForm.ClickElementalSelenium();
            var newTabHandle = Tabs.Handles.Last();
            switchMethod.Invoke();
            Assert.That(Tabs.CurrentHandle, Is.EqualTo(newTabHandle), "Browser should be switched to correct tab");
            Assert.That(Tabs.Handles.Count, Is.EqualTo(expectedTabCount), "Number of tabs should be correct");
        }
    }
}
