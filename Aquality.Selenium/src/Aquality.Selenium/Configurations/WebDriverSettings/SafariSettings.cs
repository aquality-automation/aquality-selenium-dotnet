using Aquality.Selenium.Browsers;
using Aquality.Selenium.Utilities;
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
        /// Instantiates class using JSON file with general settings.
        /// </summary>
        /// <param name="settingsFile">JSON settings file.</param>
        public SafariSettings(JsonFile settingsFile) : base(settingsFile)
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
                return options;
            }
        }
        public override string DownloadDirCapabilityKey => "safari.options.dataDir";
    }
}
