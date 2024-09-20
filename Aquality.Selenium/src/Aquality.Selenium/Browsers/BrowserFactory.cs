using Aquality.Selenium.Configurations;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Utilities;
using OpenQA.Selenium;
using System;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Abstract representation of <see cref="IBrowserFactory"/>.
    /// </summary>
    public abstract class BrowserFactory : IBrowserFactory
    {
        protected BrowserFactory(IActionRetrier actionRetrier, IBrowserProfile browserProfile, ITimeoutConfiguration timeoutConfiguration, ILocalizedLogger localizedLogger)
        {
            ActionRetrier = actionRetrier;
            BrowserProfile = browserProfile;
            TimeoutConfiguration = timeoutConfiguration;
            LocalizedLogger = localizedLogger;
        }

        protected IActionRetrier ActionRetrier { get; }
        protected IBrowserProfile BrowserProfile { get; }
        protected ITimeoutConfiguration TimeoutConfiguration { get; }
        protected ILocalizedLogger LocalizedLogger { get; }

        protected abstract WebDriver Driver { get; }
        protected virtual DriverContext DriverContext { get; }

        public virtual Browser Browser
        {
            get
            {
                var driverCtx = ActionRetrier.DoWithRetry(() => DriverContext,
                    new[] { typeof(WebDriverException), typeof(InvalidOperationException) });

                var browser = driverCtx != null 
                    ? new Browser(driverCtx.Driver, driverCtx.DriverService) 
                    : new Browser(ActionRetrier.DoWithRetry(() => Driver, new[] { typeof(WebDriverException), typeof(InvalidOperationException) }));
                
                LocalizedLogger.Info("loc.browser.ready", BrowserProfile.BrowserName);
                return browser;
            }
        }
    }
}
