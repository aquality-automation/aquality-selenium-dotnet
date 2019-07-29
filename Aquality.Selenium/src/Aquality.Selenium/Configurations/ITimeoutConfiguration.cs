using System;

namespace Aquality.Selenium.Configurations
{
    public interface ITimeoutConfiguration
    {
        TimeSpan Implicit { get; }

        TimeSpan Script { get; }

        TimeSpan PageLoad { get; }

        TimeSpan Condition { get; }

        TimeSpan PollingInterval { get; }
    }
}
