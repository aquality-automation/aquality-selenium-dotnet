using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chromium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Aquality.Selenium.Configurations.WebDriverSettings
{
    /// <summary>
    /// Settings specific for Chromium web driver.
    /// </summary>
    public abstract class ChromiumSettings : DriverSettings
    {
        private const string MobileEmulationCapability = "mobileEmulation";

        /// <summary>
        /// Instantiates class using file with general settings.
        /// </summary>
        /// <param name="settingsFile">Settings file.</param>
        protected ChromiumSettings(ISettingsFile settingsFile) : base(settingsFile)
        {
        }

        protected override IDictionary<string, Action<DriverOptions, object>> KnownCapabilitySetters => new Dictionary<string, Action<DriverOptions, object>>
        {
            { MobileEmulationCapability, (options, value) => SetMobileEmulation((ChromiumOptions) options) }
        };

        /// <summary>
        /// Allows the Chromium browser to emulate a mobile device. Settings are gathered from capabilities.mobileEmulation path.
        /// </summary>
        /// <param name="options">Current options object to inject capabilities into.</param>
        /// <exception cref="ArgumentException">Thrown if the mobileEmulation is not a dictionary, does not have a user agent string set nor the name of the device to emulate.</exception>
        protected void SetMobileEmulation(ChromiumOptions options)
        {
            var dictionary = SettingsFile.GetValueDictionaryOrEmpty<object>($"{DriverSettingsPath}.capabilities.{MobileEmulationCapability}");
            if (!dictionary.Any(pair => new Regex("(deviceName|userAgent)").IsMatch(pair.Key)))
            {
                throw new ArgumentException("mobileEmulation must be an object (dictionary), and have either deviceName or userAgent specified", nameof(options));
            }
            if (dictionary.TryGetValue("deviceName", out object deviceName))
            {
                options.EnableMobileEmulation(deviceName as string);
            }
            else
            {
                var mobileEmulationOptions = new ChromiumMobileEmulationDeviceSettings(dictionary["userAgent"] as string);
                var deviceMetrics = SettingsFile.GetValueDictionaryOrEmpty<object>($"{DriverSettingsPath}.capabilities.{MobileEmulationCapability}.deviceMetrics");
                if (deviceMetrics.TryGetValue("width", out var width) && long.TryParse(width.ToString(), out long widthLong))
                {
                    mobileEmulationOptions.Width = widthLong;
                }
                if (deviceMetrics.TryGetValue("height", out var height) && long.TryParse(height.ToString(), out long heightLong))
                {
                    mobileEmulationOptions.Height = heightLong;
                }
                if (deviceMetrics.TryGetValue("pixelRatio", out var pixelRatio) && double.TryParse(pixelRatio.ToString(), out double pixelRatioDouble))
                {
                    mobileEmulationOptions.PixelRatio = pixelRatioDouble;
                }
                if (deviceMetrics.TryGetValue("touch", out var touch) && bool.TryParse(touch?.ToString(), out var enableTouch))
                {
                    mobileEmulationOptions.EnableTouchEvents = enableTouch;
                }
                options.EnableMobileEmulation(mobileEmulationOptions);
            }
        }

        public override string DownloadDirCapabilityKey => "download.default_directory";
    }
}
