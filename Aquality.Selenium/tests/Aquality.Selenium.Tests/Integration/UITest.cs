using Aquality.Selenium.Browsers;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System.IO;

[assembly: LevelOfParallelism(10)]
namespace Aquality.Selenium.Tests.Integration
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal class UITest
    {
        protected const int RetriesCount = 5;
        protected const string RetriesGroup = "Flaky";

        [TearDown]
        public void CleanUp()
        {
            if (AqualityServices.IsBrowserStarted)
            {
                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
                {
                    var screenshotPng = "screenshot.png";
                    AqualityServices.Browser.Driver.GetScreenshot().SaveAsFile(screenshotPng);
                    TestContext.AddTestAttachment(screenshotPng);
                    var sourceHtml = "sourceHtml.txt";  
                    File.WriteAllText(sourceHtml, AqualityServices.Browser.Driver.PageSource);
                    TestContext.AddTestAttachment(sourceHtml);
                }
                AqualityServices.Browser.Quit();
            }
        }
    }
}
