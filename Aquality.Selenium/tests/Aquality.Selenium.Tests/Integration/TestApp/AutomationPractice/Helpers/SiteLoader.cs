using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System;
using static Aquality.Selenium.Browsers.AqualityServices;

namespace Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Helpers
{
    internal static class SiteLoader
    {
        public static void OpenAutomationPracticeSite(string customUrl = null)
        {
            var resourceLimitLabel = Get<IElementFactory>()
                .GetLabel(By.XPath("//h1[.='Resource Limit Is Reached']"), "Resource Limit Is Reached");
            Browser.GoTo(customUrl ?? Constants.UrlAutomationPractice);
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
            message: $"Failed to load [{customUrl ?? Constants.UrlAutomationPractice}] website. {resourceLimitLabel.Name} message is displayed.");
        }
    }
}
