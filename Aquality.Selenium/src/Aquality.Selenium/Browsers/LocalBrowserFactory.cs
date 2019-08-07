using Aquality.Selenium.Configurations;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using System;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Factory that creates instance of local Browser.
    /// </summary>
    public class LocalBrowserFactory : BrowserFactory
    {
        public LocalBrowserFactory(IConfiguration configuration) : base(configuration)
        {
        }

        public override Browser Browser => CreateBrowser();

        private Browser CreateBrowser()
        {
            var browserName = Configuration.BrowserProfile.BrowserName;
            var driverSettings = Configuration.BrowserProfile.DriverSettings;
            var driverManager = new DriverManager();
            RemoteWebDriver driver;
            switch (browserName)
            {
                case BrowserName.Chrome:
                    driverManager.SetUpDriver(new ChromeConfig(), driverSettings.WebDriverVersion, driverSettings.SystemArchitecture);
                    driver = new ChromeDriver((ChromeOptions)driverSettings.DriverOptions);
                    break;
                case BrowserName.Firefox:
                    driverManager.SetUpDriver(new FirefoxConfig(), driverSettings.WebDriverVersion, driverSettings.SystemArchitecture);
                    driver = new FirefoxDriver((FirefoxOptions)driverSettings.DriverOptions);
                    break;
                case BrowserName.IExplorer:
                    driverManager.SetUpDriver(new InternetExplorerConfig(), driverSettings.WebDriverVersion, driverSettings.SystemArchitecture);
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
    }
}
