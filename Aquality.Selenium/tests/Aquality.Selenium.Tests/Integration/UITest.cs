using Aquality.Selenium.Browsers;
using NUnit.Framework;

[assembly: LevelOfParallelism(10)]
namespace Aquality.Selenium.Tests.Integration
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal class UITest
    {
        [SetUp]
        public void SetUp()
        {
            BrowserManager.SetStartup(new BrowserStartup());
        }

        [TearDown]
        public void CleanUp()
        {
            BrowserManager.Browser.Quit();
        }
    }
}
