using Aquality.Selenium.Configurations;
using Aquality.Selenium.Core.Utilities;
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
                var browserProfile = AqualityServices.Get<IBrowserProfile>();
                var capabilities = browserProfile.DriverSettings.DriverOptions.ToCapabilities();
                var timeoutConfiguration = AqualityServices.Get<ITimeoutConfiguration>();
                var driver = AqualityServices.Get<IActionRetrier>().DoWithRetry(() => 
                    (RemoteWebDriver)Activator.CreateInstance(typeof(RemoteWebDriver), 
                    browserProfile.RemoteConnectionUrl, capabilities, timeoutConfiguration.Command));
                return new Browser(driver);
            }
        }
    }
}
