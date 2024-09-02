using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Utilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aquality.Selenium.Configurations.WebDriverSettings
{
    /// <summary>
    /// Abstract representation of web driver settings.
    /// </summary>
    public abstract class DriverSettings : IDriverSettings
    {
        private IReadOnlyDictionary<string, object> options;
        private IReadOnlyDictionary<string, object> capabilities;
        private IReadOnlyDictionary<string, LogLevel> loggingPreferences;
        private IReadOnlyList<string> startArguments;
        private IReadOnlyList<string> excludedArguments;

        /// <summary>
        /// Instantiates class using JSON file with general settings.
        /// </summary>
        /// <param name="settingsFile">JSON settings file.</param>
        protected DriverSettings(ISettingsFile settingsFile)
        {
            SettingsFile = settingsFile;
        }

        protected ISettingsFile SettingsFile { get; }

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

        protected IReadOnlyDictionary<string, LogLevel> LoggingPreferences
        {
            get
            {
                if (loggingPreferences == null)
                {
                    loggingPreferences = SettingsFile.GetValueDictionaryOrEmpty<LogLevel>($"{DriverSettingsPath}.{nameof(loggingPreferences)}");
                    if (loggingPreferences.Any())
                    {
                        AqualityServices.LocalizedLogger.Debug("loc.browser.loggingPreferences",
                            args: string.Join(",", loggingPreferences.Select(opt => $"{Environment.NewLine}{opt.Key}: {opt.Value}")));
                    }
                }

                return loggingPreferences;
            }
        }

        protected IReadOnlyList<string> BrowserExcludedArguments
        {
            get
            {
                if (excludedArguments == null)
                {
                    excludedArguments = SettingsFile.GetValueListOrEmpty<string>($"{DriverSettingsPath}.{nameof(excludedArguments)}");
                    if (excludedArguments.Any())
                    {
                        AqualityServices.LocalizedLogger.Debug("loc.browser.excludedArguments", args: string.Join(" ", excludedArguments));
                    }
                }

                return excludedArguments;
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

        protected virtual string DriverSettingsPath => $".driverSettings.{BrowserName.ToString().ToLowerInvariant()}";

        protected abstract BrowserName BrowserName { get; }

        protected virtual IDictionary<string, Action<DriverOptions, object>> KnownCapabilitySetters => new Dictionary<string, Action<DriverOptions, object>>();

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
                    var defaultAddCapabilityMethod = addCapabilityMethod ?? options.AddAdditionalOption;
                    defaultAddCapabilityMethod(capability.Key, capability.Value);
                }
                catch (ArgumentException exception)
                {
                    if (exception.Message.StartsWith("There is already an option"))
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

        protected void SetLoggingPreferences(DriverOptions options)
        {
            foreach (var preference in LoggingPreferences)
            {
                options.SetLoggingPreference(preference.Key.ToLower(), preference.Value);
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

        private static void SetOptionByPropertyName(DriverOptions options, KeyValuePair<string, object> option, Exception exception)
        {
            var optionProperty = options
                            .GetType()
                            .GetProperties()
                            .FirstOrDefault(property => IsPropertyNameMatchOption(property.Name, option.Key) && property.CanWrite)
                            ?? throw exception;
            var propertyType = optionProperty.PropertyType;
            var valueToSet = IsEnumValue(propertyType, option.Value)
                ? ParseEnumValue(propertyType, option.Value)
                : option.Value;
            optionProperty.SetValue(options, valueToSet);
        }

        private static object ParseEnumValue(Type propertyType, object optionValue)
        {
            return optionValue is string
                ? Enum.Parse(propertyType, optionValue.ToString(), ignoreCase: true)
                : Enum.ToObject(propertyType, Convert.ChangeType(optionValue, Enum.GetUnderlyingType(propertyType)));
        }

        private static bool IsEnumValue(Type propertyType, object optionValue)
        {
            var valueAsString = optionValue.ToString();
            if (!propertyType.IsEnum || string.IsNullOrEmpty(valueAsString))
            {
                return false;
            }
            var normalizedValue = char.ToUpper(valueAsString[0]) +
                (valueAsString.Length > 1 ? valueAsString.Substring(1) : string.Empty);
            return propertyType.IsEnumDefined(normalizedValue)
                || propertyType.IsEnumDefined(valueAsString)
                || (IsValueOfIntegralNumericType(optionValue)
                    && propertyType.IsEnumDefined(Convert.ChangeType(optionValue, Enum.GetUnderlyingType(propertyType))));
        }

        private static bool IsValueOfIntegralNumericType(object value)
        {
            return value is byte || value is sbyte
                || value is ushort || value is short
                || value is uint || value is int
                || value is ulong || value is long;
        }

        private static bool IsPropertyNameMatchOption(string propertyName, string optionKey)
        {
            return propertyName.Equals(optionKey, StringComparison.InvariantCultureIgnoreCase)
                || optionKey.ToLowerInvariant().Contains(propertyName.ToLowerInvariant());
        }
    }
}
