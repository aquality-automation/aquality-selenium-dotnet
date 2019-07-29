using OpenQA.Selenium;

namespace Aquality.Selenium.Configurations.WebDriverSettings
{
    public abstract class DriverSettings : IDriverSettings
    {
        public string WebDriverVersion { get; protected set; } = "Latest";

        public abstract DriverOptions DriverOptions { get; }

        public abstract string DownloadDir { get; }

        public abstract string DownloadDirCapabilityKey { get; }
    }
}
