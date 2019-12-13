using Aquality.Selenium.Elements;
using Aquality.Selenium.Elements.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Logging;
using System.Reflection;
using Aquality.Selenium.Core.Applications;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Core.Configurations;
using CoreElementFactory = Aquality.Selenium.Core.Elements.Interfaces.IElementFactory;
using CoreTimeoutConfiguration = Aquality.Selenium.Core.Configurations.ITimeoutConfiguration;
using ILoggerConfiguration = Aquality.Selenium.Core.Configurations.ILoggerConfiguration;
using ITimeoutConfiguration = Aquality.Selenium.Configurations.ITimeoutConfiguration;
using TimeoutConfiguration = Aquality.Selenium.Configurations.TimeoutConfiguration;

namespace Aquality.Selenium.Browsers
{
    public class BrowserStartup : Startup
    {
        public override IServiceCollection ConfigureServices(IServiceCollection services, Func<IServiceProvider, IApplication> applicationProvider,
            ISettingsFile settings = null)
        {
            settings = settings ?? GetSettings();
            base.ConfigureServices(services, applicationProvider, settings);
            services.AddTransient<IElementFactory, ElementFactory>();
            services.AddTransient<CoreElementFactory, ElementFactory>();
            services.AddSingleton<ITimeoutConfiguration>(serviceProvider => new TimeoutConfiguration(settings));
            services.AddSingleton<CoreTimeoutConfiguration>(serviceProvider => new TimeoutConfiguration(settings));
            services.AddSingleton<IBrowserProfile>(serviceProvider => new BrowserProfile(settings));
            services.AddSingleton(serviceProvider => new LocalizationManager(serviceProvider.GetRequiredService<ILoggerConfiguration>(), serviceProvider.GetRequiredService<Logger>(), Assembly.GetExecutingAssembly()));
            services.AddTransient(serviceProvider => AqualityServices.BrowserFactory);
            return services;
        }
    }
}
