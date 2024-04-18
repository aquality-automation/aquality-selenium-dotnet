using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using System.Globalization;

namespace Aquality.Selenium.Tests.Integration.TestApp.MyLocation
{
    internal class LocationForm : Form
    {
        private const string LocatorTemplate = "//div[@aria-expanded='true']//td[contains(., '{0}')]/following-sibling::td[not(contains(.,'waiting') or contains(.,'failed'))]";
        private readonly ILabel latitudeLabel = ElementFactory.GetLabel(By.XPath(string.Format(LocatorTemplate, "Latitude")), "Latitude");
        private readonly ILabel longitudeLabel = ElementFactory.GetLabel(By.XPath(string.Format(LocatorTemplate, "Longitude")), "Longitude");
        private readonly IButton browserGeoLocationButton = ElementFactory.GetButton(By.XPath("//*[@aria-controls='geo-div']"), "Browser GeoLocation");
        private readonly IButton startTestButton = ElementFactory.GetButton(By.Id("geo-test"), "Browser GeoLocation");
        private readonly IButton consentCookieInfoButton = ElementFactory.GetButton(By.XPath("//button[contains(@aria-label,'Consent')]"), "Consent cookie info");
        private readonly IButton gotItCookieButton = ElementFactory.GetButton(By.XPath("//a[contains(@aria-label,'dismiss')] | //img[contains(@src,'close')]"), "Got it! for Google cookie");
        private readonly IButton closeCookieSettingsButton = ElementFactory.GetButton(By.XPath("//*[contains(@class,'google-revocation-link')]//*[contains(@src,'close') or contains(@aria-label, 'lose')]"), "Close cookie settings");

        public LocationForm() : base(By.Id("accordion"), "My Location")
        {
        }

        public void DismissCookieInfo()
        {
            consentCookieInfoButton.Click();
            consentCookieInfoButton.State.WaitForNotDisplayed();
            if (gotItCookieButton.State.WaitForDisplayed())
            {
                gotItCookieButton.Click();
                gotItCookieButton.State.WaitForNotDisplayed();
            }
            if (closeCookieSettingsButton.State.IsDisplayed)
            {
                closeCookieSettingsButton.Click();
            }
        }

        public void DetectBrowserGeolocation()
        {
            browserGeoLocationButton.JsActions.Click();
            startTestButton.JsActions.Click();
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
