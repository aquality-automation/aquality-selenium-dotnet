using System.Drawing;
using Aquality.Selenium.Elements;
using Aquality.Selenium.Tests.UITests.Forms.AutomationPractice;
using NUnit.Framework;

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
        public void Should_BeAbleSetFocus_WithJsActions()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.SelectExample(AvailableExample.Hovers);

            var hoversForm = new HoversForm();
            hoversForm.SetFocusViaJs(HoverExample.First);
            Assert.IsTrue(hoversForm.IsHiddenElementVisible(HoverExample.First), $"Hidden element for {HoverExample.First} should be visible after set focus via js.");
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
            Assert.AreEqual(WelcomeForm.SubTitle, welcomeForm.GetSubTitleViaJs(), $"Sub title should be {WelcomeForm.SubTitle}");
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
    }
}
