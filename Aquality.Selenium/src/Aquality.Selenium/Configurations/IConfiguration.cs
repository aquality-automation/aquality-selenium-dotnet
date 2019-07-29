namespace Aquality.Selenium.Configurations
{
    public interface IConfiguration
    {
        IBrowserProfile BrowserProfile { get; }

        ITimeoutConfiguration TimeoutConfiguration { get; }
    }
}
