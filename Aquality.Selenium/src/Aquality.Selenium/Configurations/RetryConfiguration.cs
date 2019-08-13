using Aquality.Selenium.Utilities;
using System;

namespace Aquality.Selenium.Configurations
{
    /// <summary>
    /// Provides retry configuration.
    /// </summary>
    public class RetryConfiguration : IRetryConfiguration
    {
        private readonly JsonFile settingsFile;

        /// <summary>
        /// Instantiates class using JSON file with general settings.
        /// </summary>
        /// <param name="settingsFile">JSON settings file.</param>
        public RetryConfiguration(JsonFile settingsFile)
        {
            this.settingsFile = settingsFile;

            Number = GetIntFromSettings(nameof(Number).ToLowerInvariant());
            PollingInterval = TimeSpan.FromMilliseconds(GetIntFromSettings("pollingInterval"));
        }

        private int GetIntFromSettings(string name)
        {
            return settingsFile.GetValue<int>($".retry.{name}");;
        }

        public int Number { get; }

        public TimeSpan PollingInterval { get; }
    }
}
