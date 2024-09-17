using Aquality.Selenium.Configurations;
using Aquality.Selenium.Configurations.WebDriverSettings;
using Aquality.Selenium.Core.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using System;
using System.IO;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Aquality.Selenium.Core.Localization;
using System.Reflection;
using System.Text.RegularExpressions;
using Aquality.Selenium.Core.Logging;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Factory that creates instance of local Browser.
    /// </summary>
    public class LocalBrowserFactory : BrowserFactory
    {
        private const string HostAddressDefault = "::1";
        private const string DriverVersionVariableName = "SE_DRIVER_VERSION";
        private const string CurrentBrowserVersionPattern = "Current browser version is ([\\d,\\.]+)";

        public LocalBrowserFactory(IActionRetrier actionRetrier, IBrowserProfile browserProfile, ITimeoutConfiguration timeoutConfiguration, ILocalizedLogger localizedLogger)
            : base(actionRetrier, browserProfile, timeoutConfiguration, localizedLogger)
        {
        }

        protected override WebDriver Driver => DriverContext.Driver;

        protected override DriverContext DriverContext
        {
            get
            {
                var commandTimeout = TimeoutConfiguration.Command;
                var browserName = BrowserProfile.BrowserName;
                var driverSettings = BrowserProfile.DriverSettings;
                DriverContext driverCtx;
                switch (browserName)
                {
                    case BrowserName.Chrome:
                    case BrowserName.Yandex:
                        driverCtx = GetDriver<ChromeDriver>(() => ChromeDriverService.CreateDefaultService(),
                            (ChromeOptions)driverSettings.DriverOptions, commandTimeout);
                        break;
                    case BrowserName.Firefox:
                        Func<DriverService> geckoServiceProvider = () =>
                        {
                            var geckoService = FirefoxDriverService.CreateDefaultService();
                            geckoService.Host = ((FirefoxSettings)driverSettings).IsGeckoServiceHostDefaultEnabled
                                ? HostAddressDefault
                                : geckoService.Host;
                            return geckoService;
                        };

                        driverCtx = GetDriver<FirefoxDriver>(geckoServiceProvider, (FirefoxOptions)driverSettings.DriverOptions, commandTimeout);
                        break;
                    case BrowserName.IExplorer:
                        driverCtx = GetDriver<InternetExplorerDriver>(() => InternetExplorerDriverService.CreateDefaultService(),
                            (InternetExplorerOptions)driverSettings.DriverOptions, commandTimeout);
                        break;
                    case BrowserName.Edge:
                        driverCtx = GetDriver<EdgeDriver>(() => EdgeDriverService.CreateDefaultService(),
                            (EdgeOptions)driverSettings.DriverOptions, commandTimeout);
                        break;
                    case BrowserName.Opera:
                        var config = new OperaConfig();
                        var operaSettings = (OperaSettings)driverSettings;
                        var driverPath = new DriverManager().SetUpDriver(config, operaSettings.WebDriverVersion, operaSettings.SystemArchitecture);
                        driverCtx = GetDriver<ChromeDriver>(
                            () => ChromeDriverService.CreateDefaultService(Path.GetDirectoryName(driverPath), config.GetBinaryName()),
                            (ChromeOptions)driverSettings.DriverOptions, commandTimeout);
                        break;
                    case BrowserName.Safari:
                        driverCtx = GetDriver<SafariDriver>(() => SafariDriverService.CreateDefaultService(),
                            (SafariOptions)driverSettings.DriverOptions, commandTimeout);
                        break;
                    default:
                        throw new NotSupportedException($"Browser [{browserName}] is not supported.");
                }

                return driverCtx;
            }
        }

        private DriverContext GetDriver<T>(Func<DriverService> driverServiceProvider, DriverOptions driverOptions, TimeSpan commandTimeout) where T : WebDriver
        {
            var currentBrowserVersionRegex = new Regex(CurrentBrowserVersionPattern, RegexOptions.None, TimeoutConfiguration.Condition);
            try
            {
                var driverService = driverServiceProvider.Invoke();
                var driver = (T)Activator.CreateInstance(typeof(T), driverService, driverOptions, commandTimeout);
                var context = new DriverContext
                {
                    Driver = driver,
                    DriverService = driverService
                };
                return context;
            }
            catch (TargetInvocationException exception)
            when (exception.InnerException != null && currentBrowserVersionRegex.IsMatch(exception.InnerException.Message))
            {
                Logger.Instance.Debug(exception.InnerException.Message, exception);
                var currentVersion = currentBrowserVersionRegex.Match(exception.InnerException.Message).Groups[1].Value;
                Environment.SetEnvironmentVariable(DriverVersionVariableName, currentVersion);
                var driverService = driverServiceProvider.Invoke();
                var driver = (T)Activator.CreateInstance(typeof(T), driverService, driverOptions, commandTimeout);
                var context = new DriverContext
                {
                    Driver = driver,
                    DriverService = driverService
                };
                return context;
            }
        }
    }
}
