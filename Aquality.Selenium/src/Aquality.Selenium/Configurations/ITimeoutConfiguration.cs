using System;

namespace Aquality.Selenium.Configurations
{
    /// <summary>
    /// Describes timeouts configuration.
    /// </summary>
    public interface ITimeoutConfiguration
    {
        /// <summary>
        /// Gets WedDriver ImplicitWait timeout.
        /// </summary>
        /// <value>ImplicitWait timeout.</value>
        TimeSpan Implicit { get; }

        /// <summary>
        /// Gets WedDriver AsynchronousJavaScript timeout.
        /// </summary>
        /// <value>AsynchronousJavaScript timeout.</value>
        TimeSpan Script { get; }

        /// <summary>
        /// Gets WedDriver PageLoad timeout.
        /// </summary>
        /// <value>PageLoad timeout.</value>
        TimeSpan PageLoad { get; }

        /// <summary>
        /// Gets default ConditionalWait timeout.
        /// </summary>
        /// <value>ConditionalWait timeout.</value>
        TimeSpan Condition { get; }

        /// <summary>
        /// Gets ConditionalWait polling interfal.
        /// </summary>
        /// <value>ConditionalWait polling interfal.</value>
        TimeSpan PollingInterval { get; }
    }
}
