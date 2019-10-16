using System;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Abstract representation of <see cref="IBrowserFactory"/>.
    /// </summary>
    public abstract class BrowserFactory : IBrowserFactory
    {
        protected BrowserFactory(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        protected IServiceProvider ServiceProvider { get; }

        public abstract Browser Browser { get; }
    }
}
