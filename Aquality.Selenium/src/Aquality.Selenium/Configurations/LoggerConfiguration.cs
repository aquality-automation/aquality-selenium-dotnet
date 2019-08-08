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

        public string Language => settingsFile.GetObject<string>(".logger.language");
    }
}
