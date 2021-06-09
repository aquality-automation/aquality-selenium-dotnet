using static Aquality.Selenium.Browsers.AqualityServices;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Tests.Integration.TestApp;
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
            if (IsBrowserStarted)
            {
                Browser.Quit();
            }
        }

        protected void OpenAutomationPracticeSite()
        {
            var resourceLimitLabel = Get<IElementFactory>()
                .GetLabel(By.XPath("//h1[.='Resource Limit Is Reached']"), "Resource Limit Is Reached");
            Browser.GoTo(Constants.UrlAutomationPractice);
            Browser.WaitForPageToLoad();
            ConditionalWait.WaitForTrue(() =>
            {
                if (resourceLimitLabel.State.IsDisplayed)
                {
                    Browser.Refresh();
                    Browser.WaitForPageToLoad();
                    return false;
                }
                return true;
            }, timeout: TimeSpan.FromMinutes(3), pollingInterval: TimeSpan.FromSeconds(15), 
            message: $"Failed to load [{Constants.UrlAutomationPractice}] website. {resourceLimitLabel.Name} message is displayed.");
        }
    }
}
