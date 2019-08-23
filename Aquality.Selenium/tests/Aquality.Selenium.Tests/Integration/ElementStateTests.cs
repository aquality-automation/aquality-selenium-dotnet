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
                Assert.IsTrue(dynamicControlsForm.ChangeInputStateButton.State.IsEnabled, "Change state button should be enabled");
                Assert.IsFalse(dynamicControlsForm.TextInputTextBox.State.IsEnabled, "Text input should be disabled");
            });
        }
        
        [Test]
        public void Should_BePossibleTo_CheckDisplayedElementState()
        {
            Assert.Multiple(() =>
            {
                Assert.IsTrue(dynamicControlsForm.TextInputTextBox.State.IsDisplayed, "Text input should be displayed");
                Assert.IsFalse(dynamicControlsForm.LoadingLabel.State.IsDisplayed, "Loading should not be displayed");
            });
        }

        [Test]
        public void Should_BePossibleTo_CheckExistElementState()
        {
            Assert.IsFalse(dynamicControlsForm.LoadingLabel.State.IsExist, "Loading element should not be exist by default");
            dynamicControlsForm.ChangeInputStateButton.Click();
            Assert.IsTrue(dynamicControlsForm.LoadingLabel.State.IsExist, "Loading element should not exist after changing state");
        }

        [Test]
        public void Should_BePossibleTo_CheckElementIsClickableState()
        {
            Assert.Multiple(() =>
            {
                Assert.IsTrue(dynamicControlsForm.ChangeInputStateButton.State.IsClickable, "Change state button should be clickable");
                Assert.IsFalse(dynamicControlsForm.TextInputTextBox.State.IsClickable, "Text input should not be clickable");
            });
        }

        [Test]
        public void Should_BePossibleTo_WaitForElementEnabledState()
        {
            Assert.IsTrue(dynamicControlsForm.IsDisplayed, "Form 'Dynamic Controls' should be displayed");
            dynamicControlsForm.ChangeInputStateButton.Click();
            Assert.IsTrue(dynamicControlsForm.TextInputTextBox.State.WaitForEnabled(), "Text input should be enable after changing state");
            dynamicControlsForm.ChangeInputStateButton.Click();
            Assert.IsTrue(dynamicControlsForm.TextInputTextBox.State.WaitForNotEnabled(), "Text input should be disabled after changing state");
        }

        [Test]
        public void Should_BePossibleTo_WaitForElementDisplayedState()
        {
            dynamicControlsForm.RemoveAddExampleButton.Click();
            Assert.IsTrue(dynamicControlsForm.ExampleCheckbox.State.WaitForNotDisplayed(), "Checkbox example should not be displayed after removing");
            dynamicControlsForm.RemoveAddExampleButton.Click();
            Assert.IsTrue(dynamicControlsForm.ExampleCheckbox.State.WaitForDisplayed(), "Checkbox example should be displayed after adding");
        }

        [Test]
        public void Should_BePossibleTo_WaitForElementExistState()
        {
            dynamicControlsForm.RemoveAddExampleButton.Click();
            Assert.IsTrue(dynamicControlsForm.ExampleCheckbox.State.WaitForNotExist(), "Checkbox example should not be exist after removing");
            dynamicControlsForm.RemoveAddExampleButton.Click();
            Assert.IsTrue(dynamicControlsForm.ExampleCheckbox.State.WaitForExist(), "Checkbox example should be exist after adding");
        }

        [Test]
        public void Should_BePossibleTo_WaitForElementIsClickable()
        {
            Assert.Throws<WebDriverTimeoutException>(() => dynamicControlsForm.TextInputTextBox.State.WaitForClickable(), "Textbox should not be clickable");
            dynamicControlsForm.ChangeInputStateButton.Click();
            dynamicControlsForm.TextInputTextBox.State.WaitForClickable();
            Assert.IsTrue(dynamicControlsForm.TextInputTextBox.State.IsClickable, "Textbox should not be clickable after changing state");
        }
    }
}
