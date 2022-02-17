using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using OpenQA.Selenium;
using OpenQA.Selenium.Opera;
using System;

namespace Aquality.Selenium.Configurations.WebDriverSettings
{
    /// <summary>
    /// Settings specific for Opera web driver.
    /// </summary>
    public class OperaSettings : DriverSettings
    {
        /// <summary>
        /// Instantiates class using JSON file with general settings.
        /// </summary>
        /// <param name="settingsFile">JSON settings file.</param>
        public OperaSettings(ISettingsFile settingsFile) : base(settingsFile)
        {
        }

        protected override BrowserName BrowserName => BrowserName.Opera;

        public override string DownloadDirCapabilityKey => throw new NotSupportedException("Download directory key for Opera profiles is not supported");

        public override DriverOptions DriverOptions
        {
            get
            {
                var options = new OperaOptions();                
                SetOperaPrefs(options);
                SetCapabilities(options, (name, value) => options.AddAdditionalOption(name, value));
                SetOperaArguments(options);
                SetOperaExcludedArguments(options);
                SetPageLoadStrategy(options);
                return options;
            }
        }

        private void SetOperaExcludedArguments(OperaOptions options)
        {
            options.AddExcludedArguments(BrowserExcludedArguments);
        }

        private void SetOperaPrefs(OperaOptions options)
        {
            foreach (var option in BrowserOptions)
            {
                options.AddUserProfilePreference(option.Key, option.Value);                
            }
        }

        private void SetOperaArguments(OperaOptions options)
        {
            options.AddArguments(BrowserStartArguments);
        }
    }
}
