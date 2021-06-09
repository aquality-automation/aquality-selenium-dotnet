using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Tests.Integration.TestApp;
using Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

[assembly: LevelOfParallelism(10)]
namespace Aquality.Selenium.Tests.Integration
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal class UITest
    {
        [TearDown]
        public void CleanUp()
        {
            if (AqualityServices.IsBrowserStarted)
            {
                AqualityServices.Browser.Quit();
            }
        }

        protected void OpenAutomationPracticeSite()
        {
            SiteLoader.OpenAutomationPracticeSite();
        }
    }
}
