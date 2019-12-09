using System;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Applications;
using Aquality.Selenium.Core.Configurations;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Aquality.Selenium.Tests.Unit.Configuration
{
    [TestFixture]
    [Parallelizable(ParallelScope.None)]
    internal class CustomConfigurationTests
    {
        private const string SpecialLoggerLanguage = "SpecialLoggerLanguage";
        private const string SpecialCustomValue = "SpecialCustomValue";
        private static readonly TimeSpan DefaultCommandTimeout = TimeSpan.FromSeconds(60);

        [SetUp]
        public static void Before()
        {
            BrowserManager.SetStartup(new CustomStartup());
        }

        [Test]
        public void Should_BeAbleOverrideDependencies_AndGetCustomService()
        {
            Assert.AreEqual(SpecialLoggerLanguage, BrowserManager.GetRequiredService<ILoggerConfiguration>().Language, "Configuration value should be overriden.");
        }

        [Test]
        public void Should_BeAbleAdd_CustomService()
        {
            Assert.AreEqual(SpecialCustomValue, BrowserManager.GetRequiredService<ICustomService>().CustomValue, "Custom service should have value");
        }

        [Test]
        public void Should_BeAbleGet_DefaultService()
        {
            Assert.AreEqual(DefaultCommandTimeout, BrowserManager.GetRequiredService<ITimeoutConfiguration>().Command, "Default service value should have default value");
        }

        [TearDown]
        public static void After()
        {
            BrowserManager.SetStartup(new BrowserStartup());
        }

        private class CustomLoggerConfiguration : ILoggerConfiguration
        {
            public string Language { get; } = SpecialLoggerLanguage;
        }

        private interface ICustomService
        {
            string CustomValue { get; }
        }

        private class CustomService : ICustomService
        {
            public string CustomValue { get; } = SpecialCustomValue;
        }

        private class CustomStartup : BrowserStartup
        {
            public override IServiceCollection ConfigureServices(IServiceCollection services, Func<IServiceProvider, IApplication> applicationProvider, ISettingsFile settings = null)
            {
                settings = GetSettings();
                base.ConfigureServices(services, applicationProvider, settings);
                services.AddSingleton<ILoggerConfiguration>(new CustomLoggerConfiguration());
                services.AddTransient<ICustomService, CustomService>();
                return services;
            }
        }
    }
}
