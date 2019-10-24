using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Configurations.WebDriverSettings;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Microsoft.Extensions.DependencyInjection;
using WebDriverManager.DriverConfigs;
using WebDriverManager.Helpers;
using System.IO;

namespace Aquality.Selenium.Tests.Integration.Usecases
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal class CustomBrowserFactoryTests
    {
        const string url = "https://www.google.com/";

        [Test]
        public void Should_BePossibleToUse_BrowserFromCustomFactory()
        {
            var browserFactory = new CustomLocalBrowserFactory();
            BrowserManager.Browser = browserFactory.Browser;
            BrowserManager.Browser.GoTo(url);
            Assert.AreEqual(url, BrowserManager.Browser.CurrentUrl);
        }

        [Test]
        public void Should_BePossibleToUse_CustomFactory()
        {
            var browserFactory = new CustomLocalBrowserFactory();
            BrowserManager.BrowserFactory = browserFactory;
            BrowserManager.Browser.GoTo(url);
            Assert.AreEqual(url, BrowserManager.Browser.CurrentUrl);
        }

        [TearDown]
        public void TearDown()
        {
            BrowserManager.Browser.Quit();
            BrowserManager.SetDefaultFactory();
        }

        public class CustomLocalBrowserFactory : BrowserFactory
        {
            private static readonly object WebDriverDownloadingLock = new object();

            public CustomLocalBrowserFactory() : base(BrowserManager.ServiceProvider)
            {
            }

            public override Browser Browser => CreateBrowser();

            private Browser CreateBrowser()
            {
                var browserProfile = ServiceProvider.GetRequiredService<IBrowserProfile>();
                var driverSettings = browserProfile.DriverSettings;
                SetUpDriver(new ChromeConfig(), driverSettings);
                var driver = new ChromeDriver((ChromeOptions)driverSettings.DriverOptions);
                return new Browser(driver, ServiceProvider);
            }

            private static void SetUpDriver(IDriverConfig driverConfig, IDriverSettings driverSettings)
            {
                var architecture = driverSettings.SystemArchitecture.Equals(Architecture.Auto) ? ArchitectureHelper.GetArchitecture() : driverSettings.SystemArchitecture;
                var version = driverSettings.WebDriverVersion.Equals("Latest") ? driverConfig.GetLatestVersion() : driverSettings.WebDriverVersion;
                var url = UrlHelper.BuildUrl(architecture.Equals(Architecture.X32) ? driverConfig.GetUrl32() : driverConfig.GetUrl64(), version);
                var binaryPath = FileHelper.GetBinDestination(driverConfig.GetName(), version, architecture, driverConfig.GetBinaryName());
                if (!File.Exists(binaryPath) || !Environment.GetEnvironmentVariable("PATH").Contains(binaryPath))
                {
                    lock (WebDriverDownloadingLock)
                    {
                        new DriverManager().SetUpDriver(url, binaryPath, driverConfig.GetBinaryName());
                    }
                }
            }
        }
    }
}
