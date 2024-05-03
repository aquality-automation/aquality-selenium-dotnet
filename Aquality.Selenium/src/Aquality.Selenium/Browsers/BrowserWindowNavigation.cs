using Aquality.Selenium.Core.Localization;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aquality.Selenium.Browsers
{
    public class BrowserWindowNavigation : IBrowserTabNavigation, IBrowserWindowNavigation
    {
        private readonly WebDriver driver;
        private readonly WindowType windowType;
        private readonly string type;

        protected internal BrowserWindowNavigation(WebDriver driver, ILocalizedLogger logger, WindowType windowType)
        {
            this.driver = driver;
            Logger = logger;
            this.windowType = windowType;
            type = windowType.ToString().ToLowerInvariant();
        }

        private ILocalizedLogger Logger { get; }

        public string CurrentHandle
        {
            get
            {
                Logger.Info($"loc.browser.get.{type}.handle");
                return driver.CurrentWindowHandle;
            }
        }

        public IList<string> Handles
        {
            get
            {
                Logger.Info($"loc.browser.get.{type}.handles");
                return driver.WindowHandles;
            }
        }

        public void Close()
        {
            Logger.Info($"loc.browser.{type}.close");
            driver.Close();
        }

        public void OpenNew(bool switchToNew = true)
            => OpenNew(switchToNew, log: true);

        private void OpenNew(bool switchToNew, bool log)
        {
            if (log)
            {
                Logger.Info($"loc.browser.{type}.open.new");
            }            
            var currentHandle = switchToNew ? null : CurrentHandle;
            driver.SwitchTo().NewWindow(windowType);
            if (!switchToNew)
            {
                CloseAndSwitch(currentHandle, closeCurrent: false);
            }
        }

        public void OpenNewViaJs(bool switchToNew = true)
        {
            Logger.Info($"loc.browser.{type}.open.new");
            var script = windowType == WindowType.Tab ? JavaScript.OpenNewTab : JavaScript.OpenNewWindow;
            driver.ExecuteScript(script.GetScript());
            if (switchToNew)
            {
                SwitchToLast();
            }
        }

        public void OpenInNew(string url)
        {
            Logger.Info($"loc.browser.navigate.in.new.{type}", url);
            OpenNew(switchToNew: true, log: false);
            driver.Navigate().GoToUrl(url);
        }

        public void OpenInNew(Uri url)
        {
            Logger.Info($"loc.browser.navigate.in.new.{type}", url);
            OpenNew(switchToNew: true);
            driver.Navigate().GoToUrl(url);
        }

        public void OpenInNewViaJs(string url)
        {
            Logger.Info($"loc.browser.navigate.in.new.{type}", url);
            var script = windowType == WindowType.Tab ? JavaScript.OpenInNewTab : JavaScript.OpenInNewWindow;
            driver.ExecuteScript(script.GetScript(), url);
        }

        public void SwitchToLast(bool closeCurrent = false)
        {
            Logger.Info($"loc.browser.switch.to.new.{type}");
            CloseAndSwitch(Handles.Last(), closeCurrent);
        }

        public void SwitchTo(string tabHandle, bool closeCurrent = false)
        {
            Logger.Info($"loc.browser.switch.to.{type}.handle", tabHandle);
            CloseAndSwitch(tabHandle, closeCurrent);
        }

        public void SwitchTo(int index, bool closeCurrent = false)
        {
            Logger.Info($"loc.browser.switch.to.{type}.index", index);
            var names = Handles;
            if (index < 0 || names.Count <= index)
            {
                throw new ArgumentOutOfRangeException(
                    $"Index of browser {type} '{index}' you provided is out of range {0}..{names.Count}");
            }

            var newTab = names[index];
            CloseAndSwitch(newTab, closeCurrent);
        }

        private void CloseAndSwitch(string name, bool closeCurrent)
        {
            if (closeCurrent)
            {
                Close();
            }

            driver.SwitchTo().Window(name);
        }

        public string CurrentTabHandle => CurrentHandle;

        public IList<string> TabHandles => Handles;

        public void SwitchToTab(string windowHandle, bool closeCurrent = false)
            => SwitchTo(windowHandle, closeCurrent);

        public void SwitchToTab(int index, bool closeCurrent = false)
            => SwitchTo(index, closeCurrent);

        public void SwitchToLastTab(bool closeCurrent = false)
            => SwitchToLast(closeCurrent);

        public void CloseTab()
            => Close();

        public void OpenNewTab(bool switchToNew = true)
            => OpenNew(switchToNew);

        public void OpenNewTabViaJs(bool switchToNew = true) 
            => OpenNewViaJs(switchToNew);

        public void OpenInNewTab(string url)
            => OpenInNew(url);

        public void OpenInNewTab(Uri url)
            => OpenInNew(url);

        public void OpenInNewTabViaJs(string url)
            => OpenInNewViaJs(url);
    }
}
