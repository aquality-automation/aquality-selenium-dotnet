using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using WebDriverManager.Helpers;

namespace Aquality.Selenium.Configurations.WebDriverSettings
{
    /// <summary>
    /// Settings specific for Yandex web driver.
    /// </summary>
    public class YandexSettings : ChromeSettings
    {
        private const string DefaultBinaryLocation = "%USERPROFILE%\\AppData\\Local\\Yandex\\YandexBrowser\\Application\\browser.exe";
        /// <summary>
        /// Instantiates class using JSON file with general settings.
        /// </summary>
        /// <param name="settingsFile">JSON settings file.</param>
        public YandexSettings(ISettingsFile settingsFile) : base(settingsFile)
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

        protected override BrowserName BrowserName => BrowserName.Yandex;

        public override DriverOptions DriverOptions
        {
            get
            {
                var options = (ChromeOptions) base.DriverOptions;
                options.BinaryLocation = BinaryLocation;
                return options;
            }
        }
    }
}
