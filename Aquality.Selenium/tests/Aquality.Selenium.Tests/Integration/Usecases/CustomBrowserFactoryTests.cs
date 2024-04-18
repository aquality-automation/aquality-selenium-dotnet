﻿using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using System;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;
using System.IO;
using Aquality.Selenium.Core.Utilities;
using OpenQA.Selenium;

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
            AqualityServices.Browser = browserFactory.Browser;
            AqualityServices.Browser.GoTo(url);
            Assert.That(AqualityServices.Browser.CurrentUrl, Is.EqualTo(url));
        }

        [Test]
        public void Should_BePossibleToUse_CustomFactory()
        {
            var browserFactory = new CustomLocalBrowserFactory();
            AqualityServices.BrowserFactory = browserFactory;
            AqualityServices.Browser.GoTo(url);
            Assert.That(AqualityServices.Browser.CurrentUrl, Is.EqualTo(url));
        }

        [TearDown]
        public void TearDown()
        {
            AqualityServices.Browser.Quit();
            AqualityServices.SetDefaultFactory();
        }

        public class CustomLocalBrowserFactory : BrowserFactory
        {
            private static readonly object WebDriverDownloadingLock = new();

            public CustomLocalBrowserFactory() : 
                base(AqualityServices.Get<IActionRetrier>(), AqualityServices.Get<IBrowserProfile>(), AqualityServices.Get<ITimeoutConfiguration>(), AqualityServices.LocalizedLogger)
            {
            }

            protected override WebDriver Driver
            {
                get
                {
                    var driverSettings = BrowserProfile.DriverSettings;
                    SetUpDriver(new());
                    return new ChromeDriver((ChromeOptions)driverSettings.DriverOptions);
                }
            }
            
            private static void SetUpDriver(ChromeConfig driverConfig)
            {
                var architecture = ArchitectureHelper.GetArchitecture();
                var version = driverConfig.GetMatchingBrowserVersion();
                var url = UrlHelper.BuildUrl(architecture.Equals(Architecture.X32) ? driverConfig.GetUrl32() : driverConfig.GetUrl64(), version);
                var binaryPath = FileHelper.GetBinDestination(driverConfig.GetName(), version, architecture, driverConfig.GetBinaryName());
                if (!File.Exists(binaryPath) || !Environment.GetEnvironmentVariable("PATH").Contains(binaryPath))
                {
                    lock (WebDriverDownloadingLock)
                    {
                        new DriverManager().SetUpDriver(url, binaryPath);
                    }
                }
            }
        }
    }
}
