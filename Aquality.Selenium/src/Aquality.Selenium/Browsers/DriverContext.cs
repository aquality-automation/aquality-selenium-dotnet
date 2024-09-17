#nullable enable
using OpenQA.Selenium;

namespace Aquality.Selenium.Browsers
{
    public class DriverContext
    {
        public DriverContext(WebDriver driver)
        {
            Driver = driver;
        }

        public WebDriver Driver { get; }
        public DriverService? DriverService { get; set; }
    }
}
