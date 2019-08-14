using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Constants;
using NUnit.Framework;

[assembly: LevelOfParallelism(2)]
namespace Aquality.Selenium.Tests.UITests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class UITest
    {
        [SetUp]
        public void SetUp()
        {
            BrowserManager.Browser.Maximize();
            BrowserManager.Browser.Navigate().GoToUrl(TestConstants.UrlTheInternet);
        }

        [TearDown]
        public void CleanUp()
        {
            BrowserManager.Browser.Quit();
        }
    }
}
