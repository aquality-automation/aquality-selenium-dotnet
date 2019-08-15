using System.Threading;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements;
using Aquality.Selenium.Tests.Constants;
using Aquality.Selenium.Tests.UITests.Forms.AutomationPractice;
using Aquality.Selenium.Waitings;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.UITests.Integration.Actions
{
    public class JsActionsTests : UITest
    {
        [Test]
        public void Should_BeAbleClick_WithJsActions()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.GetExampleLink(AvailableExample.Dropdown).JsActions.Click();
            Assert.IsTrue(new DropdownListForm().IsDisplayed, "Dropdown form should be displayed");
        }

        [Test]
        public void Should_BeAbleClickAndWait_WithJsActions()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.GetExampleLink(AvailableExample.Dropdown).JsActions.ClickAndWait();
            Assert.IsTrue(new DropdownListForm().IsDisplayed, "Dropdown form should be displayed");
        }

        [Test]
        public void Should_BeAbleHighlight_WithJsActions()
        {
            var welcomeForm = new WelcomeForm();
            var dropdownExample = welcomeForm.GetExampleLink(AvailableExample.Dropdown);
            dropdownExample.JsActions.HighlightElement(HighlightState.Highlight);
            var border = dropdownExample.GetCssValue("border");
            Assert.AreEqual("3px solid rgb(255, 0, 0)", border, "Element should be highlighted");
        }

        [Test]
        public void Should_BeAbleHover_WithJsActions()
        {
            BrowserManager.Browser.Navigate().GoToUrl(TestConstants.UrlAutomationPractice);
            var productList = new ProductListForm();
            productList.NavigateToLastProduct();

            var product = new ProductForm();
            product.GetProductView().JsActions.HoverMouse();
            var classAttribute = product.GetProductView().GetAttribute("class");
            Assert.IsTrue(classAttribute.Contains("shown"), "Product view should be shown");
        }

        [Test]
        public void Should_BeAbleSetFocus_WithJsActions()
        {
            BrowserManager.Browser.Navigate().GoToUrl(TestConstants.UrlAutomationPractice);
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
        public void Should_BeAbleCheckIsElementOnScreen_WithJsActions()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.SelectExample(AvailableExample.Hovers);

            var hoversForm = new HoversForm();
            Assert.Multiple(() =>
            {
                Assert.IsFalse(hoversForm.IsHiddenElementOnScreenViaJs(HoverExample.First, ElementState.ExistsInAnyState),
                    $"Hidden element for {HoverExample.First} should be invisible.");
                Assert.IsTrue(hoversForm.IsElementOnScreenViaJs(HoverExample.First),
                    $"Element for {HoverExample.First} should be visible.");
            });
        }

        [Test]
        public void Should_BeAbleSetValue_WithJsActions()
        {
            const string text = "text";
            var welcomeForm = new WelcomeForm();
            welcomeForm.SelectExample(AvailableExample.KeyPresses);

            var keyPressesForm = new KeyPressesForm();
            keyPressesForm.SetValueViaJs(text);
            var actualText = keyPressesForm.GetValue();
            Assert.AreEqual(text, actualText, $"Text should be '{text}' after setting value via JS");
        }

        [Test]
        public void Should_BeAbleGetText_WithJsActions()
        {
            var welcomeForm = new WelcomeForm();
            Assert.AreEqual(WelcomeForm.SubTitle, welcomeForm.GetSubTitleViaJs(),
                $"Sub title should be {WelcomeForm.SubTitle}");
        }

        [Test]
        public void Should_BeAbleGetLocator_WithJsActions()
        {
            const string expectedLocator = "/html/body/DIV[2]/DIV[1]/H2[1]";
            var welcomeForm = new WelcomeForm();
            var actualLocator = welcomeForm.GetSubTitleLocatorViaJs();
            Assert.AreEqual(expectedLocator, actualLocator, $"Locator of sub title should be {expectedLocator}");
        }

        [Test]
        public void Should_BeAbleGetCoordinates_WithJsActions()
        {
            var welcomeForm = new WelcomeForm();
            var actualPoint = welcomeForm.GetSubTitleCoordinatesViaJs();
            Assert.IsFalse(actualPoint.IsEmpty, "Coordinates of Sub title should not be empty");
        }

        [Test]
        public void Should_BeAbleScrollIntoView_WithJsActions()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.SelectExample(AvailableExample.InfiniteScroll);

            var infiniteScrollForm = new InfiniteScrollForm();
            var defaultCount = infiniteScrollForm.GetExamplesCount();
            infiniteScrollForm.ScrollIntoViewToLastExample();
            Assert.DoesNotThrow(
                () => ConditionalWait.WaitForTrue(() => infiniteScrollForm.GetExamplesCount() > defaultCount),
                "Some examples should be added after scroll");
        }

        [Test]
        public void Should_BeAbleScrollBy_WithJsActions()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.SelectExample(AvailableExample.InfiniteScroll);

            var infiniteScrollForm = new InfiniteScrollForm();
            var defaultCount = infiniteScrollForm.GetExamplesCount();
            infiniteScrollForm.ScrollByCoordinates(100000, 100000);
            Assert.DoesNotThrow(
                () => ConditionalWait.WaitForTrue(() => infiniteScrollForm.GetExamplesCount() > defaultCount),
                "Some examples should be added after scroll");
        }

        [Test]
        public void Should_BeAbleToTheCenter_WithJsActions()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.SelectExample(AvailableExample.InfiniteScroll);

            var infiniteScrollForm = new InfiniteScrollForm();
            var defaultCount = infiniteScrollForm.GetExamplesCount();
            infiniteScrollForm.ScrollToTheCenterOfLastExample();
            Assert.DoesNotThrow(
                () => ConditionalWait.WaitForTrue(() => infiniteScrollForm.GetExamplesCount() > defaultCount),
                "Some examples should be added after scroll");
        }
    }
}
