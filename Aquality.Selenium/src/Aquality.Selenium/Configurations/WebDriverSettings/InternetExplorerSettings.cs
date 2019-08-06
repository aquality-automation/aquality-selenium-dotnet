using System;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;

namespace Aquality.Selenium.Configurations.WebDriverSettings
{
    /// <summary>
    /// Settings specific for InternetExplorer web driver.
    /// </summary>
    public class InternetExplorerSettings : DriverSettings
    {
        /// <summary>
        /// Instantiates class using JSON file with general settings.
        /// </summary>
        /// <param name="settingsFile">JSON settings file.</param>
        public InternetExplorerSettings(JsonFile settingsFile) : base(settingsFile)
        {
        }

        public override DriverOptions DriverOptions
        {
            get
            {
                var options = new InternetExplorerOptions();
                SetCapabilities(options);
                return options;
            }
        }

        public override string DownloadDirCapabilityKey => throw new NotSupportedException("Download directory key for Internet Explorer profiles is not supported");

        protected override BrowserName BrowserName => BrowserName.InternetExplorer;
    }
}
