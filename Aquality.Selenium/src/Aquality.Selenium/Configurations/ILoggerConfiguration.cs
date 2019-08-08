using Aquality.Selenium.Localization;

namespace Aquality.Selenium.Configurations
{
    /// <summary>
    /// Describes logger configuration.
    /// </summary>
    public interface ILoggerConfiguration
    {
        /// <summary>
        /// Gets language of framework.
        /// </summary>
        /// <value>Supported language.</value>
        SupportedLanguage Language { get; }
    }
}
