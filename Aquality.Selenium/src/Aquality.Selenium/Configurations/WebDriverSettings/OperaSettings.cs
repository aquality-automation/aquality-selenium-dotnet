using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;

namespace Aquality.Selenium.Configurations.WebDriverSettings
{
    /// <summary>
    /// Settings specific for Opera web driver.
    /// </summary>
    public class OperaSettings : ChromeSettings
    {
        private const string DefaultBinaryLocation = "%USERPROFILE%\\AppData\\Local\\Programs\\Opera\\opera.exe";
        /// <summary>
        /// Instantiates class using JSON file with general settings.
        /// </summary>
        /// <param name="settingsFile">JSON settings file.</param>
        public OperaSettings(ISettingsFile settingsFile) : base(settingsFile)
        {
        }

        public virtual string BinaryLocation
        {
            get
            {
                var pathInConfiguration = SettingsFile.GetValueOrDefault($"{DriverSettingsPath}.binaryLocation", DefaultBinaryLocation);
                return pathInConfiguration.StartsWith("%") ? Environment.ExpandEnvironmentVariables(pathInConfiguration) : Path.GetFullPath(pathInConfiguration);
            }
        }

        protected override BrowserName BrowserName => BrowserName.Opera;

        public override DriverOptions DriverOptions
        {
            get
            {
                var options = (ChromeOptions) base.DriverOptions;
                options.BinaryLocation = BinaryLocation;
                options.AddArgument("user-data-dir=" + Path.GetDirectoryName(BinaryLocation));
                return options;
            }
        }
    }
}
