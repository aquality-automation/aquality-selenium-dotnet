using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Integration.TestApp;
using NUnit.Framework;
using System.Drawing;

[assembly: LevelOfParallelism(1)]
namespace Aquality.Selenium.Tests.Integration
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal class UITest
    {
        protected Size DefaultWindowSize => new Size(1024, 768);

        [SetUp]
        public void SetUp()
        {
            BrowserManager.Browser.GoTo(Constants.UrlTheInternet);
            BrowserManager.Browser.SetWindowSize(DefaultWindowSize.Width, DefaultWindowSize.Height);
        }

        [TearDown]
        public void CleanUp()
        {
            BrowserManager.Browser.Quit();
        }
    }
}
