using System;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace Aquality.Selenium.Configurations.WebDriverSettings
{
    /// <summary>
    /// Settings specific for Edge web driver.
    /// </summary>
    public class EdgeSettings : DriverSettings
    {
        /// <summary>
        /// Instantiates class using JSON file with general settings.
        /// </summary>
        /// <param name="settingsFile">JSON settings file.</param>
        public EdgeSettings(ISettingsFile settingsFile) : base(settingsFile)
        {
        }

        protected override BrowserName BrowserName => BrowserName.Edge;

        public override DriverOptions DriverOptions
        {
            get
            {
                var options = new EdgeOptions();
                SetCapabilities(options);
                SetOptionsByPropertyNames(options);
                SetPageLoadStrategy(options);
                return options;
            }
        }
        
        public override string DownloadDirCapabilityKey => throw new NotSupportedException("Download directory key for Edge profiles is not supported");
    }
}
