using Aquality.Selenium.Localization;
using Aquality.Selenium.Logging;
using Aquality.Selenium.Utilities;

namespace Aquality.Selenium.Configurations
{
    /// <summary>
    /// Provides logger configuration.
    /// </summary>
    public class LoggerConfiguration : ILoggerConfiguration
    {
        private readonly JsonFile settingsFile;

        /// <summary>
        /// Instantiates class using JSON file with general settings.
        /// </summary>
        /// <param name="settingsFile">JSON settings file.</param>
        public LoggerConfiguration(JsonFile settingsFile)
        {
            this.settingsFile = settingsFile;
        }

        public SupportedLanguage Language
        {
            get
            {
                var loggerLang = settingsFile.GetValueOrDefault<string>(".logger.language");
                if (!loggerLang.TryParseToEnum<SupportedLanguage>(out var language))
                {
                    Logger.Instance.Warn($"Provided logger language '{loggerLang}' is not supported.");
                }
                return language;
            }
        }
    }
}
