using Aquality.Selenium.Configurations;
using OpenQA.Selenium.Remote;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Factory that creates instance of remote Browser (using <see cref="OpenQA.Selenium.Remote.RemoteWebDriver"/>).
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
                var driver = new RemoteWebDriver(browserProfile.RemoteConnectionUrl, browserProfile.DriverSettings.DriverOptions);
                return new Browser(driver, Configuration);
            }
        }
    }
}
