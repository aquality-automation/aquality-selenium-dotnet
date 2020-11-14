using System;
using System.Collections.Generic;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace Aquality.Selenium.Configurations.WebDriverSettings
{
    /// <summary>
    /// Settings specific for InternetExplorer web driver.
    /// </summary>
    public class InternetExplorerSettings : DriverSettings
    {
        /// <summary>
        /// Instantiates class using file with general settings.
        /// </summary>
        /// <param name="settingsFile">Settings file.</param>
        public InternetExplorerSettings(ISettingsFile settingsFile) : base(settingsFile)
        {
        }

        protected override BrowserName BrowserName => BrowserName.IExplorer;

        protected override IDictionary<string, Action<DriverOptions, object>> KnownCapabilitySetters => new Dictionary<string, Action<DriverOptions, object>>
        {
            { "ignoreProtectedModeSettings", (options, value) => ((InternetExplorerOptions) options).IntroduceInstabilityByIgnoringProtectedModeSettings = (bool) value },
            { "ignoreZoomSetting", (options, value) => ((InternetExplorerOptions) options).IgnoreZoomLevel = (bool) value },
            { CapabilityType.HasNativeEvents, (options, value) => ((InternetExplorerOptions) options).EnableNativeEvents = (bool) value },
            { CapabilityType.UnhandledPromptBehavior, (options, value) => ((InternetExplorerOptions) options).UnhandledPromptBehavior = value.ToEnum<UnhandledPromptBehavior>() },
            { "ie.browserCommandLineSwitches", (options, value) => ((InternetExplorerOptions) options).BrowserCommandLineArguments = value.ToString() },
            { "elementScrollBehavior", (options, value) => ((InternetExplorerOptions) options).ElementScrollBehavior = value.ToEnum<InternetExplorerElementScrollBehavior>() }
        };

        public override DriverOptions DriverOptions
        {
            get
            {
                var options = new InternetExplorerOptions();
                SetCapabilities(options);
                SetOptionsByPropertyNames(options);
                SetPageLoadStrategy(options);
                options.BrowserCommandLineArguments = string.Join(" ", BrowserStartArguments);
                return options;
            }
        }

        public override string DownloadDirCapabilityKey => throw new NotSupportedException("Download directory key for Internet Explorer profiles is not supported");
    }
}
