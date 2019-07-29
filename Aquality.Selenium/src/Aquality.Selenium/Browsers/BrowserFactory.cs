using Aquality.Selenium.Configurations;

namespace Aquality.Selenium.Browsers
{
    public abstract class BrowserFactory : IBrowserFactory
    {
        protected BrowserFactory(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected IConfiguration Configuration { get; }

        public abstract Browser Browser { get; }
    }
}
