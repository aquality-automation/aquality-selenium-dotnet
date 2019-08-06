using Aquality.Selenium.Browsers;
using Aquality.Selenium.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System.Linq;

namespace Aquality.Selenium.Configurations.WebDriverSettings
{
    /// <summary>
    /// Settings specific for Firefox web driver.
    /// </summary>
    public class FirefoxSettings : DriverSettings
    {
        /// <summary>
        /// Instantiates class using JSON file with general settings.
        /// </summary>
        /// <param name="settingsFile">JSON settings file.</param>
        public FirefoxSettings(JsonFile settingsFile) : base(settingsFile)
        {
        }

        public override DriverOptions DriverOptions
        {
            get
            {
                var options = new FirefoxOptions();
                SetGlobalCapabilities(options);
                SetFirefoxPrefs(options);
                SetFirefoxArguments(options);
                return options;
            }
        }
        private void SetFirefoxPrefs(FirefoxOptions options)
        {
            foreach (var option in BrowserOptions)
            {
                var value = option.Key == DownloadDirCapabilityKey ? DownloadDir : option.Value;
                if(option.Key == DownloadDirCapabilityKey)
                {
                    options.SetPreference(option.Key, DownloadDir);
                }
                else if(value is bool)
                {
                    options.SetPreference(option.Key, (bool) value);
                }
                else if(value is int)
                {
                    options.SetPreference(option.Key, (int) value);
                }
                else if(value is float)
                {
                    options.SetPreference(option.Key, (float) value);
                }
                else if (value is string)
                {
                    options.SetPreference(option.Key, value as string);
                }
            }
        }

        private void SetFirefoxArguments(FirefoxOptions options)
        {
            options.AddArguments(BrowserStartArguments);
        }

        private void SetGlobalCapabilities(FirefoxOptions options)
        {
            BrowserCapabilities.ToList().ForEach(capability => options.AddAdditionalCapability(capability.Key, capability.Value, isGlobalCapability: true));
        }

        public override string DownloadDirCapabilityKey => "browser.download.dir";

        protected override BrowserName BrowserName => BrowserName.Firefox;
    }
}
