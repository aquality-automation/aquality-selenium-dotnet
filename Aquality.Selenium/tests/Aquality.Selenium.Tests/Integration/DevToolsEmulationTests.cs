using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Integration.TestApp.MyLocation;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using OpenQA.Selenium.DevTools.V85.Emulation;
using System;
using System.Collections.Generic;

namespace Aquality.Selenium.Tests.Integration
{
    internal class DevToolsEmulationTests : UITest
    {
        private static readonly double LatitudeForOverride = 35.8235;
        private static readonly double LongitudeForOverride = -78.8256;
        private static readonly double Accuracy = 0.97;

        private static readonly long deviceModeSettingWidth = 600;
        private static readonly long deviceModeSettingHeight = 1000;
        private static readonly bool deviceModeSettingMobile = true;
        private static readonly double deviceModeSettingDeviceScaleFactor = 50;

        private static DevToolsHandling DevTools => AqualityServices.Browser.DevTools;

        [Test]
        public void Should_BePossibleTo_GetAndCloseDevToolsSession()
        {
            Assert.IsFalse(DevTools.HasActiveDevToolsSession, "No DevTools session should be running initially");
            Assert.IsNotNull(DevTools.GetDevToolsSession(), "Should be possible to get DevTools session");
            Assert.IsTrue(DevTools.HasActiveDevToolsSession, "DevTools session should be indicated as active after getting");
            Assert.DoesNotThrow(() => DevTools.CloseDevToolsSession(), "Should be possible to close DevTools session");
            Assert.IsFalse(DevTools.HasActiveDevToolsSession, "DevTools session should be indicated as not active after close");
            Assert.IsNotNull(DevTools.GetDevToolsSession(), "Should be possible to get a new DevTools session after close");
            Assert.IsTrue(DevTools.HasActiveDevToolsSession, "DevTools session should be indicated as active after getting for a second time");
        }

        [Test]
        public void Should_BePossibleTo_CheckThatBrowserCanEmulate()
        {
            var canEmulate = false;
            Assert.DoesNotThrowAsync(async () => canEmulate = await DevTools.CanEmulate(), "Should be possible to check that browser can emulate");
            Assert.IsTrue(canEmulate, "Emulation should be supported in browser");
        }

        [Test]
        public void Should_BePossibleTo_SetAndClearDeviceMetricsOverride()
        {
            CheckDeviceMetricsOverride((width, height, isMobile, scaleFactor) => Assert.DoesNotThrowAsync(
                () => DevTools.SetDeviceMetricsOverride(width, height, isMobile), "Should be possible to set device metrics override"));
        }

        [Test]
        public void Should_BePossibleTo_SetAndClearDeviceMetricsOverride_WithVersionSpecificParameters()
        {
            void setAction(long width, long height, bool isMobile, double scaleFactor)
            {
                var parameters = new OpenQA.Selenium.DevTools.V95.Emulation.SetDeviceMetricsOverrideCommandSettings
                {
                    DisplayFeature = new OpenQA.Selenium.DevTools.V95.Emulation.DisplayFeature
                    {
                        Orientation = OpenQA.Selenium.DevTools.V95.Emulation.DisplayFeatureOrientationValues.Horizontal
                    },
                    Width = width,
                    Height = height,
                    Mobile = isMobile,
                    DeviceScaleFactor = scaleFactor
                };
                Assert.DoesNotThrowAsync(() => DevTools.SetDeviceMetricsOverride(parameters), "Should be possible to set device metrics override with version-specific parameters");
            }

            CheckDeviceMetricsOverride(setAction);
        }

        private static void CheckDeviceMetricsOverride(Action<long, long, bool, double> setAction)
        {
            long getWindowHeight() => AqualityServices.Browser.ExecuteScriptFromFile<long>("Resources.GetWindowSize.js");
            var initialValue = getWindowHeight();
            Assume.That(initialValue, Is.Not.EqualTo(deviceModeSettingHeight), "To check that override works, initial value should differ from the new one");
            setAction(deviceModeSettingWidth, deviceModeSettingHeight, deviceModeSettingMobile, deviceModeSettingDeviceScaleFactor);
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            Assert.AreEqual(deviceModeSettingHeight, getWindowHeight(), "Browser height should match to override value");
            
            Assert.DoesNotThrowAsync(() => DevTools.ClearDeviceMetricsOverride(), "Should be possible to clear device metrics override");
            AqualityServices.Browser.Refresh();
            Assert.AreEqual(initialValue, getWindowHeight(), "Browser height should match to initial value after clear");
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
