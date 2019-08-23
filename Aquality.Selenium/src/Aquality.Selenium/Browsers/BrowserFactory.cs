using Aquality.Selenium.Configurations;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Abstract representation of <see cref="IBrowserFactory"/>.
    /// </summary>
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
