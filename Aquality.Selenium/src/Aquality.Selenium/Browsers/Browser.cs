using Aquality.Selenium.Configurations;
using Aquality.Selenium.Logging;
using Aquality.Selenium.Waitings;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Drawing;

namespace Aquality.Selenium.Browsers
{
    public class Browser
    {
        private readonly Logger logger = Logger.Instance;
        private readonly IConfiguration configuration;
        private TimeSpan implicitWaitTimeout;
        private TimeSpan pageLoadTimeout;

        public Browser(RemoteWebDriver webDriver, IConfiguration configuration)
        {
            this.configuration = configuration;
            Driver = webDriver;
            BrowserName = configuration.BrowserProfile.BrowserName;
            ImplicitWaitTimeout = configuration.TimeoutConfiguration.Implicit;
            PageLoadTimeout = configuration.TimeoutConfiguration.PageLoad;
            ScriptTimeout = configuration.TimeoutConfiguration.Script;
        }

        public RemoteWebDriver Driver { get; }

        public BrowserName BrowserName { get; }

        public TimeSpan ImplicitWaitTimeout
        {
            get
            {
                return implicitWaitTimeout;
            }
            set
            {
                if (!value.Equals(implicitWaitTimeout))
                {
                    Driver.Manage().Timeouts().ImplicitWait = value;
                    implicitWaitTimeout = value;
                }
            }
        }

        /// <summary>
        /// Set Page Load timeout (Will be ignored for Safari https://github.com/SeleniumHQ/selenium-google-code-issue-archive/issues/687)
        /// </summary>
        /// <param name="timeout"></param>
        public TimeSpan PageLoadTimeout
        {
            private get
            {
                return pageLoadTimeout;
            }
            set
            {
                if (!configuration.BrowserProfile.BrowserName.Equals(BrowserName.Safari))
                {
                    Driver.Manage().Timeouts().PageLoad = value;
                    pageLoadTimeout = value;
                }
            }
        }

        public TimeSpan ScriptTimeout
        {
            set
            {
                Driver.Manage().Timeouts().AsynchronousJavaScript = value;
            }
        }

        public string DownloadDirectory => configuration.BrowserProfile.DriverSettings.DownloadDir;

        public string CurrentUrl
        {
            get
            {
                logger.InfoLoc("loc.browser.getUrl");
                return Driver.Url;
            }
        }

        public void Quit()
        {
            logger.InfoLoc("loc.browser.driver.quit");
            Driver?.Quit();
        }

        public INavigation Navigate()
        {
            return new BrowserNavigation(Driver);
        }

        public void RefreshPageWithAlert(AlertActions alertAction)
        {
            Navigate().Refresh();
            HandleAlert(alertAction);
        }

        public void Maximize()
        {
            logger.InfoLoc("loc.browser.maximize");
            Driver.Manage().Window.Maximize();
        }

        public void WaitForPageToLoad()
        {
            var isLoaded = ConditionalWait.WaitForTrue(driver => ExecuteScript<bool>(JavaScript.IsPageLoaded), PageLoadTimeout);
            if (!isLoaded)
            {
                logger.WarnLoc("loc.browser.page.timeout");
            }
        }

        public byte[] GetScreenshot()
        {
            return Driver.GetScreenshot().AsByteArray;
        }

        public void ScrollWindowBy(int x, int y)
        {
            ExecuteScript(JavaScript.ScrollWindowBy, x, y);
        }

        public void ExecuteScriptFromFile(string embeddedResourcePath, params object[] arguments)
        {
            ExecuteScript(embeddedResourcePath.GetScript(), arguments);
        }

        public void ExecuteScript(JavaScript scriptName, params object[] arguments)
        {
            ExecuteScript(scriptName.GetScript(), arguments);
        }

        public void ExecuteScript(string script, params object[] arguments)
        {
            Driver.ExecuteJavaScript(script, arguments);
        }

        public T ExecuteScriptFromFile<T>(string embeddedResourcePath, params object[] arguments)
        {
            return ExecuteScript<T>(embeddedResourcePath.GetScript(), arguments);
        }

        public T ExecuteScript<T>(JavaScript scriptName, params object[] arguments)
        {
            return ExecuteScript<T>(scriptName.GetScript(), arguments);
        }

        public T ExecuteScript<T>(string script, params object[] arguments)
        {
            return Driver.ExecuteJavaScript<T>(script, arguments);
        }

        public void HandleAlert(AlertActions alertAction, string text = null)
        {
            try
            {
                IAlert alert = Driver.SwitchTo().Alert();
                if (!string.IsNullOrEmpty(text))
                {
                    alert.SendKeys(text);
                }
                if (alertAction.Equals(AlertActions.Accept))
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
            }
            catch (NoAlertPresentException ex)
            {
                logger.FatalLoc("loc.browser.alert.fail", ex);
                throw ex;
            }
        }        

        public void SetWindowSize(int width, int height)
        {
            Driver.Manage().Window.Size = new Size(width, height);
        }
    }

    public enum BrowserName
    {
        Chrome,
        Edge,
        Firefox,
        InternetExplorer,
        Safari
    }

    public enum AlertActions
    {
        Accept,
        Decline
    }
}
