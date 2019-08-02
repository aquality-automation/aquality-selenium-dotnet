using Aquality.Selenium.Utilities;
using OpenQA.Selenium;
using WebDriverManager.Helpers;

namespace Aquality.Selenium.Configurations.WebDriverSettings
{
    public abstract class DriverSettings : IDriverSettings
    {
        public DriverSettings(JsonFile settingsFile)
        {
            SettingsFile = settingsFile;
        }

        protected JsonFile SettingsFile { get; }

        public string WebDriverVersion { get; protected set; } = "Latest";

        public Architecture SystemArchitecture => Architecture.Auto;

        public abstract DriverOptions DriverOptions { get; }

        public abstract string DownloadDir { get; }

        public abstract string DownloadDirCapabilityKey { get; }
    }
}
