using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Integration.TestApp;
using NUnit.Framework;

[assembly: LevelOfParallelism(1)]
namespace Aquality.Selenium.Tests.Integration
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal class UITest
    {
        [SetUp]
        public void SetUp()
        {
            BrowserManager.Browser.Maximize();
            BrowserManager.Browser.Navigate().GoToUrl(Constants.UrlTheInternet);
        }

        [TearDown]
        public void CleanUp()
        {
            BrowserManager.Browser.Quit();
        }
    }
}
