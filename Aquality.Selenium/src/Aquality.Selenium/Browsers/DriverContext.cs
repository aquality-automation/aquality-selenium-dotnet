#nullable enable
using OpenQA.Selenium;

namespace Aquality.Selenium.Browsers
{
    public class DriverContext
    {
        public WebDriver Driver { get; set; }
        public DriverService? DriverService { get; set; }
    }
}