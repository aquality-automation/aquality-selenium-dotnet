using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
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
        private IReadOnlyDictionary<string, object> options;
        private IReadOnlyDictionary<string, object> capabilities;
        private IReadOnlyList<string> startArguments;

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

        public UnhandledPromptBehavior UnhandledPromptBehavior => SettingsFile.GetValueOrDefault($"{DriverSettingsPath}.unhandledPromptBehavior", UnhandledPromptBehavior.Ignore).ToEnum<UnhandledPromptBehavior>();

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

        protected IReadOnlyDictionary<string, object> BrowserCapabilities
        {
            get
            {
                if (capabilities == null)
                {
                    capabilities = SettingsFile.GetValueDictionaryOrEmpty<object>($"{DriverSettingsPath}.{nameof(capabilities)}");
                    if (capabilities.Any())
                    {
                        AqualityServices.LocalizedLogger.Debug("loc.browser.capabilities",
                            args: string.Join(",", capabilities.Select(cap => $"{Environment.NewLine}{cap.Key}: {cap.Value}")));
                    }
                }
                
                return capabilities;
            }
        }

        protected IReadOnlyDictionary<string, object> BrowserOptions
        {
            get
            {
                if (options == null)
                {
                    options = SettingsFile.GetValueDictionaryOrEmpty<object>($"{DriverSettingsPath}.{nameof(options)}"); 
                    if (options.Any())
                    {
                        AqualityServices.LocalizedLogger.Debug("loc.browser.options",
                            args: string.Join(",", options.Select(opt => $"{Environment.NewLine}{opt.Key}: {opt.Value}")));
                    }
                }                
                
                return options;
            }
        }

        protected IReadOnlyList<string> BrowserStartArguments
        {
            get
            {
                if (startArguments == null)
                {
                    startArguments = SettingsFile.GetValueListOrEmpty<string>($"{DriverSettingsPath}.{nameof(startArguments)}");

                    if (startArguments.Any())
                    {
                        AqualityServices.LocalizedLogger.Debug("loc.browser.arguments", args: string.Join(" ", startArguments));
                    }
                }
                return startArguments;
            }
        }

        private string DriverSettingsPath => $".driverSettings.{BrowserName.ToString().ToLowerInvariant()}";        

        protected abstract BrowserName BrowserName { get; }

        protected virtual IDictionary<string, Action<DriverOptions, object>> KnownCapabilitySetters => new Dictionary<string, Action<DriverOptions, object>>
        {
            { CapabilityType.UnhandledPromptBehavior, (options, value) => options.UnhandledPromptBehavior = value.ToEnum<UnhandledPromptBehavior>() }
        };

        protected void SetPageLoadStrategy(DriverOptions options)
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
