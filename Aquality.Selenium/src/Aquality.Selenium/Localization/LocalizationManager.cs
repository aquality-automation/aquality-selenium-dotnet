using Aquality.Selenium.Configurations;
using Aquality.Selenium.Logging;
using Aquality.Selenium.Utilities;
using System;
using System.Reflection;

namespace Aquality.Selenium.Localization
{
    /// <summary>
    /// This class is using for translation messages to different languages
    /// </summary>
    internal sealed class LocalizationManager
    {
        private const string LangResource = "Resources.Localization.{0}.json";
        private readonly JsonFile localManager;
        private static readonly Lazy<LocalizationManager> LazyInstance = new Lazy<LocalizationManager>(() => new LocalizationManager());

        private LocalizationManager()
        {
            var language = Configuration.Instance.LoggerConfiguration.Language;
            localManager = new JsonFile(string.Format(LangResource, language.ToString().ToLower()), Assembly.GetCallingAssembly());
        }

        /// <summary>
        /// Gets LocalizationManager instance.
        /// </summary>
        public static LocalizationManager Instance => LazyInstance.Value;

        /// <summary>
        /// Get localized message from resources by its key.
        /// </summary>
        /// <param name="messageKey">Key in resource file.</param>
        /// <param name="args">Arguments, which will be provided to template of localized message.</param>
        /// <returns>Localized message.</returns>
        public string GetLocalizedMessage(string messageKey, params string [] args)
        {
            var jsonKey = $"$['{messageKey}']";
            if (localManager.IsValuePresent(jsonKey))
            {
                return string.Format(localManager.GetValue<string>(jsonKey), args);
            }

            Logger.Instance.Debug($"Cannot find localized message by key '{jsonKey}'");
            return default;
        }
    }
}
