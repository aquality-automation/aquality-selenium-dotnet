using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System;

namespace Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Helpers
{
    internal static class SiteLoader
    {
        public static void OpenAutomationPracticeSite(string customUrl = null)
        {
            var resourceLimitLabel = AqualityServices.Get<IElementFactory>()
                .GetLabel(By.XPath("//h1[.='Resource Limit Is Reached']"), "Resource Limit Is Reached");
            AqualityServices.Browser.GoTo(customUrl ?? Constants.UrlAutomationPractice);
            AqualityServices.Browser.WaitForPageToLoad();
            AqualityServices.ConditionalWait.WaitForTrue(() =>
            {
                if (resourceLimitLabel.State.IsDisplayed)
                {
                    AqualityServices.Browser.Refresh();
                    AqualityServices.Browser.WaitForPageToLoad();
                    return false;
                }
                return true;
            }, timeout: TimeSpan.FromMinutes(3), pollingInterval: TimeSpan.FromSeconds(15),
            message: $"Failed to load [{customUrl ?? Constants.UrlAutomationPractice}] website. {resourceLimitLabel.Name} message is displayed.");
        }
    }
}
