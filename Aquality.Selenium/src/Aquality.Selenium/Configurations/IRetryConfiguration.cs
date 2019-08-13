using System;

namespace Aquality.Selenium.Configurations
{
    /// <summary>
    /// Describes retry configuration.
    /// </summary>
    public interface IRetryConfiguration
    {
        /// <summary>
        /// Gets the number of attempts during retry.
        /// </summary>
        /// <value>Number of retry attempts.</value>
        int Number { get; }

        /// <summary>
        /// Gets the polling interval used in retry.
        /// </summary>
        /// <value>Polling interval for retry.</value>
        TimeSpan PollingInterval { get; }
    }
}
