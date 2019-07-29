using System;

namespace Aquality.Selenium.Configurations
{
    public class Configuration : IConfiguration
    {
        public IBrowserProfile BrowserProfile => throw new NotImplementedException();

        public ITimeoutConfiguration TimeoutConfiguration => throw new NotImplementedException();                       

        public static Configuration Instance => throw new NotImplementedException();
    }
}
