using Aquality.Selenium.Browsers;
using Aquality.Selenium.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebDriverManager.Helpers;

namespace Aquality.Selenium.Configurations.WebDriverSettings
{
    /// <summary>
    /// Abstract representation of web driver settings.
    /// </summary>
    public abstract class DriverSettings : IDriverSettings
    {
        /// <summary>
        /// Instantiates class using JSON file with general settings.
        /// </summary>
        /// <param name="settingsFile">JSON settings file.</param>
        public DriverSettings(JsonFile settingsFile)
        {
            SettingsFile = settingsFile;
        }

        private string DriverSettingsPath
        {
            get
            {
                return $".driverSettings.{BrowserName.ToString().ToLowerInvariant()}";
            }
        }

        protected IDictionary<string, object> BrowserCapabilities
        {
            get
            {
                return SettingsFile.GetObject<IDictionary<string, object>>($"{DriverSettingsPath}.capabilities");
            }
        }

        protected IDictionary<string, object> BrowserOptions
        {
            get
            {
                return SettingsFile.GetObject<IDictionary<string, object>>($"{DriverSettingsPath}.options");
            }
        }

        protected IList<string> BrowserStartArguments
        {
            get
            {
                return SettingsFile.GetObject<IList<string>>($"{DriverSettingsPath}.startArguments");
            }
        }

        protected JsonFile SettingsFile { get; }

        protected abstract BrowserName BrowserName { get; }

        public abstract string DownloadDirCapabilityKey { get; }

        public abstract DriverOptions DriverOptions { get; }

        public string WebDriverVersion => SettingsFile.GetObject<string>($"{DriverSettingsPath}.webDriverVersion");

        public Architecture SystemArchitecture
        {
            get
            {
                var jsonPath = $"{DriverSettingsPath}.systemArchitecture";
                return SettingsFile.IsValuePresent(jsonPath)
                ? (Architecture) Enum.Parse(typeof(Architecture), SettingsFile.GetObject<string>(jsonPath))
                : Architecture.Auto;
            }
        }

        public virtual string DownloadDir
        {
            get
            {
                if (BrowserOptions.ContainsKey(DownloadDirCapabilityKey))
                {
                    var pathInConfiguration = BrowserOptions[DownloadDirCapabilityKey].ToString();
                    return pathInConfiguration.Contains(".") ? Path.GetFullPath(pathInConfiguration) : pathInConfiguration;
                }

                throw new InvalidDataException($"failed to find {DownloadDirCapabilityKey} profiles option for {BrowserName}");
            }
        }

        protected void SetCapabilities(DriverOptions options)
        {
            BrowserCapabilities.ToList().ForEach(capability => options.AddAdditionalCapability(capability.Key, capability.Value));
        }
    }
}
