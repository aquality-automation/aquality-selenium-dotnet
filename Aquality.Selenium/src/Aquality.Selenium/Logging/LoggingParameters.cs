namespace Aquality.Selenium.Logging
{
    /// <summary>
    /// Logging parameters for specific features, such as HTTP Exchange.
    /// </summary>
    public struct LoggingParameters
    {
        /// <summary>
        /// Whether feature logging should be enabled or not.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Logging level for the specific feature.
        /// </summary>
        public LogLevel LogLevel { get; set; }
    }
}
