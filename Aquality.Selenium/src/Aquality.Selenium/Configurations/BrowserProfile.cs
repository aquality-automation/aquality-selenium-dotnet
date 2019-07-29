using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations.WebDriverSettings;
using System;

namespace Aquality.Selenium.Configurations
{
    public abstract class BrowserProfile : IBrowserProfile
    {
        public BrowserName BrowserName => throw new NotImplementedException();

        public bool IsRemote => throw new NotImplementedException();

        public Uri RemoteConnectionUrl => throw new NotImplementedException();

        public bool IsElementHighlightEnabled => throw new NotImplementedException();

        public IDriverSettings DriverSettings => throw new NotImplementedException();
    }
}
