using Aquality.Selenium.Configurations;
using Aquality.Selenium.Logging;
using Aquality.Selenium.Waitings;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Drawing;
using System.Reflection;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Provides functionality to work with browser via Selenium WebDriver.  
    /// </summary>
    public class Browser
    {
        private readonly Logger logger = Logger.Instance;
        private readonly IConfiguration configuration;
        private TimeSpan implicitWaitTimeout;
        private TimeSpan pageLoadTimeout;

        /// <summary>
        /// Instantiate browser.
        /// </summary>
        /// <param name="webDriver">Instance of Selenium WebDriver for desired web browser.</param>
        /// <param name="configuration">Configuration.</param>
        public Browser(RemoteWebDriver webDriver, IConfiguration configuration)
        {
            this.configuration = configuration;
            Driver = webDriver;
            BrowserName = configuration.BrowserProfile.BrowserName;
            ImplicitWaitTimeout = configuration.TimeoutConfiguration.Implicit;
            PageLoadTimeout = configuration.TimeoutConfiguration.PageLoad;
            ScriptTimeout = configuration.TimeoutConfiguration.Script;
        }

        /// <summary>
        /// Gets instance of Selenium WebDriver.
        /// </summary>
        /// <value>Instance of Selenium WebDriver for desired web browser.</value>
        public RemoteWebDriver Driver { get; }

        /// <summary>
        /// Gets name of desired browser from configuration.
        /// </summary>
        /// <value>Name of browser.</value>
        public BrowserName BrowserName { get; }

        /// <summary>
        /// Sets Selenium WebDriver ImplicitWait timeout. 
        /// Default value: <see cref="Aquality.Selenium.Configurations.ITimeoutConfiguration.Implicit"/>.
        /// </summary>
        public TimeSpan ImplicitWaitTimeout
        {
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
        /// Sets Selenium WebDriver PageLoad timeout. 
        /// Default value: <see cref="Aquality.Selenium.Configurations.ITimeoutConfiguration.PageLoad"/>.
        /// Ignored for Safari cause of https://github.com/SeleniumHQ/selenium-google-code-issue-archive/issues/687.
        /// </summary>
        public TimeSpan PageLoadTimeout
        {
            set
            {
                if (!configuration.BrowserProfile.BrowserName.Equals(BrowserName.Safari))
                {
                    Driver.Manage().Timeouts().PageLoad = value;
                    pageLoadTimeout = value;
                }
            }
        }

        /// <summary>
        /// Sets Selenium WebDriver AsynchronousJavaScript timeout. 
        /// Default value: <see cref="Aquality.Selenium.Configurations.ITimeoutConfiguration.Script"/>.
        /// </summary>
        public TimeSpan ScriptTimeout
        {
            set
            {
                Driver.Manage().Timeouts().AsynchronousJavaScript = value;
            }
        }

        /// <summary>
        /// Gets browser configured download directory.
        /// </summary>
        public string DownloadDirectory => configuration.BrowserProfile.DriverSettings.DownloadDir;

        /// <summary>
        /// Gets URL of currently opened page in web browser.
        /// </summary>
        /// <value>String representation of page URL.</value>
        public string CurrentUrl
        {
            get
            {
                logger.InfoLoc("loc.browser.getUrl");
                return Driver.Url;
            }
        }

        /// <summary>
        /// Quit web browser.
        /// </summary>
        public void Quit()
        {
            logger.InfoLoc("loc.browser.driver.quit");
            Driver?.Quit();
        }

        /// <summary>
        /// Gets wrapper over Selenium WebDriver INavigate implementation.
        /// </summary>
        /// <returns>Instance of custom navigation object.</returns>
        public INavigation Navigate()
        {
            return new BrowserNavigation(Driver);
        }

        /// <summary>
        /// Refreshes web page and handles alert. 
        /// </summary>
        /// <param name="alertAction">Action which should be done with appeared alert.</param>
        public void RefreshPageWithAlert(AlertAction alertAction)
        {
            Navigate().Refresh();
            HandleAlert(alertAction);
        }

        /// <summary>
        /// Handles alert.
        /// </summary>
        /// <param name="alertAction">Action which should be done with appeared alert.</param>
        /// <param name="text">Text which can be send to alert.</param>
        /// <exception cref="OpenQA.Selenium.NoAlertPresentException">Thrown when no alert found.</exception>
        public void HandleAlert(AlertAction alertAction, string text = null)
        {
            try
            {
                var alert = Driver.SwitchTo().Alert();
                if (!string.IsNullOrEmpty(text))
                {
                    alert.SendKeys(text);
                }
                if (alertAction.Equals(AlertAction.Accept))
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

        /// <summary>
        /// Maximises web page.
        /// </summary>
        public void Maximize()
        {
            logger.InfoLoc("loc.browser.maximize");
            Driver.Manage().Window.Maximize();
        }

        /// <summary>
        /// Waits for page to load.
        /// Default value of timeout: <see cref="Aquality.Selenium.Configurations.ITimeoutConfiguration.PageLoad"/>.
        /// </summary>
        public void WaitForPageToLoad()
        {
            var isLoaded = ConditionalWait.WaitForTrue(driver => ExecuteScript<bool>(JavaScript.IsPageLoaded), pageLoadTimeout);
            if (!isLoaded)
            {
                logger.WarnLoc("loc.browser.page.timeout");
            }
        }

        /// <summary>
        /// Gets screenshot of web page.
        /// </summary>
        /// <returns>Screenshot as byte array.</returns>
        public byte[] GetScreenshot()
        {
            return Driver.GetScreenshot().AsByteArray;
        }

        /// <summary>
        /// Scrolls window by coordinates.
        /// </summary>
        /// <param name="x">Horizontal coordinate.</param>
        /// <param name="y">Vertical coordinate.</param>
        public void ScrollWindowBy(int x, int y)
        {
            ExecuteScript(JavaScript.ScrollWindowBy, x, y);
        }

        /// <summary>
        /// Executes JS script from embedded resource file (*.js).
        /// </summary>
        /// <param name="embeddedResourcePath">Embedded resource path.</param>
        /// <param name="arguments">Script arguments.</param>
        public void ExecuteScriptFromFile(string embeddedResourcePath, params object[] arguments)
        {
            ExecuteScript(embeddedResourcePath.GetScript(Assembly.GetCallingAssembly()), arguments);
        }

        /// <summary>
        /// Executes predefined JS script.
        /// </summary>
        /// <param name="scriptName">Name of desired JS script.</param>
        /// <param name="arguments">Script arguments.</param>
        public void ExecuteScript(JavaScript scriptName, params object[] arguments)
        {
            ExecuteScript(scriptName.GetScript(), arguments);
        }

        /// <summary>
        /// Executes JS script.
        /// </summary>
        /// <param name="script">String representation of JS script.</param>
        /// <param name="arguments">Script arguments.</param>
        public void ExecuteScript(string script, params object[] arguments)
        {
            Driver.ExecuteJavaScript(script, arguments);
        }

        /// <summary>
        /// Executes JS script from embedded resource file (*.js) and gets result value.
        /// </summary>
        /// <param name="embeddedResourcePath">Embedded resource path.</param>
        /// <param name="arguments">Script arguments.</param>
        /// <typeparam name="T">Type of return value.</typeparam>
        /// <returns>Script execution result.</returns>
        public T ExecuteScriptFromFile<T>(string embeddedResourcePath, params object[] arguments)
        {
            return ExecuteScript<T>(embeddedResourcePath.GetScript(Assembly.GetCallingAssembly()), arguments);
        }

        /// <summary>
        /// Executes predefined JS script and gets result value.
        /// </summary>
        /// <param name="scriptName">Name of desired JS script.</param>
        /// <param name="arguments">Script arguments.</param>
        /// <typeparam name="T">Type of return value.</typeparam>
        /// <returns>Script execution result.</returns>
        public T ExecuteScript<T>(JavaScript scriptName, params object[] arguments)
        {
            return ExecuteScript<T>(scriptName.GetScript(), arguments);
        }

        /// <summary>
        /// Executes JS script and gets result value. 
        /// </summary>
        /// <param name="script">String representation of JS script.</param>
        /// <param name="arguments">Script arguments.</param>
        /// <typeparam name="T">Type of return value.</typeparam>
        /// <returns>Script execution result.</returns>
        public T ExecuteScript<T>(string script, params object[] arguments)
        {
            return Driver.ExecuteJavaScript<T>(script, arguments);
        }                

        /// <summary>
        /// Sets size of current window.
        /// </summary>
        /// <param name="width">Width in pixels.</param>
        /// <param name="height">Height in pixels.</param>
        public void SetWindowSize(int width, int height)
        {
            Driver.Manage().Window.Size = new Size(width, height);
        }
    }

    /// <summary>
    /// Supported browser.
    /// </summary>
    public enum BrowserName
    {
        Chrome,
        Edge,
        Firefox,
        IExplorer,
        Safari
    }

    /// <summary>
    /// Possible alert action.
    /// </summary>
    public enum AlertAction
    {
        Accept,
        Decline
    }
}
