using Aquality.Selenium.Logging;
using Aquality.Selenium.Utilities;
using System.Reflection;
using System.Threading;

namespace Aquality.Selenium.Configurations
{
    /// <summary>
    /// Provides tool configuration.
    /// </summary>
    public class Configuration : IConfiguration
    {
        private static readonly ThreadLocal<Configuration> InstanceHolder = new ThreadLocal<Configuration>();
        private Logger Logger => Logger.Instance;

        private Configuration()
        {
            var settings = GetSettings();
            BrowserProfile = new BrowserProfile(settings);
            TimeoutConfiguration = new TimeoutConfiguration(settings);
            RetryConfiguration = new RetryConfiguration(settings);
            LoggerConfiguration = new LoggerConfiguration(settings);
        }

        private JsonFile GetSettings()
        {
            var profileNameFromEnvironment = EnvironmentConfiguration.GetVariable("PROFILE");
            var settingsProfile = profileNameFromEnvironment == null ? "settings.json" : $"settings.{profileNameFromEnvironment}.json";
            Logger.Info($"Get settings from: {settingsProfile}");

            var jsonFile = FileReader.IsResourceFileExist(settingsProfile)
                ? new JsonFile(settingsProfile)
                : new JsonFile($"Resources.{settingsProfile}", Assembly.GetCallingAssembly());
            return jsonFile;
        }

        public IBrowserProfile BrowserProfile { get; }

        public ITimeoutConfiguration TimeoutConfiguration { get; }

        public IRetryConfiguration RetryConfiguration { get; }

        public ILoggerConfiguration LoggerConfiguration { get; }

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
