using Aquality.Selenium.Browsers;
using Aquality.Selenium.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;

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

        protected override BrowserName BrowserName => BrowserName.Firefox;

        protected override IDictionary<string, Action<DriverOptions, object>> KnownCapabilitySetters => new Dictionary<string, Action<DriverOptions, object>>
        {
            { "binary", (options, value) => ((FirefoxOptions) options).BrowserExecutableLocation = value.ToString() },            
            { "firefox_binary", (options, value) => ((FirefoxOptions) options).BrowserExecutableLocation = value.ToString() },
            { "firefox_profile", (options, value) => ((FirefoxOptions) options).Profile = new FirefoxProfileManager().GetProfile(value.ToString()) },
            { "log", (options, value) => ((FirefoxOptions) options).LogLevel = value.ToEnum<FirefoxDriverLogLevel>() },
            { "marionette", (options, value) => ((FirefoxOptions) options).UseLegacyImplementation = (bool) value }
        };

        public override DriverOptions DriverOptions
        {
            get
            {
                var options = new FirefoxOptions();
                SetCapabilities(options, (name, value) => options.AddAdditionalCapability(name, value, isGlobalCapability: true));
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

        public override string DownloadDirCapabilityKey => "browser.download.dir";
    }
}
