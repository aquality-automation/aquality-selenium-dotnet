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
    }
}
