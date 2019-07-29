using OpenQA.Selenium;

namespace Aquality.Selenium.Configurations.WebDriverSettings
{
    public interface IDriverSettings
    {
        string WebDriverVersion { get; }

        DriverOptions DriverOptions { get; }

        string DownloadDir { get; }

        string DownloadDirCapabilityKey { get; }
    }
}
