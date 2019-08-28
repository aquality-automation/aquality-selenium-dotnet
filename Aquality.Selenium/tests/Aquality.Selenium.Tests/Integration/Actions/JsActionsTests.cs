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
        [Ignore("test ignore")]
        [Test]
        public void Should_BePossibleTo_Click()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            welcomeForm.GetExampleLink(AvailableExample.Dropdown).JsActions.Click();
            Assert.IsTrue(new DropdownForm().IsDisplayed, "Dropdown form should be displayed");
        }

        [Ignore("test ignore")]
        [Test]
        public void Should_BePossibleTo_ClickAndWait()
        {            
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            welcomeForm.GetExampleLink(AvailableExample.Dropdown).JsActions.ClickAndWait();
            Assert.IsTrue(new DropdownForm().IsDisplayed, "Dropdown form should be displayed");
        }

        [Ignore("test ignore")]
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

        [Ignore("test ignore")]
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

        [Ignore("test ignore")]
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

        [Ignore("test ignore")]
        [Test]
        public void Should_BePossibleTo_SetValue()
        {
            var keyPressesForm = new KeyPressesForm();
            keyPressesForm.Open();
            var text = "text";
            keyPressesForm.InputTextBox.JsActions.SetValue(text);
            var actualText = keyPressesForm.InputTextBox.Value;
            Assert.AreEqual(text, actualText, $"Text should be '{text}' after setting value via JS");
        }

        [Ignore("test ignore")]
        [Test]
        public void Should_BePossibleTo_GetElementText()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            Assert.AreEqual(WelcomeForm.SubTitle, welcomeForm.SubTitleLabel.JsActions.GetElementText(),
                $"Sub title should be {WelcomeForm.SubTitle}");
        }

        [Ignore("test ignore")]
        [Test]
        public void Should_BePossibleTo_GetXPathLocator()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            var actualLocator = welcomeForm.SubTitleLabel.JsActions.GetXPath();
            var expectedLocator = "/html/body/DIV[2]/DIV[1]/H2[1]";
            Assert.AreEqual(expectedLocator, actualLocator, $"Locator of sub title should be {expectedLocator}");
        }

        [Ignore("test ignore")]
        [Test]
        public void Should_BePossibleTo_GetViewPortCoordinates()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            var actualPoint = welcomeForm.SubTitleLabel.JsActions.GetViewPortCoordinates();
            Assert.IsFalse(actualPoint.IsEmpty, "Coordinates of Sub title should not be empty");
        }

        [Ignore("test ignore")]
        [Test]
        public void Should_BePossibleTo_ScrollIntoView()
        {
            var infiniteScrollForm = new InfiniteScrollForm();
            infiniteScrollForm.Open();
            var defaultCount = infiniteScrollForm.ExampleLabels.Count;
            infiniteScrollForm.LastExampleLabel.JsActions.ScrollIntoView();
            Assert.DoesNotThrow(
                () => ConditionalWait.WaitForTrue(() => infiniteScrollForm.ExampleLabels.Count > defaultCount),
                "Some examples should be added after scroll");
        }

        [Ignore("test ignore")]
        [Test]
        public void Should_BePossibleTo_ScrollBy()
        {
            var infiniteScrollForm = new InfiniteScrollForm();
            infiniteScrollForm.Open();
            var defaultCount = infiniteScrollForm.ExampleLabels.Count;
            infiniteScrollForm.LastExampleLabel.JsActions.ScrollBy(100000, 100000);
            Assert.DoesNotThrow(
                () => ConditionalWait.WaitForTrue(() => infiniteScrollForm.ExampleLabels.Count > defaultCount),
                "Some examples should be added after scroll");
        }

        [Ignore("test ignore")]
        [Test]
        public void Should_BePossibleTo_ScrollToTheCenter()
        {
            var infiniteScrollForm = new InfiniteScrollForm();
            infiniteScrollForm.Open();
            var defaultCount = infiniteScrollForm.ExampleLabels.Count;
            infiniteScrollForm.LastExampleLabel.JsActions.ScrollToTheCenter();
            Assert.DoesNotThrow(
                () => ConditionalWait.WaitForTrue(() => infiniteScrollForm.ExampleLabels.Count > defaultCount),
                "Some examples should be added after scroll");
        }
    }
}
