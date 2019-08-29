using Aquality.Selenium.Browsers;
using NUnit.Framework;

[assembly: LevelOfParallelism(15)]
namespace Aquality.Selenium.Tests.Integration
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal class UITest
    {       
        [TearDown]
        public void CleanUp()
        {
            BrowserManager.Browser.Quit();
        }
    }
}
