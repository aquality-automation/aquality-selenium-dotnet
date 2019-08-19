using Aquality.Selenium.Utilities;
using System;

namespace Aquality.Selenium.Configurations
{
    /// <summary>
    /// Provides timeouts configuration.
    /// </summary>
    public class TimeoutConfiguration : ITimeoutConfiguration
    {
        private readonly JsonFile settingsFile;

        /// <summary>
        /// Instantiates class using JSON file with general settings.
        /// </summary>
        /// <param name="settingsFile">JSON settings file.</param>
        public TimeoutConfiguration(JsonFile settingsFile)
        {
            this.settingsFile = settingsFile;
            Implicit = GetTimeoutFromSeconds(nameof(Implicit));
            Script = GetTimeoutFromSeconds(nameof(Script));
            PageLoad = GetTimeoutFromSeconds(nameof(PageLoad));
            Condition = GetTimeoutFromSeconds(nameof(Condition));
            PollingInterval = TimeSpan.FromMilliseconds(GetIntFromTimeoutSettings(nameof(PollingInterval)));
            Command = GetTimeoutFromSeconds(nameof(Command));
        }

        private TimeSpan GetTimeoutFromSeconds(string name)
        {
            return TimeSpan.FromSeconds(GetIntFromTimeoutSettings(name));
        }

        private int GetIntFromTimeoutSettings(string name)
        {
            return settingsFile.GetValue<int>($".timeouts.timeout{name}");
        }

        public TimeSpan Implicit { get; }

        public TimeSpan Script { get; }

        public TimeSpan PageLoad { get; }

        public TimeSpan Condition { get; }

        public TimeSpan PollingInterval { get; }

        public TimeSpan Command { get; }
    }
}
