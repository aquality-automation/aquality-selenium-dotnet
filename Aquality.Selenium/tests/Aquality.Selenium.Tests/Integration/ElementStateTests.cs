using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration
{
    internal class ElementStateTests : UITest
    {
        private readonly DynamicControlsForm dynamicControlsForm = new DynamicControlsForm();

        [SetUp]
        public void BeforeTest()
        {
            dynamicControlsForm.Open();
        }

        [Test]
        public void Should_BePossibleTo_CheckEnabledElementState()
        {
            Assert.Multiple(() =>
            {
                Assert.IsTrue(dynamicControlsForm.BtnChangeInputState.State.IsEnabled, "Change state button should be enabled");
                Assert.IsFalse(dynamicControlsForm.TxtTextInput.State.IsEnabled, "Text input should be disabled");
            });
        }

        [Test]
        public void Should_BePossibleTo_CheckDisplayedElementState()
        {
            Assert.Multiple(() =>
            {
                Assert.IsTrue(dynamicControlsForm.TxtTextInput.State.IsDisplayed, "Text input should be displayed");
                Assert.IsFalse(dynamicControlsForm.LblLoading.State.IsDisplayed, "Loading should not be displayed");
            });
        }

        [Test]
        public void Should_BePossibleTo_CheckExistElementState()
        {
            Assert.IsFalse(dynamicControlsForm.LblLoading.State.IsExist, "Loading element should not be exist by default");
            dynamicControlsForm.BtnChangeInputState.Click();
            Assert.IsTrue(dynamicControlsForm.LblLoading.State.IsExist, "Loading element should not exist after changing state");
        }

        [Test]
        public void Should_BePossibleTo_CheckElementIsClickableState()
        {
            Assert.Multiple(() =>
            {
                Assert.IsTrue(dynamicControlsForm.BtnChangeInputState.State.IsClickable, "Change state button should be clickable");
                Assert.IsFalse(dynamicControlsForm.TxtTextInput.State.IsClickable, "Text input should not be clickable");
            });
        }

        [Test]
        public void Should_BePossibleTo_WaitForElementEnabledState()
        {
            Assert.IsTrue(dynamicControlsForm.IsDisplayed, "Form 'Dynamic Controls' should be displayed");
            dynamicControlsForm.BtnChangeInputState.Click();
            Assert.IsTrue(dynamicControlsForm.TxtTextInput.State.WaitForEnabled(), "Text input should be enable after changing state");
            dynamicControlsForm.BtnChangeInputState.Click();
            Assert.IsTrue(dynamicControlsForm.TxtTextInput.State.WaitForNotEnabled(), "Text input should be disabled after changing state");
        }

        [Test]
        public void Should_BePossibleTo_WaitForElementDisplayedState()
        {
            dynamicControlsForm.BtnRemoveAddCheckbox.Click();
            Assert.IsTrue(dynamicControlsForm.CbxCheckboxExample.State.WaitForNotDisplayed(), "Checkbox example should not be displayed after removing");
            dynamicControlsForm.BtnRemoveAddCheckbox.Click();
            Assert.IsTrue(dynamicControlsForm.CbxCheckboxExample.State.WaitForDisplayed(), "Checkbox example should be displayed after adding");
        }

        [Test]
        public void Should_BePossibleTo_WaitForElementExistState()
        {
            dynamicControlsForm.BtnRemoveAddCheckbox.Click();
            Assert.IsTrue(dynamicControlsForm.CbxCheckboxExample.State.WaitForNotExist(), "Checkbox example should not be exist after removing");
            dynamicControlsForm.BtnRemoveAddCheckbox.Click();
            Assert.IsTrue(dynamicControlsForm.CbxCheckboxExample.State.WaitForExist(), "Checkbox example should be exist after adding");
        }

        [Test]
        public void Should_BePossibleTo_WaitForElementIsClickable()
        {
            Assert.Throws<WebDriverTimeoutException>(() => dynamicControlsForm.TxtTextInput.State.WaitForClickable(), "Textbox should not be clickable");
            dynamicControlsForm.BtnChangeInputState.Click();
            dynamicControlsForm.TxtTextInput.State.WaitForClickable();
            Assert.IsTrue(dynamicControlsForm.TxtTextInput.State.IsClickable, "Textbox should not be clickable after changing state");
        }
    }
}
