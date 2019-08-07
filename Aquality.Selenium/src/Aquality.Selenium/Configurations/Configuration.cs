using Aquality.Selenium.Utilities;
using System.Threading;

namespace Aquality.Selenium.Configurations
{
    /// <summary>
    /// Provides tool configuration.
    /// </summary>
    public class Configuration : IConfiguration
    {
        private static readonly ThreadLocal<Configuration> InstanceHolder = new ThreadLocal<Configuration>();        

        private Configuration()
        {
            var settings = GetSettings();
            BrowserProfile = new BrowserProfile(settings);
            TimeoutConfiguration = new TimeoutConfiguration(settings);
            LocaleConfiguration = new LocaleConfiguration(settings);
        }

        private JsonFile GetSettings()
        {
            var profileNameFromEnvironment = EnvironmentConfiguration.GetVariable("profile");
            var settingsProfile = profileNameFromEnvironment == null ? "settings.json" : $"settings.{profileNameFromEnvironment}.json";
            return new JsonFile(settingsProfile);
        }

        public IBrowserProfile BrowserProfile { get; }

        public ITimeoutConfiguration TimeoutConfiguration { get; }

        public ILocaleConfiguration LocaleConfiguration { get; }

        /// <summary>
        /// Gets thread-safe instance of configuration.
        /// </summary>
        /// <value>Instance of configuration.</value>
        public static Configuration Instance
        {
            get
            {
                if (!InstanceHolder.IsValueCreated)
                {
                    InstanceHolder.Value = new Configuration();
                }
                return InstanceHolder.Value;
            }
        }
    }
}
