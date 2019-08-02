using Aquality.Selenium.Utilities;
using System;

namespace Aquality.Selenium.Configurations
{
    public class TimeoutConfiguration : ITimeoutConfiguration
    {
        private readonly JsonFile settingsFile;

        public TimeoutConfiguration(JsonFile settingsFile)
        {
            this.settingsFile = settingsFile;
            Implicit = GetTimeout(nameof(Implicit));
            Script = GetTimeout(nameof(Script));
            PageLoad = GetTimeout(nameof(PageLoad));
            Condition = GetTimeout(nameof(Condition));
            PollingInterval = GetTimeout(nameof(PollingInterval));
        }

        private TimeSpan GetTimeout(string name)
        {
            return TimeSpan.FromSeconds(settingsFile.GetObject<int>($".timeouts.timeout{name}"));
        }

        public TimeSpan Implicit { get; }

        public TimeSpan Script { get; }

        public TimeSpan PageLoad { get; }

        public TimeSpan Condition { get; }

        public TimeSpan PollingInterval { get; }
    }
}
