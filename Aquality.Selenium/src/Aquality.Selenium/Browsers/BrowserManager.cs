using Aquality.Selenium.Elements;
using Aquality.Selenium.Elements.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Logging;
using System.Reflection;
using Aquality.Selenium.Core.Applications;
using Aquality.Selenium.Configurations;
using CoreElementFactory = Aquality.Selenium.Core.Elements.Interfaces.IElementFactory;
using CoreTimeoutConfiguration = Aquality.Selenium.Core.Configurations.ITimeoutConfiguration;
using ILoggerConfiguration = Aquality.Selenium.Core.Configurations.ILoggerConfiguration;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Controls browser instance creation.
    /// </summary>
    public class BrowserManager : ApplicationManager<BrowserManager, Browser>
    {
        private static readonly ThreadLocal<IBrowserFactory> BrowserFactoryContainer = new ThreadLocal<IBrowserFactory>();

        /// <summary>
        /// Gets and sets thread-safe instance of browser.
        /// </summary>
        /// <value>Instance of desired browser.</value>
        public static Browser Browser
        {
            get
            {
                return GetApplication(StartBrowserFunction, () => RegisterServices(services => Browser));
            }
            set
            {
                SetApplication(value);
            }
        }

        private static Func<IServiceProvider, Browser> StartBrowserFunction => services => BrowserFactory.Browser;

        public static IServiceProvider ServiceProvider
        {
            get
            {
                return GetServiceProvider(services => Browser, () => RegisterServices(services => Browser));
            }
            set
            {
                SetServiceProvider(value);
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
            set
            {
                BrowserFactoryContainer.Value = value;
            }
        }

        private static IServiceCollection RegisterServices(Func<IServiceProvider, Browser> browserSupplier)
        {
            var services = new ServiceCollection();
            var startup = new Startup();
            var settingsFile = startup.GetSettings();
            startup.ConfigureServices(services, browserSupplier, settingsFile);
            services.AddTransient<IElementFactory, ElementFactory>();
            services.AddTransient<CoreElementFactory, ElementFactory>();
            services.AddSingleton<ITimeoutConfiguration>(serviceProvider => new TimeoutConfiguration(settingsFile));
            services.AddSingleton<CoreTimeoutConfiguration>(serviceProvider => new TimeoutConfiguration(settingsFile));
            services.AddSingleton<IBrowserProfile>(serviceProvider => new BrowserProfile(settingsFile));
            services.AddSingleton(serviceProvider => new LocalizationManager(serviceProvider.GetRequiredService<ILoggerConfiguration>(), serviceProvider.GetRequiredService<Logger>(), Assembly.GetExecutingAssembly()));
            services.AddTransient(serviceProvider => BrowserFactory);
            return services;
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
    }
}
