using Aquality.Selenium.Configurations;
using Aquality.Selenium.Core.Applications;
using CoreElementFactory = Aquality.Selenium.Core.Elements.Interfaces.IElementFactory;
using Aquality.Selenium.Elements;
using Aquality.Selenium.Elements.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Logging;

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
        public static Browser Browser => GetApplication(StartBrowserFunction, () => RegisterServices(services => Browser));

        private static Func<IServiceProvider, Browser> StartBrowserFunction
        {
            get
            {
                if (!BrowserFactoryContainer.IsValueCreated)
                {
                    SetDefaultFactory();
                }

                return services => BrowserFactoryContainer.Value.Browser;
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
            var browserProfile = new BrowserProfile(settingsFile);
            services.AddSingleton(browserProfile.DriverSettings);
            services.AddSingleton<IBrowserProfile>(browserProfile);
            services.AddSingleton(new LocalizationManager(new LoggerConfiguration(settingsFile), Logger.Instance));
            return services;
        }

        /// <summary>
        /// Sets default factory responsible for browser creation.
        /// RemoteBrowserFactory if value set in configuration and LocalBrowserFactory otherwise.
        /// </summary>
        public static void SetDefaultFactory()
        {
            /*IConfiguration configuration = Configuration.Instance;
            IBrowserFactory browserFactory;
            if (configuration.BrowserProfile.IsRemote)
            {
                browserFactory = new RemoteBrowserFactory(configuration);
            }
            else
            {
                browserFactory = new LocalBrowserFactory(configuration);
            }
            SetFactory(browserFactory);*/
        }

        /// <summary>
        /// Sets custom browser factory.
        /// </summary>
        /// <param name="browserFactory">Custom implementation of <see cref="IBrowserFactory"/></param>
        public static void SetFactory(IBrowserFactory browserFactory)
        {
            BrowserFactoryContainer.Value = browserFactory;
        }
    }
}
