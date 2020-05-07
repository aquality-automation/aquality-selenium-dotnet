using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Aquality.Selenium.Configurations.WebDriverSettings
{
    /// <summary>
    /// Settings specific for Chrome web driver.
    /// </summary>
    public class ChromeSettings : DriverSettings
    {
        /// <summary>
        /// Instantiates class using JSON file with general settings.
        /// </summary>
        /// <param name="settingsFile">JSON settings file.</param>
        public ChromeSettings(ISettingsFile settingsFile) : base(settingsFile)
        {
        }

        protected override BrowserName BrowserName => BrowserName.Chrome;

        public override string DownloadDirCapabilityKey => "download.default_directory";

        public override DriverOptions DriverOptions
        {
            get
            {
                var options = new ChromeOptions();
                SetChromePrefs(options);
                SetCapabilities(options, (name, value) => options.AddAdditionalCapability(name, value, isGlobalCapability: true));
                SetChromeArguments(options);
                SetPageLoadStratergy(options);
                return options;
            }
        }

        private void SetChromePrefs(ChromeOptions options)
        {
            foreach (var option in BrowserOptions)
            {
                var value = option.Key == DownloadDirCapabilityKey ? DownloadDir : option.Value;
                options.AddUserProfilePreference(option.Key, value);                
            }
        }

        private void SetChromeArguments(ChromeOptions options)
        {
            options.AddArguments(BrowserStartArguments);
        }
    }
}
