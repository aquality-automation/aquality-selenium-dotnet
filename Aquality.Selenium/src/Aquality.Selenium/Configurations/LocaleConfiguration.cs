using Aquality.Selenium.Utilities;

namespace Aquality.Selenium.Configurations
{
    /// <summary>
    /// Provides locale configuration.
    /// </summary>
    public class LocaleConfiguration : ILocaleConfiguration
    {
        private readonly JsonFile settingsFile;

        /// <summary>
        /// Instantiates class using JSON file with general settings.
        /// </summary>
        /// <param name="settingsFile">JSON settings file.</param>
        public LocaleConfiguration(JsonFile settingsFile)
        {
            this.settingsFile = settingsFile;
        }

        public string Language => settingsFile.GetObject<string>(".locale.lang");
    }
}
