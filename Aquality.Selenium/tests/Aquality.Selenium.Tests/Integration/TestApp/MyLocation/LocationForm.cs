using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using System.Globalization;

namespace Aquality.Selenium.Tests.Integration.TestApp.MyLocation
{
    internal class LocationForm : Form
    {
        private readonly ILabel latitudeLabel = ElementFactory.GetLabel(By.Id("latitude"), "Latitude");
        private readonly ILabel longitudeLabel = ElementFactory.GetLabel(By.Id("longitude"), "Longitude");
        private readonly IButton consentCookieInfoButton = ElementFactory.GetButton(By.XPath("//button[@aria-label='Consent' or contains(@class,'fc-cta-consent')]"), "Consent cookie info");
      
        public LocationForm() : base(By.XPath("//*[contains(text(),'Location')]"), "Location")
        {
        }

        public void DismissCookieInfo()
        {
            if (consentCookieInfoButton.State.WaitForDisplayed())
            {
                consentCookieInfoButton.Click();
                consentCookieInfoButton.State.WaitForNotDisplayed();
            }
        }

        public void DetectBrowserGeolocation()
        {
            ConditionalWait.WaitForTrue(() => latitudeLabel.State.IsDisplayed && !string.IsNullOrWhiteSpace(latitudeLabel.GetText()),
                message: "Latitude text should be displayed & not empty");
        }

        public double Latitude => double.Parse(latitudeLabel.Text, CultureInfo.InvariantCulture);

        public double Longitude => double.Parse(longitudeLabel.Text, CultureInfo.InvariantCulture);

        public static void Open()
        {
            AqualityServices.Browser.GoTo(Constants.UrlMyLocation);
            AqualityServices.Browser.WaitForPageToLoad();
        }
    }
}
