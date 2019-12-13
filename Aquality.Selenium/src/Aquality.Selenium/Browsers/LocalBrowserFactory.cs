using Aquality.Selenium.Configurations;
using Aquality.Selenium.Configurations.WebDriverSettings;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using System;
using System.IO;
using WebDriverManager;
using WebDriverManager.DriverConfigs;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Factory that creates instance of local Browser.
    /// </summary>
    public class LocalBrowserFactory : BrowserFactory
    {
        private static readonly object WebDriverDownloadingLock = new object();

        public LocalBrowserFactory() : base()
        {
        }

        public override Browser Browser => CreateBrowser();

        private Browser CreateBrowser()
        {
            var browserProfile = AqualityServices.Get<IBrowserProfile>();
            var browserName = browserProfile.BrowserName;
            var driverSettings = browserProfile.DriverSettings;
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
            return new Browser(driver);
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
