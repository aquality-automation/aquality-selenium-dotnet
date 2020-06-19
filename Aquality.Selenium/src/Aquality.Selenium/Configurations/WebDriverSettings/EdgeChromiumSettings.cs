using System;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using Microsoft.Edge.SeleniumTools;
using OpenQA.Selenium;

namespace Aquality.Selenium.Configurations.WebDriverSettings
{
    /// <summary>
    /// Settings specific for Edge(chromium) web driver.
    /// </summary>
    public class EdgeChromiumSettings : DriverSettings
    {
        /// <summary>
        /// Instantiates class using JSON file with general settings
        /// </summary>
        /// <param name="settingsFile">JSON settings file.</param>
        public EdgeChromiumSettings(ISettingsFile settingsFile) : base(settingsFile)
        {
        }

        protected override BrowserName BrowserName => BrowserName.EdgeChromium;

        public override string DownloadDirCapabilityKey => "download.default_directory";

        public override DriverOptions DriverOptions
        {
            get
            {
                var options = new EdgeOptions()
                {
                    UseChromium = true
                };
                SetEdgeChromiumPrefs(options);
                SetCapabilities(options, (name, value) => options.AddAdditionalCapability(name, value, isGlobalCapability: true));
                SetEdgeChromiumArguments(options);
                SetPageLoadStratergy(options);
                return options;
            }
        }

        private void SetEdgeChromiumPrefs(EdgeOptions options)
        {
            foreach (var option in BrowserOptions)
            {
                var value = option.Key == DownloadDirCapabilityKey ? DownloadDir : option.Value;
                options.AddUserProfilePreference(option.Key, value);
            }
        }

        private void SetEdgeChromiumArguments(EdgeOptions options)
        {
            options.AddArguments(BrowserStartArguments);
        }
    }
}
