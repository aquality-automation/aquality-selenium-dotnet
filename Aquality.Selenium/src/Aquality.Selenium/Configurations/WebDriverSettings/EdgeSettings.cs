using System;
using Aquality.Selenium.Utilities;
using OpenQA.Selenium;

namespace Aquality.Selenium.Configurations.WebDriverSettings
{
    public class EdgeSettings : DriverSettings
    {
        public EdgeSettings(JsonFile settingsFile) : base(settingsFile)
        {
        }

        public override DriverOptions DriverOptions => throw new NotImplementedException();

        public override string DownloadDir => throw new NotImplementedException();

        public override string DownloadDirCapabilityKey => throw new NotImplementedException();
    }
}
