using Aquality.Selenium.Core.Localization;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aquality.Selenium.Browsers
{
    public class BrowserTabNavigation : IBrowserTabNavigation
    {
        private readonly WebDriver driver;

        protected internal BrowserTabNavigation(WebDriver driver, ILocalizedLogger logger)
        {
            this.driver = driver;
            Logger = logger;
        }

        private ILocalizedLogger Logger { get; }

        public string CurrentTabHandle
        {
            get
            {
                Logger.Info("loc.browser.get.tab.handle");
                return driver.CurrentWindowHandle;
            }
        }

        public IList<string> TabHandles
        {
            get
            {
                Logger.Info("loc.browser.get.tab.handles");
                return driver.WindowHandles;
            }
        }

        public void CloseTab()
        {
            Logger.Info("loc.browser.tab.close");
            driver.Close();
        }

        public void OpenNewTab(bool switchToNew = true)
        {
            Logger.Info("loc.browser.tab.open.new");
            var currentHandle = switchToNew ? null : CurrentTabHandle;
            driver.SwitchTo().NewWindow(WindowType.Tab);
            if (!switchToNew)
            {
                CloseAndSwitch(currentHandle, closeCurrent: false);
            }
        }

        public void OpenNewTabViaJs(bool switchToNew = true)
        {
            Logger.Info("loc.browser.tab.open.new");
            driver.ExecuteScript(JavaScript.OpenNewTab.GetScript());
            if (switchToNew)
            {
                SwitchToLastTab();
            }
        }

        public void OpenInNewTab(string url)
        {
            OpenNewTab(switchToNew: true);
            driver.Navigate().GoToUrl(url);
        }

        public void OpenInNewTab(Uri url)
        {
            OpenNewTab(switchToNew: true);
            driver.Navigate().GoToUrl(url);
        }

        public void OpenInNewTabViaJs(string url)
        {
            driver.ExecuteScript(JavaScript.OpenInNewTab.GetScript(), url);
        }

        public void SwitchToLastTab(bool closeCurrent = false)
        {
            Logger.Info("loc.browser.switch.to.new.tab");
            CloseAndSwitch(TabHandles.Last(), closeCurrent);
        }

        public void SwitchToTab(string tabHandle, bool closeCurrent = false)
        {
            Logger.Info("loc.browser.switch.to.tab.handle", tabHandle);
            CloseAndSwitch(tabHandle, closeCurrent);
        }

        public void SwitchToTab(int index, bool closeCurrent = false)
        {
            Logger.Info("loc.browser.switch.to.tab.index", index);
            var names = TabHandles;
            if (index < 0 || names.Count <= index)
            {
                throw new ArgumentOutOfRangeException(
                    $"Index of browser tab '{index}' you provided is out of range {0}..{names.Count}");
            }

            var newTab = names.ElementAt(index);
            CloseAndSwitch(newTab, closeCurrent);
        }

        private void CloseAndSwitch(string name, bool closeCurrent)
        {
            if (closeCurrent)
            {
                CloseTab();
            }

            driver.SwitchTo().Window(name);
        }
    }
}
