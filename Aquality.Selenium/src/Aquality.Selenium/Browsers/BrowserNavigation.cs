using Aquality.Selenium.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace Aquality.Selenium.Browsers
{
    public class BrowserNavigation : INavigation
    {
        private readonly Logger logger = Logger.Instance;
        private readonly RemoteWebDriver driver;

        internal BrowserNavigation(RemoteWebDriver driver)
        {
            this.driver = driver;
        }

        public void Back()
        {
            logger.InfoLoc("loc.browser.back");
            driver.Navigate().Back();
        }

        public void Forward()
        {
            logger.InfoLoc("loc.browser.forward");
            driver.Navigate().Forward();
        }

        public void GoToUrl(string url)
        {
            logger.InfoLoc("loc.browser.navigate", url);
            driver.Navigate().GoToUrl(url);
        }

        public void GoToUrl(Uri url)
        {
            logger.InfoLoc("loc.browser.navigate", url);
            driver.Navigate().GoToUrl(url);
        }

        public void Refresh()
        {
            logger.InfoLoc("loc.browser.refresh");
            driver.Navigate().Refresh();
        }
    }
}
