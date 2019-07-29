using System;
using OpenQA.Selenium;

namespace Aquality.Selenium.Configurations.WebDriverSettings
{
    public class ChromeSettings : DriverSettings
    {
        public override DriverOptions DriverOptions => throw new NotImplementedException();

        public override string DownloadDir => throw new NotImplementedException();

        public override string DownloadDirCapabilityKey => throw new NotImplementedException();
    }
}
