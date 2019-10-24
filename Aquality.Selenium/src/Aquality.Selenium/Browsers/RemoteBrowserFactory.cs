using Aquality.Selenium.Configurations;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium.Remote;
using System;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Factory that creates instance of remote Browser.
    /// </summary>
    public class RemoteBrowserFactory : BrowserFactory
    {
        public RemoteBrowserFactory(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override Browser Browser
        {
            get
            {
                var browserProfile = ServiceProvider.GetRequiredService<IBrowserProfile>();
                var capabilities = browserProfile.DriverSettings.DriverOptions.ToCapabilities();
                var timeoutConfiguration = ServiceProvider.GetRequiredService<ITimeoutConfiguration>();
                var driver = new RemoteWebDriver(browserProfile.RemoteConnectionUrl, capabilities, timeoutConfiguration.Command);
                return new Browser(driver, ServiceProvider);
            }
        }
    }
}
