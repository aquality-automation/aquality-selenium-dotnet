using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations.WebDriverSettings;
using System;

namespace Aquality.Selenium.Configurations
{
    public interface IBrowserProfile
    {
        BrowserName BrowserName { get; }

        bool IsRemote { get; }

        Uri RemoteConnectionUrl { get; }

        bool IsElementHighlightEnabled { get; }

        IDriverSettings DriverSettings { get; }
    }
}
