using Aquality.Selenium.Configurations;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Utilities;
using OpenQA.Selenium;
using System;
using System.Reflection;

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

        protected virtual T DoWithRetry<T>(Func<T> function) => ActionRetrier.DoWithRetry(function, new[] { typeof(WebDriverException), typeof(InvalidOperationException), typeof(TargetInvocationException) });

        public virtual Browser Browser
        {
            get
            {
                var driverCtx = DoWithRetry(() => DriverContext);

                var browser = driverCtx != null 
                    ? DoWithRetry(() => new Browser(driverCtx.Driver, driverCtx.DriverService))
                    : DoWithRetry(() => new Browser(Driver));
                
                LocalizedLogger.Info("loc.browser.ready", BrowserProfile.BrowserName);
                return browser;
            }
        }
    }
}
