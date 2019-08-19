using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Configurations.WebDriverSettings;
using Aquality.Selenium.Localization;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace Aquality.Selenium.Tests.Usecases.BrowserFactory
{
    [TestFixture]
    internal class CustomBrowserFactoryTests
    {
        [Test]
        public void Should_BePossibleToUse_CustomBrowser()
        {
            var browserFactory = new CustomLocalBrowserFactory();
            BrowserManager.Browser = browserFactory.Browser;
            Assert.AreEqual(BrowserManager.Browser.DownloadDirectory, new CustomChromeSettings().DownloadDir);
        }

        [Test]
        public void Should_BePossibleToUse_CustomFactory()
        {
            var browserFactory = new CustomLocalBrowserFactory();
            BrowserManager.SetFactory(browserFactory);
            Assert.AreEqual(BrowserManager.Browser.DownloadDirectory, new CustomChromeSettings().DownloadDir);
        }

        [TearDown]
        public void TearDown()
        {
            BrowserManager.Browser.Quit();
            BrowserManager.SetDefaultFactory();
        }
    }

    class CustomLocalBrowserFactory : IBrowserFactory
    {
        public Browser Browser
        {
            get
            {
                new DriverManager().SetUpDriver(new ChromeConfig());
                var chromeDriver = new ChromeDriver();
                return new Browser(chromeDriver, new CustomConfiguration());
            }
        }
    }

    class CustomLocalBrowserFactoryWithOverridenConfig : IBrowserFactory
    {
        public Browser Browser
        {
            get
            {
                new DriverManager().SetUpDriver(new ChromeConfig());
                var chromeDriver = new ChromeDriver();
                return new Browser(chromeDriver, new CustomConfiguration());
            }
        }
    }

    class CustomConfiguration : IConfiguration
    {
        public IBrowserProfile BrowserProfile => new CustomBrowserProfile();

        public ITimeoutConfiguration TimeoutConfiguration => new CustomTimeoutConfiguration();

        public IRetryConfiguration RetryConfiguration => new CustomRetryConfiguration();

        public ILoggerConfiguration LoggerConfiguration => new LoggerConfiguration();

    }

    class CustomTimeoutConfiguration : ITimeoutConfiguration
    {
        public TimeSpan Implicit => TimeSpan.FromSeconds(10);

        public TimeSpan Script => TimeSpan.FromSeconds(5);

        public TimeSpan PageLoad => TimeSpan.FromSeconds(15);

        public TimeSpan Condition => TimeSpan.FromSeconds(15);

        public TimeSpan PollingInterval => TimeSpan.FromMilliseconds(100);
    }

    class LoggerConfiguration : ILoggerConfiguration
    {
        public SupportedLanguage Language => SupportedLanguage.EN;
    }

    class CustomRetryConfiguration : IRetryConfiguration
    {
        public int Number => 1;

        public TimeSpan PollingInterval => TimeSpan.FromMilliseconds(200);
    }

    class CustomBrowserProfile : IBrowserProfile
    {
        public BrowserName BrowserName => BrowserName.Chrome;

        public bool IsRemote => false;

        public Uri RemoteConnectionUrl => new Uri(string.Empty);

        public bool IsElementHighlightEnabled => false;

        public IDriverSettings DriverSettings => new CustomChromeSettings();
    }

    class CustomChromeSettings : IDriverSettings
    {
        public string WebDriverVersion => "Latest";

        public Architecture SystemArchitecture => Architecture.Auto;

        public DriverOptions DriverOptions => new ChromeOptions();

        public string DownloadDir => "custom download dir";

        public string DownloadDirCapabilityKey => "key";
    }
}
