using Aquality.Selenium.Configurations;
using OpenQA.Selenium.Remote;

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
                var driver = new RemoteWebDriver(browserProfile.RemoteConnectionUrl, capabilities, timeoutConfiguration.Command);
                return new Browser(driver);
            }
        }
    }
}
