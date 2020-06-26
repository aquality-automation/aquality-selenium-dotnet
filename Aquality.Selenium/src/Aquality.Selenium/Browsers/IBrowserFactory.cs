namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Factory that creates instance of desired Browser based on <see cref="Configurations.IBrowserProfile"/>.
    /// </summary>
    public interface IBrowserFactory
    {
        /// <summary>
        /// Creates instance of Browser.
        /// </summary>
        /// <value>Instance of desired Browser.</value>
        Browser Browser { get; }
    }
}
