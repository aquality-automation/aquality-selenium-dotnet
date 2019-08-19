using System;

namespace Aquality.Selenium.Configurations
{
    /// <summary>
    /// Describes timeouts configuration.
    /// </summary>
    public interface ITimeoutConfiguration
    {
        /// <summary>
        /// Gets WedDriver ImplicitWait timeout (in seconds).
        /// </summary>
        TimeSpan Implicit { get; }

        /// <summary>
        /// Gets WedDriver AsynchronousJavaScript timeout (in seconds).
        /// </summary>
        TimeSpan Script { get; }

        /// <summary>
        /// Gets WedDriver PageLoad timeout (in seconds).
        /// </summary>
        TimeSpan PageLoad { get; }

        /// <summary>
        /// Gets default ConditionalWait timeout (in seconds).
        /// </summary>
        TimeSpan Condition { get; }

        /// <summary>
        /// Gets ConditionalWait polling interfal (in milliseconds).
        /// </summary>
        TimeSpan PollingInterval { get; }

        /// <summary>
        /// Gets Command timeout (in seconds).
        /// </summary>
        TimeSpan Command { get; }
    }
}
