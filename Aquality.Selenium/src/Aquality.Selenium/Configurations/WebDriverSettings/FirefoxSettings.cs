using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
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
        /// Instantiates class using file with general settings.
        /// </summary>
        /// <param name="settingsFile">Settings file.</param>
        public FirefoxSettings(ISettingsFile settingsFile) : base(settingsFile)
        {
        }

        protected override BrowserName BrowserName => BrowserName.Firefox;

        public bool IsGeckoServiceHostDefaultEnabled => SettingsFile.GetValueOrDefault($"{DriverSettingsPath}.isGeckoServiceHostDefaultEnabled", false);

        protected override IDictionary<string, Action<DriverOptions, object>> KnownCapabilitySetters => new Dictionary<string, Action<DriverOptions, object>>
        {
            { "binary", (options, value) => ((FirefoxOptions) options).BrowserExecutableLocation = value.ToString() },            
            { "firefox_binary", (options, value) => ((FirefoxOptions) options).BrowserExecutableLocation = value.ToString() },
            { "firefox_profile", (options, value) => ((FirefoxOptions) options).Profile = new FirefoxProfileManager().GetProfile(value.ToString()) },
            { "log", (options, value) => ((FirefoxOptions) options).LogLevel = value.ToEnum<FirefoxDriverLogLevel>() }
        };

        public override DriverOptions DriverOptions
        {
            get
            {
                var options = new FirefoxOptions();
                SetCapabilities(options, (name, value) => options.AddAdditionalOption(name, value));
                SetFirefoxPrefs(options);
                SetFirefoxArguments(options);
                SetPageLoadStrategy(options);
                SetLoggingPreferences(options);
                return options;
            }
        }

        private void SetFirefoxPrefs(FirefoxOptions options)
        {
            foreach (var option in BrowserOptions)
            {
                var value = option.Key == DownloadDirCapabilityKey ? DownloadDir : option.Value;
                if (option.Key == DownloadDirCapabilityKey)
                {
                    options.SetPreference(option.Key, DownloadDir);
                }
                else if (value is bool boolean)
                {
                    options.SetPreference(option.Key, boolean);
                }
                else if (value is int @int)
                {
                    options.SetPreference(option.Key, @int);
                }
                else if (value is long @long)
                {
                    options.SetPreference(option.Key, @long);
                }
                else if (value is float @float)
                {
                    options.SetPreference(option.Key, @float);
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
