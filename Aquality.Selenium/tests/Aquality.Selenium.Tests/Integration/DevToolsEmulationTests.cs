using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Integration.TestApp.ManyTools.Forms;
using Aquality.Selenium.Tests.Integration.TestApp.MyLocation;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V85.Emulation;
using System;
using System.Collections.Generic;

namespace Aquality.Selenium.Tests.Integration
{
    internal class DevToolsEmulationTests : UITest
    {
        private const double LatitudeForOverride = 35.8235;
        private const double LongitudeForOverride = -78.8256;
        private const double Accuracy = 0.97;

        private const long DeviceModeSettingWidth = 600;
        private const long DeviceModeSettingHeight = 1000;
        private const bool DeviceModeSettingMobile = true;
        private const double DeviceModeSettingDeviceScaleFactor = 50;

        private const string CustomUserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 6_1_4 like Mac OS X) AppleWebKit/536.26 (KHTML, like Gecko) Version/6.0 Mobile/10B350 Safari/8536.25";
        private const string CustomAcceptLanguage = "be-BY";

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
                async () => await DevTools.SetDeviceMetricsOverride(width, height, isMobile), "Should be possible to set device metrics override"));
        }

        [Test]
        public void Should_BePossibleTo_SetAndClearDeviceMetricsOverride_WithVersionSpecificParameters()
        {
            void setAction(long width, long height, bool isMobile, double scaleFactor)
            {
                var parameters = new OpenQA.Selenium.DevTools.V120.Emulation.SetDeviceMetricsOverrideCommandSettings
                {
                    DisplayFeature = new OpenQA.Selenium.DevTools.V120.Emulation.DisplayFeature
                    {
                        Orientation = OpenQA.Selenium.DevTools.V120.Emulation.DisplayFeatureOrientationValues.Horizontal
                    },
                    Width = width,
                    Height = height,
                    Mobile = isMobile,
                    DeviceScaleFactor = scaleFactor
                };
                Assert.DoesNotThrowAsync(async () => await DevTools.SetDeviceMetricsOverride(parameters), 
                    "Should be possible to set device metrics override with version-specific parameters, even if the version doesn't match");
            }

            CheckDeviceMetricsOverride(setAction);
        }
        
        private static void CheckDeviceMetricsOverride(Action<long, long, bool, double> setAction)
        {
            static long getWindowHeight() => AqualityServices.Browser.ExecuteScriptFromFile<long>("Resources.GetWindowSize.js");
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            var initialValue = getWindowHeight();
            Assume.That(initialValue, Is.Not.EqualTo(DeviceModeSettingHeight), "To check that override works, initial value should differ from the new one");
            setAction(DeviceModeSettingWidth, DeviceModeSettingHeight, DeviceModeSettingMobile, DeviceModeSettingDeviceScaleFactor);
            Assert.AreEqual(DeviceModeSettingHeight, getWindowHeight(), "Browser height should match to override value");
            
            Assert.DoesNotThrowAsync(async () => await DevTools.ClearDeviceMetricsOverride(), "Should be possible to clear device metrics override");
            AqualityServices.Browser.Refresh();
            AqualityServices.Browser.WaitForPageToLoad();
            Assert.AreEqual(initialValue, getWindowHeight(), "Browser height should match to initial value after clear");
        }

        [Test]
        public void Should_BePossibleTo_SetAndClearGeoLocationOverride()
        {
            CheckGeolocationOverride(
                (latitude, longitude, accuracy) => 
                Assert.DoesNotThrowAsync(async () => await DevTools.SetGeoLocationOverride(latitude, longitude, accuracy), "Should be possible to override geoLocation"),
                () => Assert.DoesNotThrowAsync(async () => await DevTools.ClearGeolocationOverride(), "Should be possible to clear geoLocation"));
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
            AqualityServices.Browser.WaitForPageToLoad();
            AqualityServices.Browser.Refresh();
            locationForm.DetectBrowserGeolocation();
            Assert.AreEqual(defaultLatitude, locationForm.Latitude, "Latitude should match to default");
            Assert.AreEqual(defaultLongitude, locationForm.Longitude, "Longitude should match to default");
        }

        [Test]
        public void Should_BePossibleTo_SetUserAgentAndLanguageOverride()
        {
            var defaultLanguage = new BrowserLanguageForm().Open().Value;
            var defaultUserAgent = new UserAgentForm().Open().Value;

            Assume.That(defaultLanguage, Is.Not.EqualTo(CustomAcceptLanguage), "Default accept-language header should be different from the custom one to check override");
            Assume.That(defaultUserAgent, Is.Not.EqualTo(CustomUserAgent), "Default user agent header should be different from the custom one to check override");

            Assert.DoesNotThrowAsync(async () => await DevTools.SetUserAgentOverride(CustomUserAgent, CustomAcceptLanguage), "Should be possible to set user agent override");
            StringAssert.Contains(CustomAcceptLanguage, new BrowserLanguageForm().Open().Value, "Accept-language header should match to value set");
            Assert.AreEqual(CustomUserAgent, new UserAgentForm().Open().Value, "User agent should match to value set");
        }

        [Test]
        public void Should_BePossibleTo_SetScriptExecutionDisabled_AndEnableAgain()
        {
            var alertsForm = new JavaScriptAlertsForm();
            alertsForm.Open();
            alertsForm.JsAlertButton.Click();
            Assert.DoesNotThrow(() => AqualityServices.Browser.HandleAlert(AlertAction.Accept), "Alert should appear and be handled");

            Assert.DoesNotThrowAsync(async () => await DevTools.SetScriptExecutionDisabled(), "Should be possible to set script execution disabled");
            alertsForm.JsAlertButton.Click();
            Assert.Throws<NoAlertPresentException>(() => AqualityServices.Browser.HandleAlert(AlertAction.Accept), "Alert should not appear as JS scripts disabled");

            Assert.DoesNotThrowAsync(async () => await DevTools.SetScriptExecutionDisabled(false), "Should be possible to set script execution enabled");
            alertsForm.JsAlertButton.Click();
            Assert.DoesNotThrow(() => AqualityServices.Browser.HandleAlert(AlertAction.Accept), "Alert should appear and be handled as JS scripts are enabled again");
        }

        [Test]
        public void Should_BePossibleTo_SetTouchEmulationEnabled_AndDisabled()
        {
            static bool isTouchEnabled() => AqualityServices.Browser.ExecuteScriptFromFile<bool>("Resources.IsTouchEnabled.js");
            Assume.That(isTouchEnabled, Is.False, "Touch should be initially disabled");

            Assert.DoesNotThrowAsync(async () => await DevTools.SetTouchEmulationEnabled(true), "Should be possible to enable touch emulation");
            Assert.IsTrue(isTouchEnabled(), "Touch should be enabled");
            Assert.DoesNotThrowAsync(async () => await DevTools.SetTouchEmulationEnabled(new SetTouchEmulationEnabledCommandSettings { Enabled = false }), 
                "Should be possible to disable touch emulation");
            Assert.IsFalse(isTouchEnabled(), "Touch should be disabled");
        }

        [Test]
        public void Should_BePossibleTo_SetEmulatedMedia()
        {
            const string emulatedMedia = "projection";

            static string getMediaType() => AqualityServices.Browser.ExecuteScriptFromFile<string>("Resources.GetMediaType.js");
            var initialValue = getMediaType();
            Assume.That(initialValue, Does.Not.Contain(emulatedMedia), "Initial media type should differ from value to be set");

            Assert.DoesNotThrowAsync(async () => await DevTools.SetEmulatedMedia(emulatedMedia, new Dictionary<string, string> { { "width", DeviceModeSettingWidth.ToString() } }), 
                "Should be possible to set emulated media");
            Assert.AreEqual(emulatedMedia, getMediaType(), "Media type should equal to emulated");
            Assert.DoesNotThrowAsync(async () => await DevTools.DisableEmulatedMediaOverride(), "Should be possible to disable emulated media override");
            Assert.AreEqual(initialValue, getMediaType(), "Media type should equal to initial after disabling the override");
        }

        [Test]
        public void Should_BePossibleTo_SetDefaultBackgroundColorOverride()
        {
            Assert.DoesNotThrowAsync(async () => await DevTools.SetDefaultBackgroundColorOverride(0, 255, 38, 0.25), 
                "Should be possible to set default background color override");
            Assert.DoesNotThrowAsync(async () => await DevTools.ClearDefaultBackgroundColorOverride(), 
                "Should be possible to clear default background color override");
        }
    }
}
