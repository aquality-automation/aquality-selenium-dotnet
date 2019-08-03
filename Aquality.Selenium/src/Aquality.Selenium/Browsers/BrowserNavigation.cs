using Aquality.Selenium.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Wrapper over implementation of Selenium WebDriver navigation.
    /// </summary>
    public class BrowserNavigation : INavigation
    {
        private readonly RemoteWebDriver driver;

        internal BrowserNavigation(RemoteWebDriver driver)
        {
            this.driver = driver;
        }

        private Logger Logger => Logger.Instance;

        /// <summary>
        /// Navigates back.
        /// </summary>
        public void Back()
        {
            Logger.InfoLoc("loc.browser.back");
            driver.Navigate().Back();
        }

        /// <summary>
        /// Navigates forward.
        /// </summary>
        public void Forward()
        {
            Logger.InfoLoc("loc.browser.forward");
            driver.Navigate().Forward();
        }

        /// <summary>
        /// Navigates to desired url.
        /// </summary>
        /// <param name="url">String representation of URL.</param>
        public void GoToUrl(string url)
        {
            Logger.InfoLoc("loc.browser.navigate", url);
            driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Navigates to desired url.
        /// </summary>
        /// <param name="url">Uri representation of URL.</param>
        public void GoToUrl(Uri url)
        {
            Logger.InfoLoc("loc.browser.navigate", url);
            driver.Navigate().GoToUrl(url);
        }

        /// <summary>
        /// Refreshes current page.
        /// </summary>
        public void Refresh()
        {
            Logger.InfoLoc("loc.browser.refresh");
            driver.Navigate().Refresh();
        }
    }
}
