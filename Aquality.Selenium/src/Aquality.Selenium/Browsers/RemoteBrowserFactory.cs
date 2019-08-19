using Aquality.Selenium.Configurations;
using OpenQA.Selenium.Remote;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Factory that creates instance of remote Browser.
    /// </summary>
    public class RemoteBrowserFactory : BrowserFactory
    {
        public RemoteBrowserFactory(IConfiguration configuration) : base(configuration)
        {
        }

        public override Browser Browser
        {
            get
            {
                var browserProfile = Configuration.BrowserProfile;
                var capabilities = browserProfile.DriverSettings.DriverOptions.ToCapabilities();
                var driver = new RemoteWebDriver(browserProfile.RemoteConnectionUrl, capabilities, Configuration.TimeoutConfiguration.Command);
                return new Browser(driver, Configuration);
            }
        }
    }
}
