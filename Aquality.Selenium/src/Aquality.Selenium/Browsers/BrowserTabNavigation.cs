﻿using Aquality.Selenium.Core.Localization;
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

        public void CloseTab()
        {
            Logger.Info("loc.browser.tab.close");
            driver.Close();
        }

        public string GetTabName()
        {
            Logger.Info("loc.browser.get.tab.name");
            return driver.CurrentWindowHandle;
        }

        public IList<string> GetTabNames()
        {
            Logger.Info("loc.browser.get.tab.names");
            return driver.WindowHandles;
        }

        public void OpenNewTab(bool switchToNew = false)
        {
            Logger.Info("loc.browser.tab.open.new");
            AqualityServices.Browser.ExecuteScript(JavaScript.OpenNewTab);
            if (switchToNew)
            {
                SwitchToNewTab();
            }
        }

        public void SwitchToNewTab(bool closeCurrent = false)
        {
            Logger.Info("loc.browser.switch.to.new.tab");
            var newTab = GetTabNames().Last();
            CloseAndSwitch(newTab, closeCurrent);
        }

        public void SwitchToTab(string name, bool closeCurrent = false)
        {
            Logger.Info("loc.browser.switch.to.tab.name", name);
            CloseAndSwitch(name, closeCurrent);
        }

        public void SwitchToTab(int index, bool closeCurrent = false)
        {
            Logger.Info("loc.browser.switch.to.tab.index", index);
            var names = GetTabNames();
            if (index >= names.Count || index < 0)
            {
                throw new IndexOutOfRangeException($"Index of browser tab '{index}' you provided is out of range {0}..{names.Count}");
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
