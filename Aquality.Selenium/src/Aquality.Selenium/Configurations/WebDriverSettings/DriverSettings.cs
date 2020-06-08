using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
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
        protected DriverSettings(ISettingsFile settingsFile)
        {
            SettingsFile = settingsFile;
        }

        protected ISettingsFile SettingsFile { get; }

        public string WebDriverVersion => SettingsFile.GetValueOrDefault($"{DriverSettingsPath}.webDriverVersion", "Latest");

        public Architecture SystemArchitecture => SettingsFile.GetValueOrDefault($"{DriverSettingsPath}.systemArchitecture", Architecture.Auto).ToEnum<Architecture>();

        public abstract DriverOptions DriverOptions { get; }

        public PageLoadStrategy PageLoadStrategy => SettingsFile.GetValueOrDefault($"{DriverSettingsPath}.pageLoadStrategy", PageLoadStrategy.Normal).ToEnum<PageLoadStrategy>();

        public virtual string DownloadDir
        {
            get
            {
                if (BrowserOptions.ContainsKey(DownloadDirCapabilityKey))
                {
                    var pathInConfiguration = BrowserOptions[DownloadDirCapabilityKey].ToString();
                    return pathInConfiguration.Contains(".") ? Path.GetFullPath(pathInConfiguration) : pathInConfiguration;
                }

                throw new InvalidOperationException($"Failed to find {DownloadDirCapabilityKey} option in settings profile for {BrowserName}");
            }
        }

        public abstract string DownloadDirCapabilityKey { get; }

        protected IReadOnlyDictionary<string, object> BrowserCapabilities => SettingsFile.GetValueDictionaryOrEmpty<object>($"{DriverSettingsPath}.capabilities");

        protected IReadOnlyDictionary<string, object> BrowserOptions => SettingsFile.GetValueDictionaryOrEmpty<object>($"{DriverSettingsPath}.options");

        protected IReadOnlyList<string> BrowserStartArguments => SettingsFile.GetValueListOrEmpty<string>($"{DriverSettingsPath}.startArguments");

        private string DriverSettingsPath => $".driverSettings.{BrowserName.ToString().ToLowerInvariant()}";        

        protected abstract BrowserName BrowserName { get; }

        protected virtual IDictionary<string, Action<DriverOptions, object>> KnownCapabilitySetters => new Dictionary<string, Action<DriverOptions, object>>();

        protected void SetPageLoadStratergy(DriverOptions options)
        {
            options.PageLoadStrategy = PageLoadStrategy;
        }

        protected void SetCapabilities(DriverOptions options, Action<string, object> addCapabilityMethod = null)
        {
            foreach (var capability in BrowserCapabilities)
            {
                try
                {
                    var defaultAddCapabilityMethod = addCapabilityMethod ?? options.AddAdditionalCapability;
                    defaultAddCapabilityMethod(capability.Key, capability.Value);
                } 
                catch (ArgumentException exception)
                {
                    if(exception.Message.StartsWith("There is already an option"))
                    {
                        SetKnownProperty(options, capability, exception);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        private void SetKnownProperty(DriverOptions options, KeyValuePair<string, object> capability, ArgumentException exception)
        {
            if (KnownCapabilitySetters.ContainsKey(capability.Key))
            {
                KnownCapabilitySetters[capability.Key](options, capability.Value);
            }
            else
            {
                SetOptionByPropertyName(options, capability, exception);
            }            
        }

        protected void SetOptionsByPropertyNames(DriverOptions options)
        {
            foreach (var option in BrowserOptions)
            {
                SetOptionByPropertyName(options, option, new NotSupportedException($"Property for option {option.Key} is not supported"));
            }
        }

        private void SetOptionByPropertyName(DriverOptions options, KeyValuePair<string, object> option, Exception exception)
        {
            var optionProperty = options
                            .GetType()
                            .GetProperties()
                            .FirstOrDefault(property => IsPropertyNameMatchOption(property.Name, option.Key) && property.CanWrite)
                            ?? throw exception;
            optionProperty.SetValue(options, option.Value);
        }

        private bool IsPropertyNameMatchOption(string propertyName, string optionKey)
        {
            return propertyName.Equals(optionKey, StringComparison.InvariantCultureIgnoreCase)
                || optionKey.ToLowerInvariant().Contains(propertyName.ToLowerInvariant());
        }
    }
}
