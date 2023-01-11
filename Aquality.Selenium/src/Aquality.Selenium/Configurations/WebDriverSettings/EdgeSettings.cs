using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace Aquality.Selenium.Configurations.WebDriverSettings
{
    /// <summary>
    /// Settings specific for Edge (chromium) web driver.
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

        public override string DownloadDirCapabilityKey => "download.default_directory";

        public override DriverOptions DriverOptions
        {
            get
            {
                var options = new EdgeOptions();
                SetEdgePrefs(options);
                SetCapabilities(options, (name, value) => options.AddAdditionalOption(name, value));
                SetEdgeArguments(options);
                SetEdgeExcludedArguments(options);
                SetPageLoadStrategy(options);
                SetLoggingPreferences(options);
                return options;
            }
        }

        private void SetEdgeExcludedArguments(EdgeOptions options)
        {
            options.AddExcludedArguments(BrowserExcludedArguments);
        }

        private void SetEdgePrefs(EdgeOptions options)
        {
            foreach (var option in BrowserOptions)
            {
                var value = option.Key == DownloadDirCapabilityKey ? DownloadDir : option.Value;
                options.AddUserProfilePreference(option.Key, value);
            }
        }

        private void SetEdgeArguments(EdgeOptions options)
        {
            options.AddArguments(BrowserStartArguments);
        }
    }
}
