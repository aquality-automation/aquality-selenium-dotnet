using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements;
using Aquality.Selenium.Tests.Integration.TestApp;
using Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Forms;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using Aquality.Selenium.Waitings;
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
            Assert.IsTrue(new DropdownForm().IsDisplayed, "Dropdown form should be displayed");
        }

        [Test]
        public void Should_BePossibleTo_ClickAndWait()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            welcomeForm.GetExampleLink(AvailableExample.Dropdown).JsActions.ClickAndWait();
            Assert.IsTrue(new DropdownForm().IsDisplayed, "Dropdown form should be displayed");
        }

        [Test]
        public void Should_BePossibleTo_HighlightElement()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            var dropdownExample = welcomeForm.GetExampleLink(AvailableExample.Dropdown);
            dropdownExample.JsActions.HighlightElement(HighlightState.Highlight);
            var border = dropdownExample.GetCssValue("border");
            Assert.AreEqual("3px solid rgb(255, 0, 0)", border, "Element should be highlighted");
        }

        [Test]
        public void Should_BePossibleTo_HoverMouse()
        {
            BrowserManager.Browser.GoTo(Constants.UrlAutomationPractice);
            var productList = new ProductListForm();
            productList.NavigateToLastProduct();

            var product = new ProductForm();
            product.GetLastProductView().JsActions.HoverMouse();
            var classAttribute = product.GetLastProductView().GetAttribute("class");
            Assert.IsTrue(classAttribute.Contains("shown"), "Product view should be shown");
        }

        [Test]
        public void Should_BePossibleTo_SetFocus()
        {
            BrowserManager.Browser.GoTo(Constants.UrlAutomationPractice);
            var productList = new ProductListForm();
            productList.NavigateToLastProduct();

            var product = new ProductForm();
            var currentText = product.TxtQuantity.Value;
            var expectedText = currentText.Remove(currentText.Length - 1);
            product.TxtQuantity.JsActions.SetFocus();
            product.TxtQuantity.SendKeys(Keys.Delete);
            Assert.AreEqual(expectedText, product.TxtQuantity.Value, $"One character should be removed from '{currentText}'");
        }

        [Test]
        public void Should_BePossibleTo_CheckIsElementOnScreen()
        {
            var hoversForm = new HoversForm();
            hoversForm.Open();
            Assert.Multiple(() =>
            {
                Assert.IsFalse(hoversForm.GetHiddenElement(HoverExample.First, ElementState.ExistsInAnyState).JsActions.IsElementOnScreen(), 
                    $"Hidden element for {HoverExample.First} should be invisible.");
                Assert.IsTrue(hoversForm.GetExample(HoverExample.First).JsActions.IsElementOnScreen(),
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
            Assert.AreEqual(text, actualText, $"Text should be '{text}' after setting value via JS");
        }

        [Test]
        public void Should_BePossibleTo_GetElementText()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            Assert.AreEqual(WelcomeForm.SubTitle, welcomeForm.SubTitleLabel.JsActions.GetElementText(),
                $"Sub title should be {WelcomeForm.SubTitle}");
        }

        [Test]
        public void Should_BePossibleTo_GetXPathLocator()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            var actualLocator = welcomeForm.SubTitleLabel.JsActions.GetXPath();
            const string expectedLocator = "/html/body/DIV[2]/DIV[1]/H2[1]";
            Assert.AreEqual(expectedLocator, actualLocator, $"Locator of sub title should be {expectedLocator}");
        }

        [Test]
        public void Should_BePossibleTo_GetViewPortCoordinates()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            var actualPoint = welcomeForm.SubTitleLabel.JsActions.GetViewPortCoordinates();
            Assert.IsFalse(actualPoint.IsEmpty, "Coordinates of Sub title should not be empty");
        }

        [Test]
        public void Should_BePossibleTo_ScrollIntoView()
        {
            var infiniteScrollForm = new InfiniteScrollForm();
            infiniteScrollForm.Open();
            infiniteScrollForm.WaitForPageToLoad();
            var defaultCount = infiniteScrollForm.ExampleLabels.Count;
            Assert.DoesNotThrow(
                () => ConditionalWait.WaitForTrue(() =>
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
            var currentCoordinates = BrowserManager.Browser
                .ExecuteScriptFromFile<IList<object>>("Resources.GetScrollCoordinates.js",
                    homeDemoSiteForm.FirstScrollableExample.GetElement()).Select(item => int.Parse(item.ToString()))
                .ToList();
            var actualPoint = new Point(currentCoordinates[0], currentCoordinates[1]);
            Assert.AreEqual(point, actualPoint, $"Current coordinates should be '{point}'");
        }

        [Test]
        public void Should_BePossibleTo_ScrollToTheCenter()
        {
            const int accuracy = 5;
            var infiniteScrollForm = new InfiniteScrollForm();
            infiniteScrollForm.Open();
            infiniteScrollForm.WaitForPageToLoad();
            infiniteScrollForm.LastExampleLabel.JsActions.ScrollToTheCenter();

            var currentY = BrowserManager.Browser.ExecuteScriptFromFile<object>("Resources.GetElementYCoordinate.js",
                infiniteScrollForm.LastExampleLabel.GetElement()).ToString();
            var windowSize = BrowserManager.Browser.ExecuteScriptFromFile<object>("Resources.GetWindowSize.js").ToString();
            var coordinateRelatingWindowCenter = double.Parse(windowSize) / 2 - double.Parse(currentY);
            Assert.IsTrue(Math.Abs(coordinateRelatingWindowCenter) < accuracy, "Upper bound of element should be in the center of the page");
        }

        [Test]
        public void Should_BePossibleTo_ScrollToTheCenter_CheckUI()
        {
            var infiniteScrollForm = new InfiniteScrollForm();
            infiniteScrollForm.Open();
            infiniteScrollForm.WaitForPageToLoad();
            var defaultCount = infiniteScrollForm.ExampleLabels.Count;
            Assert.DoesNotThrow(
                () => ConditionalWait.WaitForTrue(() =>
                {
                    infiniteScrollForm.Footer.JsActions.ScrollToTheCenter();
                    return infiniteScrollForm.ExampleLabels.Count > defaultCount;
                }), "Some examples should be added after scroll");
        }
    }
}
