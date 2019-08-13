namespace Aquality.Selenium.Configurations
{
    /// <summary>
    /// Describes tool configuration.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Gets desired browser profile.
        /// </summary>
        /// <value>Profile of browser.</value>
        IBrowserProfile BrowserProfile { get; }

        /// <summary>
        /// Gets configuration of timeouts.
        /// </summary>
        /// <value>Configuration of timeouts.</value>
        ITimeoutConfiguration TimeoutConfiguration { get; }

        /// <summary>
        /// Gets configuration of retries.
        /// </summary>
        /// <value>Configuration of retries.</value>
        IRetryConfiguration RetryConfiguration { get; }

        /// <summary>
        /// Gets configuration of logger.
        /// </summary>
        /// <value>Configuration of logger.</value>
        ILoggerConfiguration LoggerConfiguration { get; }
    }
}
