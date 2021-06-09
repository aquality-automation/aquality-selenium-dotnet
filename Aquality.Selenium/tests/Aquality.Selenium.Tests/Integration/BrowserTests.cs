using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Integration.TestApp;
using Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Forms;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using TheInternetAuthenticationForm = Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms.AuthenticationForm;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.Drawing;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Core.Utilities;

namespace Aquality.Selenium.Tests.Integration
{
    internal class BrowserTests : UITest
    {
        [Test]
        public void Should_BePossibleTo_StartBrowserAndNavigate()
        {
            var url = new WelcomeForm().Url;
            var browser = AqualityServices.Browser;
            browser.GoTo(url);
            Assert.AreEqual(browser.CurrentUrl, url);
        }

        [Test]
        public void Should_BePossibleTo_GetWebDriverInstance()
        {
            var url = new WelcomeForm().Url;
            var browser = AqualityServices.Browser;
            browser.Driver.Navigate().GoToUrl(url);
            Assert.AreEqual(browser.Driver.Url, url);
        }

        [Test]
        public void Should_BePossibleTo_NavigateBackAndForward()
        {
            var firstNavigationUrl = new WelcomeForm().Url;
            var secondNavigationUrl = new CheckBoxesForm().Url;

            var browser = AqualityServices.Browser;
            browser.GoTo(firstNavigationUrl);
            Assert.AreEqual(browser.CurrentUrl, firstNavigationUrl);

            browser.GoTo(secondNavigationUrl);
            Assert.AreEqual(browser.CurrentUrl, secondNavigationUrl);

            browser.GoBack();
            Assert.AreEqual(browser.CurrentUrl, firstNavigationUrl);

            browser.GoForward();
            Assert.AreEqual(browser.CurrentUrl, secondNavigationUrl);
        }

        [Test]
        public void Should_BePossibleTo_OpenNewBrowserAfterQuit()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            AqualityServices.Browser.Quit();

            Assert.AreNotEqual(welcomeForm.Url, AqualityServices.Browser.CurrentUrl);
        }

        [Test]
        public void Should_BePossibleTo_RefreshPage()
        {
            var dynamicContentForm = new DynamicContentForm();
            dynamicContentForm.Open();
            var firstItem = dynamicContentForm.GetContentItem(1).GetText();

            var browser = AqualityServices.Browser;
            browser.Refresh();
            browser.WaitForPageToLoad();

            var firstItemAfterRefresh = dynamicContentForm.GetContentItem(1).GetText();
            Assert.AreNotEqual(firstItem, firstItemAfterRefresh);
        }

        [Test]
        public void Should_BePossibleTo_SetPageLoadTimeout()
        {
            var browser = AqualityServices.Browser;
            browser.SetPageLoadTimeout(TimeSpan.FromSeconds(1));
            Assert.Throws<WebDriverTimeoutException>(() => browser.GoTo("https://github.com/aquality-automation"));
        }

        [Test]
        public void Should_BePossibleTo_TakeScreenshot()
        {
            new DynamicContentForm().Open();
            Assert.IsTrue(AqualityServices.Browser.GetScreenshot().Length > 0);
        }

        [Test]
        public void Should_BePossibleTo_ExecuteJavaScript()
        {
            var dynamicContentForm = new DynamicContentForm();
            dynamicContentForm.Open();
            var currentUrl = AqualityServices.Browser.ExecuteScript<string>("return window.location.href");
            Assert.AreEqual(dynamicContentForm.Url, currentUrl);
        }

        [Test]
        public void Should_BePossibleTo_ExecuteAsyncJavaScript()
        {
            const int expectedDurationInSeconds = 1;
            const int operationDurationInSeconds = 1;

            new DynamicContentForm().Open();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            AqualityServices.Browser.ExecuteAsyncScript(GetAsyncTimeoutJavaScript(expectedDurationInSeconds));
            stopwatch.Stop();
            var durationSeconds = stopwatch.Elapsed.TotalSeconds;

            Assert.Multiple(() =>
            {
                Assert.Less(durationSeconds, (expectedDurationInSeconds + operationDurationInSeconds), "Elapsed time should be less than (js + operation) duration");
                Assert.GreaterOrEqual(durationSeconds, expectedDurationInSeconds, "Elapsed time should be greater or equal than js duration");
            });
        }

        [Test]
        public void Should_BePossibleTo_ExecuteAsyncJavaScript_WithScriptTimeoutException()
        {
            new DynamicContentForm().Open();
            var expectedDurationInSeconds = AqualityServices.Get<ITimeoutConfiguration>().Script.TotalSeconds + 1;            
            Assert.Throws<WebDriverTimeoutException>(() => AqualityServices.Browser.ExecuteAsyncScript(GetAsyncTimeoutJavaScript(expectedDurationInSeconds)));
        }

        private string GetAsyncTimeoutJavaScript(double expectedDurationInSeconds)
        {
            return $"window.setTimeout(arguments[arguments.length - 1], {expectedDurationInSeconds * 1000});";
        }

        [Test]
        public void Should_BePossibleTo_ExecuteJavaScriptFromFile()
        {
            var dynamicContentForm = new DynamicContentForm();
            dynamicContentForm.Open();
            var currentUrl = AqualityServices.Browser.ExecuteScriptFromFile<string>("Resources.GetCurrentUrl.js");
            Assert.AreEqual(dynamicContentForm.Url, currentUrl);
        }

        [Test]
        public void Should_BePossibleTo_ExecuteJavaScriptFromPredefinedFile()
        {
            var valueToSet = "username";
            var authForm = new TheInternetAuthenticationForm();
            authForm.Open();
            AqualityServices.Browser.ExecuteScript(JavaScript.SetValue, authForm.UserNameTextBox.GetElement(), valueToSet);
            Assert.AreEqual(valueToSet, authForm.UserNameTextBox.Value);
        }

        [Test]
        public void Should_BePossibleTo_SetWindowSize()
        {
            var defaultSize = new Size(1024, 768);
            var browser = AqualityServices.Browser;
            var initialSize = browser.Driver.Manage().Window.Size;

            var testSize = new Size(600, 600);
            browser.SetWindowSize(testSize.Width, testSize.Height);

            var currentSize = browser.Driver.Manage().Window.Size;
            Assert.Multiple(() => 
            {
                Assert.IsTrue(currentSize.Height < initialSize.Height);
                Assert.IsTrue(currentSize.Width < initialSize.Width);
                Assert.IsTrue(currentSize.Width >= testSize.Width);
            });

            browser.Maximize();
            currentSize = browser.Driver.Manage().Window.Size;
            Assert.Multiple(() =>
            {
                Assert.AreNotEqual(currentSize, testSize);
                Assert.IsTrue(currentSize.Height > testSize.Height);
                Assert.IsTrue(currentSize.Width > testSize.Width);
            });

            browser.SetWindowSize(defaultSize.Width, defaultSize.Height);
            Assert.AreEqual(browser.Driver.Manage().Window.Size, defaultSize);
        }

        [Test]
        public void Should_BePossibleTo_ScrollWindowBy()
        {
            OpenAutomationPracticeSite();
            var sliderForm = new SliderForm();
            var initialY = sliderForm.FormPointInViewPort.Y;
            var formHeight = sliderForm.Size.Height;
            AqualityServices.Browser.ScrollWindowBy(0, formHeight);
            Assert.LessOrEqual(Math.Abs(formHeight - (initialY - sliderForm.FormPointInViewPort.Y)), 1, "Window should be scrolled.");
        }

        [Test]
        public void Should_BePossibleTo_GetBrowserName()
        {
            var profileNameFromEnvironment = Environment.GetEnvironmentVariable("profile");
            var settingsProfile = profileNameFromEnvironment == null ? "settings.json" : $"settings.{profileNameFromEnvironment}.json";
            var settingsFile = new JsonSettingsFile(settingsProfile);
            var browserName = (BrowserName)Enum.Parse(typeof(BrowserName), settingsFile.GetValue<string>(".browserName"), ignoreCase: true);
            Assert.AreEqual(browserName, AqualityServices.Browser.BrowserName);
        }

        [Test]
        public void Should_BePossibleTo_SetImplicitWait()
        {
            var browser = AqualityServices.Browser;
            new WelcomeForm().Open();
            var waitTime = TimeSpan.FromSeconds(5);
            browser.SetImplicitWaitTimeout(waitTime);

            var stopwatch = Stopwatch.StartNew();
            var elapsedTime = TimeSpan.Zero;
            try
            {
                browser.Driver.FindElement(By.Id("not_exist_element"));
            } 
            catch (NoSuchElementException)
            {
                elapsedTime = stopwatch.Elapsed;
            }
            Assert.Multiple(() =>
            {
                Assert.Less(elapsedTime, waitTime.Add(TimeSpan.FromSeconds(2)), 
                    $"Elapsed time should be less than implicit timeout + 2 sec(accuracy). Elapsed time: {elapsedTime.Seconds}");
                Assert.GreaterOrEqual(elapsedTime, waitTime, "Elapsed time should be greater or equal than implicit timeout");
            });
        }

        [Test]
        public void Should_BePossibleTo_GetDownloadDir()
        {
            var downloadDir = AqualityServices.Browser.DownloadDirectory;
            Assert.IsTrue(downloadDir.ToLower().Contains("downloads", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
