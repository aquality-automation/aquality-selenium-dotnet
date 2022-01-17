using Aquality.Selenium.Core.Localization;
using OpenQA.Selenium;
using System;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Wrapper over implementation of Selenium WebDriver navigation.
    /// </summary>
    public class BrowserNavigation : INavigation
    {
        private readonly WebDriver driver;

        internal BrowserNavigation(WebDriver driver)
        {
            this.driver = driver;
        }

        private ILocalizedLogger Logger => AqualityServices.LocalizedLogger;

        /// <summary>
        /// Navigates back.
        /// </summary>
        public void Back()
        {
            Logger.Info("loc.browser.back");
            driver.Navigate().Back();
        }

        /// <summary>
        /// Navigates forward.
        /// </summary>
        public void Forward()
        {
            Logger.Info("loc.browser.forward");
            driver.Navigate().Forward();
        }

        /// <summary>
        /// Navigates to desired url.
        /// </summary>
        /// <param name="url">String representation of URL.</param>
        public void GoToUrl(string url)
        {
            InfoLocNavigate(url);
            driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Navigates to desired url.
        /// </summary>
        /// <param name="url">Uri representation of URL.</param>
        public void GoToUrl(Uri url)
        {
            InfoLocNavigate(url.ToString());
            driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Refreshes current page.
        /// </summary>
        public void Refresh()
        {
            Logger.Info("loc.browser.refresh");
            driver.Navigate().Refresh();
        }

        private void InfoLocNavigate(string url)
        {
            Logger.Info("loc.browser.navigate", url);
        }
    }
}
