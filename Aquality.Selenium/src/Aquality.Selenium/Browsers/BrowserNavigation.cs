using Aquality.Selenium.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace Aquality.Selenium.Browsers
{
    public class BrowserNavigation : INavigation
    {
        private readonly RemoteWebDriver driver;

        internal BrowserNavigation(RemoteWebDriver driver)
        {
            this.driver = driver;
        }

        private Logger Logger => Logger.Instance;

        public void Back()
        {
            Logger.InfoLoc("loc.browser.back");
            driver.Navigate().Back();
        }

        public void Forward()
        {
            Logger.InfoLoc("loc.browser.forward");
            driver.Navigate().Forward();
        }

        public void GoToUrl(string url)
        {
            Logger.InfoLoc("loc.browser.navigate", url);
            driver.Navigate().GoToUrl(url);
        }

        public void GoToUrl(Uri url)
        {
            Logger.InfoLoc("loc.browser.navigate", url);
            driver.Navigate().GoToUrl(url);
        }

        public void Refresh()
        {
            Logger.InfoLoc("loc.browser.refresh");
            driver.Navigate().Refresh();
        }
    }
}
