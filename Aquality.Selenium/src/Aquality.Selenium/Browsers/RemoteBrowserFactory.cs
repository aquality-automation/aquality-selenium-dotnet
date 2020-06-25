using Aquality.Selenium.Configurations;
using Aquality.Selenium.Core.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Factory that creates instance of remote Browser.
    /// </summary>
    public class RemoteBrowserFactory : BrowserFactory
    {
        public RemoteBrowserFactory() : base()
        {
        }

        public override Browser Browser
        {
            get
            {
                AqualityServices.LocalizedLogger.Info("loc.browser.grid");
                var browserProfile = AqualityServices.Get<IBrowserProfile>();
                var capabilities = browserProfile.DriverSettings.DriverOptions.ToCapabilities();
                var timeoutConfiguration = AqualityServices.Get<ITimeoutConfiguration>();
                var driver = AqualityServices.Get<IActionRetrier>().DoWithRetry(() => 
                {
                    try
                    {
                        return new RemoteWebDriver(browserProfile.RemoteConnectionUrl, capabilities, timeoutConfiguration.Command);
                    }
                    catch (Exception e)
                    {
                        AqualityServices.LocalizedLogger.Fatal("loc.browser.grid.fail", e);
                        throw;
                    }                    
                }, new[] { typeof(WebDriverException) });
                LogBrowserIsReady(browserProfile.BrowserName);
                return new Browser(driver);
            }
        }
    }
}
