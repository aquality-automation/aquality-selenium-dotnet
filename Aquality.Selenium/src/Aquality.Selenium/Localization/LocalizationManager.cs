using System;

namespace Aquality.Selenium.Localization
{
    /// <summary>
    /// This class is using for translation messages to different languages
    /// </summary>
    internal sealed class LocalizationManager
    {
        private const string DefLocale = "en";
        private const string LocaleKey = "locale";
        private const string LangResources = "Resources.Localization.{0}.json";
        private static readonly Lazy<LocalizationManager> LazyInstance = new Lazy<LocalizationManager>(() => new LocalizationManager());
        
        private LocalizationManager()
        {   
        }

        /// <summary>
        /// Gets LocalizationManager instance.
        /// </summary>
        public static LocalizationManager Instance => LazyInstance.Value;

        /// <summary>
        /// Get localized message from resources by its key.
        /// </summary>
        /// <param name="messageKey">Key in resources file.</param>
        /// <param name="args">Arguments, which will be provided to template of localized message.</param>
        /// <returns>Localized message.</returns>
        public string GetLocalizedMessage(string messageKey, params string [] args)
        {
            throw new NotImplementedException();
        }
    }
}
