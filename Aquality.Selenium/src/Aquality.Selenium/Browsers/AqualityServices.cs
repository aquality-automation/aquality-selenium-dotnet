using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Core.Applications;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Logging;
using Aquality.Selenium.Core.Waitings;
using Aquality.Selenium.Core.Utilities;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Controls browser instance creation.
    /// </summary>
    public class AqualityServices : AqualityServices<Browser>
    {
        private static readonly AsyncLocal<BrowserStartup> BrowserStartupContainer = new AsyncLocal<BrowserStartup>();
        private static readonly AsyncLocal<IBrowserFactory> BrowserFactoryContainer = new AsyncLocal<IBrowserFactory>();

        /// <summary>
        /// Check if browser already started.
        /// </summary>
        /// <value>true if browser started and false otherwise.</value>
        public static bool IsBrowserStarted => IsApplicationStarted();

        /// <summary>
        /// Gets registered instance of logger
        /// </summary>
        public static Logger Logger => Get<Logger>();

        /// <summary>
        /// Gets registered instance of localized logger
        /// </summary>
        public static ILocalizedLogger LocalizedLogger => Get<ILocalizedLogger>();

        /// <summary>
        /// Gets ConditionalWait object
        /// </summary>
        public static IConditionalWait ConditionalWait => Get<IConditionalWait>();

        /// <summary>
        /// Gets and sets thread-safe instance of browser.
        /// </summary>
        /// <value>Instance of desired browser.</value>
        public static Browser Browser
        {
            get => GetApplication(StartBrowserFunction, ConfigureServices);
            set => SetApplication(value);
        }

        private static Func<IServiceProvider, Browser> StartBrowserFunction => services => BrowserFactory.Browser;

        /// <summary>
        /// Method which allow user to override or add custom services.
        /// </summary>
        /// <param name="startup"><see cref="BrowserStartup"/>> object with custom or overriden services.</param>
        public static void SetStartup(BrowserStartup startup)
        {
            if (startup != null)
            {
                BrowserStartupContainer.Value = startup;
                SetServiceProvider(ConfigureServices().BuildServiceProvider());
            }
        }

        /// <summary>
        /// Factory for application creation.
        /// </summary>
        public static IBrowserFactory BrowserFactory
        {
            get
            {
                if (BrowserFactoryContainer.Value == null)
                {
                    SetDefaultFactory();
                }

                return BrowserFactoryContainer.Value;
            }
            set => BrowserFactoryContainer.Value = value;
        }

        /// <summary>
        /// Sets default factory responsible for browser creation.
        /// RemoteBrowserFactory if value set in configuration and LocalBrowserFactory otherwise.
        /// </summary>
        public static void SetDefaultFactory()
        {
            var appProfile = Get<IBrowserProfile>();
            IBrowserFactory applicationFactory;
            if (appProfile.IsRemote)
            {
                applicationFactory = new RemoteBrowserFactory(Get<IActionRetrier>(), Get<IBrowserProfile>(), Get<ITimeoutConfiguration>(), LocalizedLogger);
            }
            else
            {
                applicationFactory = new LocalBrowserFactory(Get<IActionRetrier>(), Get<IBrowserProfile>(), Get<ITimeoutConfiguration>(), LocalizedLogger);
            }

            BrowserFactory = applicationFactory;
        }

        /// <summary>
        /// Resolves required service from <see cref="ServiceProvider"/>
        /// </summary>
        /// <typeparam name="T">type of required service.</typeparam>
        /// <exception cref="InvalidOperationException">Thrown if there is no service of the required type.</exception> 
        /// <returns></returns>
        public static T Get<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }

        private static IServiceProvider ServiceProvider => GetServiceProvider(services => Browser, ConfigureServices);

        private static IServiceCollection ConfigureServices()
        {
            if (BrowserStartupContainer.Value == null)
            {
                BrowserStartupContainer.Value = new BrowserStartup();
            }
            return BrowserStartupContainer.Value.ConfigureServices(new ServiceCollection(), services => Browser);
        }
    }
}
