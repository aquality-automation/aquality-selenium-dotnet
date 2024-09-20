using OpenQA.Selenium;

namespace Aquality.Selenium.Browsers
{
    public class DriverContext
    {
        public DriverContext(WebDriver driver, DriverService driverService)
        {
            Driver = driver;
            DriverService = driverService;
        }

        public WebDriver Driver { get; }
        public DriverService DriverService { get; }
    }
}
