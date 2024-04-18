using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Elements;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.Actions
{
    internal class JsActionsTests : UITest
    {
        [Test]
        public void Should_BePossibleTo_Click()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            welcomeForm.GetExampleLink(AvailableExample.Dropdown).JsActions.Click();
            Assert.That(new DropdownForm().State.WaitForDisplayed(), "Dropdown form should be displayed");
        }

        [Test]
        public void Should_BePossibleTo_ClickAndWait()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            welcomeForm.GetExampleLink(AvailableExample.Dropdown).JsActions.ClickAndWait();
            Assert.That(new DropdownForm().State.WaitForDisplayed(), "Dropdown form should be displayed");
        }

        [Test]
        public void Should_BePossibleTo_HighlightElement()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            var dropdownExample = welcomeForm.GetExampleLink(AvailableExample.Dropdown);
            dropdownExample.JsActions.HighlightElement(HighlightState.Highlight);
            var border = dropdownExample.GetCssValue("border");
            Assert.That(border, Is.EqualTo("3px solid rgb(255, 0, 0)"), "Element should be highlighted");
        }

        [Test]
        public void Should_BePossibleTo_HoverMouse()
        {
            var menuForm = new JQueryMenuForm();
            menuForm.Open();
            JQueryMenuForm.EnabledButton.JsActions.HoverMouse();            
            Assert.That(JQueryMenuForm.IsEnabledButtonFocused, "Element should be focused after hover");
        }

        [Test]
        public void Should_BePossibleTo_SetFocus()
        {
            var form = new ForgotPasswordForm();
            form.Open();
            ForgotPasswordForm.EmailTextBox.ClearAndType("peter.parker@example.com");
            ForgotPasswordForm.RetrievePasswordButton.JsActions.SetFocus();
            
            var currentText = ForgotPasswordForm.EmailTextBox.Value;
            var expectedText = currentText.Remove(currentText.Length - 1);
            ForgotPasswordForm.EmailTextBox.JsActions.SetFocus();
            ForgotPasswordForm.EmailTextBox.SendKeys(Keys.Delete);
            ForgotPasswordForm.EmailTextBox.SendKeys(Keys.Backspace);
            Assert.That(ForgotPasswordForm.EmailTextBox.Value, Is.EqualTo(expectedText), $"One character should be removed from '{currentText}'");
        }

        [Test]
        public void Should_BePossibleTo_CheckIsElementOnScreen()
        {
            var hoversForm = new HoversForm();
            hoversForm.Open();
            Assert.Multiple(() =>
            {
                Assert.That(HoversForm.GetHiddenElement(HoverExample.First, ElementState.ExistsInAnyState).JsActions.IsElementOnScreen(), Is.False,
                    $"Hidden element for {HoverExample.First} should be invisible.");
                Assert.That(HoversForm.GetExample(HoverExample.First).JsActions.IsElementOnScreen(),
                    $"Element for {HoverExample.First} should be visible.");
            });
        }

        [Test]
        public void Should_BePossibleTo_SetValue()
        {
            var keyPressesForm = new KeyPressesForm();
            keyPressesForm.Open();
            const string text = "text";
            keyPressesForm.InputTextBox.JsActions.SetValue(text);
            var actualText = keyPressesForm.InputTextBox.Value;
            Assert.That(actualText, Is.EqualTo(text), $"Text should be '{text}' after setting value via JS");
        }

        [Test]
        public void Should_BePossibleTo_GetElementText()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            Assert.That(welcomeForm.SubTitleLabel.JsActions.GetElementText(), Is.EqualTo(WelcomeForm.SubTitle),
                $"Sub title should be {WelcomeForm.SubTitle}");
        }

        [Test]
        public void Should_BePossibleTo_GetXPathLocator()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            var actualLocator = welcomeForm.SubTitleLabel.JsActions.GetXPath();
            const string expectedLocator = "/html/body/DIV[2]/DIV[1]/H2[1]";
            Assert.That(actualLocator, Is.EqualTo(expectedLocator), $"Locator of sub title should be {expectedLocator}");
        }

        [Test]
        public void Should_BePossibleTo_GetViewPortCoordinates()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            var actualPoint = welcomeForm.SubTitleLabel.JsActions.GetViewPortCoordinates();
            Assert.That(actualPoint.IsEmpty, Is.False, "Coordinates of Sub title should not be empty");
        }

        [Test]
        public void Should_BePossibleTo_ScrollIntoView()
        {
            var infiniteScrollForm = new InfiniteScrollForm();
            infiniteScrollForm.Open();
            infiniteScrollForm.WaitForPageToLoad();
            var defaultCount = infiniteScrollForm.ExampleLabels.Count;
            Assert.DoesNotThrow(
                () => AqualityServices.ConditionalWait.WaitForTrue(() =>
                {
                    infiniteScrollForm.LastExampleLabel.JsActions.ScrollIntoView();
                    return infiniteScrollForm.ExampleLabels.Count > defaultCount;
                }), "Some examples should be added after scroll");
        }

        [Test]
        public void Should_BePossibleTo_ScrollBy()
        {
            var point = new Point(50, 40);
            var homeDemoSiteForm = new HomeDemoSiteForm();
            homeDemoSiteForm.Open();
            homeDemoSiteForm.FirstScrollableExample.JsActions.ScrollBy(point.X, point.Y);
            var currentCoordinates = AqualityServices.Browser
                .ExecuteScriptFromFile<IList<object>>("Resources.GetScrollCoordinates.js",
                    homeDemoSiteForm.FirstScrollableExample.GetElement()).
                    Select(item => (int)Math.Round(double.Parse(item.ToString())))
                .ToList();
            var actualPoint = new Point(currentCoordinates[0], currentCoordinates[1]);
            Assert.That(actualPoint, Is.EqualTo(point), $"Current coordinates should be '{point}'");
        }

        [Test]
        public void Should_BePossibleTo_ScrollToTheCenter()
        {
            const int accuracy = 1;
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            welcomeForm.GetExampleLink(AvailableExample.Hovers).JsActions.ScrollToTheCenter();

            var windowSize = AqualityServices.Browser.ExecuteScriptFromFile<object>("Resources.GetWindowSize.js").ToString();
            var currentY = AqualityServices.Browser.ExecuteScriptFromFile<object>("Resources.GetElementYCoordinate.js",
                welcomeForm.GetExampleLink(AvailableExample.Hovers).GetElement()).ToString();
            var coordinateRelatingWindowCenter = double.Parse(windowSize) / 2 - double.Parse(currentY);
            Assert.That(Math.Abs(coordinateRelatingWindowCenter), Is.LessThanOrEqualTo(accuracy), "Upper bound of element should be in the center of the page");
        }

        [Test]
        public void Should_BePossibleTo_ScrollToTheCenter_CheckUI()
        {
            var infiniteScrollForm = new InfiniteScrollForm();
            infiniteScrollForm.Open();
            infiniteScrollForm.WaitForPageToLoad();
            var defaultCount = infiniteScrollForm.ExampleLabels.Count;
            Assert.DoesNotThrow(
                () => AqualityServices.ConditionalWait.WaitForTrue(() =>
                {
                    infiniteScrollForm.Footer.JsActions.ScrollToTheCenter();
                    return infiniteScrollForm.ExampleLabels.Count > defaultCount;
                }), "Some examples should be added after scroll");
        }
    }
}
