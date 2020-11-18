using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using OpenQA.Selenium;
using OpenQA.Selenium.Safari;

namespace Aquality.Selenium.Configurations.WebDriverSettings
{
    /// <summary>
    /// Settings specific for Safari web driver.
    /// </summary>
    public class SafariSettings : DriverSettings
    {
        /// <summary>
        /// Instantiates class using file with general settings.
        /// </summary>
        /// <param name="settingsFile">Settings file.</param>
        public SafariSettings(ISettingsFile settingsFile) : base(settingsFile)
        {
        }

        protected override BrowserName BrowserName => BrowserName.Safari;

        public override DriverOptions DriverOptions
        {
            get
            {
                var options = new SafariOptions();
                SetCapabilities(options);
                SetOptionsByPropertyNames(options);
                SetPageLoadStrategy(options);
                return options;
            }
        }
        public override string DownloadDirCapabilityKey => "safari.options.dataDir";
    }
}
