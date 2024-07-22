using Aquality.Selenium.Browsers;
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
            Assert.That(browser.CurrentUrl, Is.EqualTo(url));
        }

        [Test]
        public void Should_BePossibleTo_GetWebDriverInstance()
        {
            var url = new WelcomeForm().Url;
            var browser = AqualityServices.Browser;
            browser.Driver.Navigate().GoToUrl(url);
            Assert.That(browser.Driver.Url, Is.EqualTo(url));
        }

        [Test]
        public void Should_BePossibleTo_NavigateBackAndForward()
        {
            var firstNavigationUrl = new WelcomeForm().Url;
            var secondNavigationUrl = new CheckBoxesForm().Url;

            var browser = AqualityServices.Browser;
            browser.GoTo(firstNavigationUrl);
            Assert.That(browser.CurrentUrl, Is.EqualTo(firstNavigationUrl));

            browser.GoTo(secondNavigationUrl);
            Assert.That(browser.CurrentUrl, Is.EqualTo(secondNavigationUrl));

            browser.GoBack();
            Assert.That(browser.CurrentUrl, Is.EqualTo(firstNavigationUrl));

            browser.GoForward();
            Assert.That(browser.CurrentUrl, Is.EqualTo(secondNavigationUrl));
        }

        [Test]
        public void Should_BePossibleTo_OpenNewBrowserAfterQuit()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            AqualityServices.Browser.Quit();

            Assert.That(AqualityServices.Browser.CurrentUrl, Is.Not.EqualTo(welcomeForm.Url));
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
            Assert.That(firstItemAfterRefresh, Is.Not.EqualTo(firstItem));
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
            Assert.That(AqualityServices.Browser.GetScreenshot().Length > 0);
        }

        [Test]
        public void Should_BePossibleTo_ExecuteJavaScript()
        {
            var dynamicContentForm = new DynamicContentForm();
            dynamicContentForm.Open();
            var currentUrl = AqualityServices.Browser.ExecuteScript<string>("return window.location.href");
            Assert.That(currentUrl, Is.EqualTo(dynamicContentForm.Url));
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
                Assert.That(durationSeconds, Is.LessThan(expectedDurationInSeconds + operationDurationInSeconds), "Elapsed time should be less than (js + operation) duration");
                Assert.That(durationSeconds, Is.GreaterThanOrEqualTo(expectedDurationInSeconds), "Elapsed time should be greater or equal than js duration");
            });
        }

        [Test]
        public void Should_BePossibleTo_ExecuteAsyncJavaScript_WithScriptTimeoutException()
        {
            new DynamicContentForm().Open();
            var expectedDurationInSeconds = AqualityServices.Get<ITimeoutConfiguration>().Script.TotalSeconds + 1;            
            Assert.Throws<WebDriverTimeoutException>(() => AqualityServices.Browser.ExecuteAsyncScript(GetAsyncTimeoutJavaScript(expectedDurationInSeconds)));
        }

        private static string GetAsyncTimeoutJavaScript(double expectedDurationInSeconds)
        {
            return $"window.setTimeout(arguments[arguments.length - 1], {expectedDurationInSeconds * 1000});";
        }

        [Test]
        public void Should_BePossibleTo_ExecuteJavaScriptFromFile()
        {
            var dynamicContentForm = new DynamicContentForm();
            dynamicContentForm.Open();
            var currentUrl = AqualityServices.Browser.ExecuteScriptFromFile<string>("Resources.GetCurrentUrl.js");
            Assert.That(currentUrl, Is.EqualTo(dynamicContentForm.Url));
        }

        [Test]
        public void Should_BePossibleTo_ExecuteJavaScriptFromPredefinedFile()
        {
            var valueToSet = "username";
            var authForm = new TheInternetAuthenticationForm();
            authForm.Open();
            AqualityServices.Browser.ExecuteScript(JavaScript.SetValue, authForm.UserNameTextBox.GetElement(), valueToSet);
            Assert.That(authForm.UserNameTextBox.Value, Is.EqualTo(valueToSet));
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
                Assert.That(currentSize.Height < initialSize.Height);
                Assert.That(currentSize.Width < initialSize.Width);
                Assert.That(currentSize.Width >= testSize.Width);
            });

            browser.Maximize();
            currentSize = browser.Driver.Manage().Window.Size;
            Assert.Multiple(() =>
            {
                Assert.That(currentSize, Is.Not.EqualTo(testSize));
                Assert.That(currentSize.Height > testSize.Height);
                Assert.That(currentSize.Width > testSize.Width);
            });

            browser.SetWindowSize(defaultSize.Width, defaultSize.Height);
            Assert.That(defaultSize, Is.EqualTo(browser.Driver.Manage().Window.Size));
        }

        [Test]
        public void Should_BePossibleTo_ScrollWindowBy()
        {
            var infiniteScrollForm = new InfiniteScrollForm();
            infiniteScrollForm.Open();
            infiniteScrollForm.WaitForPageToLoad();
            var defaultCount = infiniteScrollForm.ExampleLabels.Count;
            Assert.DoesNotThrow(
                () => AqualityServices.ConditionalWait.WaitForTrue(() =>
                {
                    var formHeight = infiniteScrollForm.Size.Height;
                    AqualityServices.Browser.ScrollWindowBy(0, formHeight);
                    return infiniteScrollForm.ExampleLabels.Count > defaultCount;
                }), "Some examples should be added after scroll");
        }

        [Test]
        public void Should_BePossibleTo_ScrollWindowBy_ViaJs()
        {
            var infiniteScrollForm = new InfiniteScrollForm();
            infiniteScrollForm.Open();
            infiniteScrollForm.WaitForPageToLoad();
            var defaultCount = infiniteScrollForm.ExampleLabels.Count;
            Assert.DoesNotThrow(
                () => AqualityServices.ConditionalWait.WaitForTrue(() =>
                {
                    var formHeight = infiniteScrollForm.Size.Height;
                    AqualityServices.Browser.ScrollWindowByViaJs(0, formHeight);
                    return infiniteScrollForm.ExampleLabels.Count > defaultCount;
                }), "Some examples should be added after scroll");
        }

        [Test]
        public void Should_BePossibleTo_GetBrowserName()
        {
            var profileNameFromEnvironment = Environment.GetEnvironmentVariable("profile");
            var settingsProfile = profileNameFromEnvironment == null ? "settings.json" : $"settings.{profileNameFromEnvironment}.json";
            var settingsFile = new JsonSettingsFile(settingsProfile);
            var browserName = (BrowserName)Enum.Parse(typeof(BrowserName), settingsFile.GetValue<string>(".browserName"), ignoreCase: true);
            Assert.That(AqualityServices.Browser.BrowserName, Is.EqualTo(browserName));
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
                Assert.That(elapsedTime, Is.LessThan(waitTime.Add(TimeSpan.FromSeconds(2))), 
                    $"Elapsed time should be less than implicit timeout + 2 sec(accuracy). Elapsed time: {elapsedTime.Seconds}");
                Assert.That(elapsedTime, Is.GreaterThanOrEqualTo(waitTime), "Elapsed time should be greater or equal than implicit timeout");
            });
        }

        [Test]
        public void Should_BePossibleTo_GetDownloadDir()
        {
            var downloadDir = AqualityServices.Browser.DownloadDirectory;
            Assert.That(downloadDir.ToLower().Contains("downloads", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
