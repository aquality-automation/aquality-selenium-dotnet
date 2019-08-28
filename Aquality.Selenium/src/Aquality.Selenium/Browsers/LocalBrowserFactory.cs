﻿using Aquality.Selenium.Configurations;
using Aquality.Selenium.Configurations.WebDriverSettings;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using System;
using System.IO;
using System.Linq;
using WebDriverManager;
using WebDriverManager.DriverConfigs;
using WebDriverManager.DriverConfigs.Impl;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Factory that creates instance of local Browser.
    /// </summary>
    public class LocalBrowserFactory : BrowserFactory
    {
        private static readonly object webDriverDownloadingLock = new object();

        public LocalBrowserFactory(IConfiguration configuration) : base(configuration)
        {
        }

        public override Browser Browser => CreateBrowser();

        private Browser CreateBrowser()
        {
            var browserName = Configuration.BrowserProfile.BrowserName;
            var driverSettings = Configuration.BrowserProfile.DriverSettings;
            RemoteWebDriver driver;
            switch (browserName)
            {
                case BrowserName.Chrome:
                    SetUpDriver(new ChromeConfig(), driverSettings);                    
                    driver = new ChromeDriver((ChromeOptions)driverSettings.DriverOptions);
                    break;
                case BrowserName.Firefox:
                    SetUpDriver(new FirefoxConfig(), driverSettings);
                    driver = new FirefoxDriver((FirefoxOptions)driverSettings.DriverOptions);
                    break;
                case BrowserName.IExplorer:
                    SetUpDriver(new InternetExplorerConfig(), driverSettings);
                    driver = new InternetExplorerDriver((InternetExplorerOptions)driverSettings.DriverOptions);
                    break;
                case BrowserName.Edge:
                    driver = new EdgeDriver((EdgeOptions)driverSettings.DriverOptions);
                    break;
                case BrowserName.Safari:
                    driver = new SafariDriver((SafariOptions)driverSettings.DriverOptions);
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"Browser {browserName} is not supported.");
            }
            return new Browser(driver, Configuration);
        }

        private static void SetUpDriver(IDriverConfig driverConfig, IDriverSettings driverSettings)
        {
            var driverPath = Path.Combine(Environment.GetEnvironmentVariable("PATH").Split(Path.PathSeparator).First(), driverConfig.GetBinaryName());
            if (!File.Exists(driverPath))
            {
                lock (webDriverDownloadingLock)
                {
                    new DriverManager().SetUpDriver(driverConfig, driverSettings.WebDriverVersion, driverSettings.SystemArchitecture);
                }
            }
        }
    }
}
