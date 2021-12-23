using Aquality.Selenium.Configurations;
using Aquality.Selenium.Configurations.WebDriverSettings;
using Aquality.Selenium.Core.Utilities;
using OpenQA.Selenium;
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
using EdgeChromiumOptions = Microsoft.Edge.SeleniumTools.EdgeOptions;
using EdgeChromiumService = Microsoft.Edge.SeleniumTools.EdgeDriverService;
using EdgeChromiumDriver = Microsoft.Edge.SeleniumTools.EdgeDriver;
using Aquality.Selenium.Core.Localization;
using OpenQA.Selenium.Opera;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Factory that creates instance of local Browser.
    /// </summary>
    public class LocalBrowserFactory : BrowserFactory
    {
        private static readonly object WebDriverDownloadingLock = new object();

        public LocalBrowserFactory(IActionRetrier actionRetrier, IBrowserProfile browserProfile, ITimeoutConfiguration timeoutConfiguration, ILocalizedLogger localizedLogger)
            : base(actionRetrier, browserProfile, timeoutConfiguration, localizedLogger)
        {
        }

        protected override RemoteWebDriver Driver
        {
            get
            {
                var commandTimeout = TimeoutConfiguration.Command;
                var browserName = BrowserProfile.BrowserName;
                var driverSettings = BrowserProfile.DriverSettings;
                RemoteWebDriver driver;
                switch (browserName)
                {
                    case BrowserName.Chrome:
                    case BrowserName.Yandex:
                        SetUpDriver(new ChromeConfig(), driverSettings);
                        driver = GetDriver<ChromeDriver>(ChromeDriverService.CreateDefaultService(),
                            (ChromeOptions)driverSettings.DriverOptions, commandTimeout);
                        break;
                    case BrowserName.Firefox:
                        SetUpDriver(new FirefoxConfig(), driverSettings);
                        var geckoService = FirefoxDriverService.CreateDefaultService();
                        geckoService.Host = "::1";
                        driver = GetDriver<FirefoxDriver>(geckoService, (FirefoxOptions)driverSettings.DriverOptions, commandTimeout);
                        break;
                    case BrowserName.IExplorer:
                        SetUpDriver(new InternetExplorerConfig(), driverSettings);
                        driver = GetDriver<InternetExplorerDriver>(InternetExplorerDriverService.CreateDefaultService(),
                            (InternetExplorerOptions)driverSettings.DriverOptions, commandTimeout);
                        break;
                    case BrowserName.Edge:
                        driver = GetDriver<EdgeDriver>(EdgeDriverService.CreateDefaultService(),
                            (EdgeOptions)driverSettings.DriverOptions, commandTimeout);
                        break;
                    case BrowserName.EdgeChromium:
                        SetUpDriver(new EdgeConfig(), driverSettings);
                        driver = GetDriver<EdgeChromiumDriver>(EdgeChromiumService.CreateChromiumService(),
                            (EdgeChromiumOptions)driverSettings.DriverOptions, commandTimeout);
                        break;
                    case BrowserName.Opera:
                        SetUpDriver(new OperaConfig(), driverSettings);
                        driver = GetDriver<OperaDriver>(OperaDriverService.CreateDefaultService(),
                            (OperaOptions)driverSettings.DriverOptions, commandTimeout);
                        break;
                    case BrowserName.Safari:
                        driver = GetDriver<SafariDriver>(SafariDriverService.CreateDefaultService(),
                            (SafariOptions)driverSettings.DriverOptions, commandTimeout);
                        break;
                    default:
                        throw new NotSupportedException($"Browser [{browserName}] is not supported.");
                }
                return driver;
            }
        }

        private RemoteWebDriver GetDriver<T>(DriverService driverService, DriverOptions driverOptions, TimeSpan commandTimeout) where T : RemoteWebDriver
        {
            return (T) Activator.CreateInstance(typeof(T), driverService, driverOptions, commandTimeout);
        }

        private static void SetUpDriver(IDriverConfig driverConfig, IDriverSettings driverSettings)
        {
            var architecture = driverSettings.SystemArchitecture.Equals(Architecture.Auto) ? ArchitectureHelper.GetArchitecture() : driverSettings.SystemArchitecture;
            var version = driverSettings.WebDriverVersion.Equals(VersionResolveStrategy.Latest) ? driverConfig.GetLatestVersion() : driverSettings.WebDriverVersion;
            version = version.Equals(VersionResolveStrategy.MatchingBrowser) ? driverConfig.GetMatchingBrowserVersion() : version;
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
