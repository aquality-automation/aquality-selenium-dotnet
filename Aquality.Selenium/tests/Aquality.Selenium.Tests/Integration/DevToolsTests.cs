using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Integration.TestApp.MyLocation;
using NUnit.Framework;
using OpenQA.Selenium.DevTools.V85.Emulation;
using System;
using System.Collections.Generic;

namespace Aquality.Selenium.Tests.Integration
{
    internal class DevToolsTests : UITest
    {
        private static readonly double LatitudeForOverride = 35.8235;
        private static readonly double LongitudeForOverride = -78.8256;
        private static readonly double Accuracy = 0.97;

        private static DevToolsHandling DevTools => AqualityServices.Browser.DevTools;

        [Test]
        public void Should_BePossibleTo_GetAndCloseDevToolsSession()
        {
            Assert.IsFalse(DevTools.HasActiveDevToolsSession, "No DevTools session should be running initially");
            Assert.IsNotNull(DevTools.GetDevToolsSession(), "Should be possible to get DevTools session");
            Assert.IsTrue(DevTools.HasActiveDevToolsSession, "DevTools session should be indicated as active after getting");
            Assert.DoesNotThrow(() => DevTools.CloseDevToolsSession(), "Should be possible to close DevTools session");
            Assert.IsFalse(DevTools.HasActiveDevToolsSession, "DevTools session should be indicated as not active after close");
        }

        [Test]
        public void Should_BePossibleTo_SetAndClearGeoLocationOverride()
        {
            CheckGeolocationOverride(
                (latitude, longitude, accuracy) => 
                Assert.DoesNotThrowAsync(() => DevTools.SetGeoLocationOverride(latitude, longitude, accuracy), "Should be possible to override geoLocation"),
                () => Assert.DoesNotThrowAsync(() => DevTools.ClearGeolocationOverride(), "Should be possible to clear geoLocation"));
        }

        [Test]
        public void Should_BePossibleTo_SetAndClearGeoLocationOverride_ByExecutingCdpCommand()
        {
            CheckGeolocationOverride(
                   (latitude, longitude, accuracy) =>
                   DevTools.ExecuteCdpCommand(
                       new SetGeolocationOverrideCommandSettings().CommandName,
                       new Dictionary<string, object>
                       {
                        { "latitude", latitude},
                        { "longitude", longitude},
                        { "accuracy", accuracy},
                       }),
                   () => DevTools.ExecuteCdpCommand(new ClearGeolocationOverrideCommandSettings().CommandName, new Dictionary<string, object>()));
        }

        private static void CheckGeolocationOverride(Action<double?, double?, double?> setAction, Action clearAction)
        {
            LocationForm.Open();
            var locationForm = new LocationForm();
            locationForm.DismissCookieInfo();
            locationForm.DetectBrowserGeolocation();
            var defaultLatitude = locationForm.Latitude;
            var defaultLongitude = locationForm.Longitude;
            Assume.That(defaultLatitude, Is.Not.EqualTo(LatitudeForOverride), "Default latitude should differ from the value for override");
            Assume.That(defaultLongitude, Is.Not.EqualTo(LongitudeForOverride), "Default longitude should differ from the value for override");

            setAction(LatitudeForOverride, LongitudeForOverride, Accuracy);
            AqualityServices.Browser.Refresh();
            locationForm.DetectBrowserGeolocation();
            Assert.AreEqual(LatitudeForOverride, locationForm.Latitude, "Latitude should match to override value");
            Assert.AreEqual(LongitudeForOverride, locationForm.Longitude, "Longitude should match to override value");

            clearAction();
            AqualityServices.Browser.Refresh();
            locationForm.DetectBrowserGeolocation();
            Assert.AreEqual(defaultLatitude, locationForm.Latitude, "Latitude should match to default");
            Assert.AreEqual(defaultLongitude, locationForm.Longitude, "Longitude should match to default");
        }
    }
}
