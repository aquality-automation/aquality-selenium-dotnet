using Aquality.Selenium.Core.Localization;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aquality.Selenium.Browsers
{
    public class BrowserTabNavigation : IBrowserTabNavigation
    {
        private readonly RemoteWebDriver driver;

        internal BrowserTabNavigation(RemoteWebDriver driver)
        {
            this.driver = driver;
        }

        private ILocalizedLogger Logger => AqualityServices.LocalizedLogger;

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
            AqualityServices.Browser.ExecuteScript(JavaScript.OpenNewTab);
            if (switchToNew)
            {
                SwitchToLastTab();
            }
        }

        public void OpenInNewTab(string url)
        {
            AqualityServices.Browser.ExecuteScript(JavaScript.OpenInNewTab, url);
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
                throw new IndexOutOfRangeException(
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
