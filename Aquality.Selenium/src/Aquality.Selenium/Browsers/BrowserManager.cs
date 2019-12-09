using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using Aquality.Selenium.Core.Applications;
using Aquality.Selenium.Configurations;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Controls browser instance creation.
    /// </summary>
    public class BrowserManager : ApplicationManager<Browser>
    {
        private static readonly ThreadLocal<BrowserStartup> BrowserStartupContainer = new ThreadLocal<BrowserStartup>(() => new BrowserStartup());
        private static readonly ThreadLocal<IBrowserFactory> BrowserFactoryContainer = new ThreadLocal<IBrowserFactory>();

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

        public static IServiceProvider ServiceProvider
        {
            get => GetServiceProvider(services => Browser, ConfigureServices);
            set => SetServiceProvider(value);
        }

        /// <summary>
        /// Method which allow user to override or add custom services.
        /// </summary>
        /// <param name="startup"><see cref="Startup"/>> object with custom or overriden services.</param>
        public static void SetStartup(Startup startup)
        {
            if (startup != null)
            {
                BrowserStartupContainer.Value = (BrowserStartup) startup;
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
                if (!BrowserFactoryContainer.IsValueCreated)
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
            var appProfile = GetRequiredService<IBrowserProfile>();
            IBrowserFactory applicationFactory;
            if (appProfile.IsRemote)
            {
                applicationFactory = new RemoteBrowserFactory(ServiceProvider);
            }
            else
            {
                applicationFactory = new LocalBrowserFactory(ServiceProvider);
            }

            BrowserFactory = applicationFactory;
        }

        /// <summary>
        /// Resolves required service from <see cref="ServiceProvider"/>
        /// </summary>
        /// <typeparam name="T">type of required service</typeparam>
        /// <exception cref="InvalidOperationException">Thrown if there is no service of the required type.</exception> 
        /// <returns></returns>
        public static T GetRequiredService<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }

        private static IServiceCollection ConfigureServices()
        {
            return BrowserStartupContainer.Value.ConfigureServices(new ServiceCollection(), services => Browser);
        }
    }
}
