using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Logging;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
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
                    try
                    {
                        var testId = TestContext.CurrentContext.Test.ID;
                        var screenshotPng = $"screenshot_{testId}.png";
                        AqualityServices.Browser.Driver.GetScreenshot().SaveAsFile(screenshotPng);
                        TestContext.AddTestAttachment(screenshotPng);
                        var sourceHtml = $"sourceHtml_{testId}.txt";
                        File.WriteAllText(sourceHtml, AqualityServices.Browser.Driver.PageSource);
                        TestContext.AddTestAttachment(sourceHtml);
                    }
                    catch (Exception e)
                    {
                        Logger.Instance.Fatal($"Failed to save test artifacts: {e.Message}", e);
                    }
                }
                AqualityServices.Browser.Quit();
            }
        }
    }
}
