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

        protected internal BrowserNavigation(WebDriver driver, ILocalizedLogger logger)
        {
            this.driver = driver;
            Logger = logger;
        }

        private ILocalizedLogger Logger { get; }

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
            // temporary workaround to avoid issue described at https://github.com/SeleniumHQ/selenium/issues/12277
            try
            {
                driver.Navigate().GoToUrl(url);
            }
            catch (WebDriverException e) when (driver.Url == url)
            {
                Logger.Fatal($"Navigation error occurred: [{e.Message}], but successfully navigated to URL [{url}]", e);
                // ignore only unknown errors
                if (e.GetType() != typeof(WebDriverException))
                {
                    throw;
                }
            }            
        }

        /// <summary>
        /// Navigates to desired url.
        /// </summary>
        /// <param name="url">Uri representation of URL.</param>
        public void GoToUrl(Uri url)
        {
            InfoLocNavigate(url.ToString());
            try
            {
                driver.Navigate().GoToUrl(url);
            }
            catch (WebDriverException e) when (driver.Url == url.ToString())
            {
                Logger.Fatal($"Navigation error occurred: [{e.Message}], but successfully navigated to URL [{url}]", e);
            }
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
