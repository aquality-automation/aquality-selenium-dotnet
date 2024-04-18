using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration
{
    internal class ElementStateTests : UITest
    {
        private readonly DynamicControlsForm dynamicControlsForm = new();

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
                Assert.That(dynamicControlsForm.ChangeInputStateButton.State.IsEnabled, "Change state button should be enabled");
                Assert.That(dynamicControlsForm.TextInputTextBox.State.IsEnabled, Is.False, "Text input should be disabled");
            });
        }
        
        [Test]
        public void Should_BePossibleTo_CheckDisplayedElementState()
        {
            Assert.Multiple(() =>
            {
                Assert.That(dynamicControlsForm.TextInputTextBox.State.IsDisplayed, "Text input should be displayed");
                Assert.That(dynamicControlsForm.LoadingLabel.State.IsDisplayed, Is.False, "Loading should not be displayed");
            });
        }

        [Test]
        public void Should_BePossibleTo_CheckExistElementState()
        {
            Assert.That(dynamicControlsForm.LoadingLabel.State.IsExist, Is.False, "Loading element should not be exist by default");
            dynamicControlsForm.ChangeInputStateButton.Click();
            Assert.That(dynamicControlsForm.LoadingLabel.State.IsExist, "Loading element should not exist after changing state");
        }

        [Test]
        public void Should_BePossibleTo_CheckElementIsClickableState()
        {
            Assert.Multiple(() =>
            {
                Assert.That(dynamicControlsForm.ChangeInputStateButton.State.IsClickable, "Change state button should be clickable");
                Assert.That(dynamicControlsForm.TextInputTextBox.State.IsClickable, Is.False, "Text input should not be clickable");
            });
        }

        [Test]
        public void Should_BePossibleTo_WaitForElementEnabledState()
        {
            Assert.That(dynamicControlsForm.State.WaitForDisplayed(), "Form 'Dynamic Controls' should be displayed");
            dynamicControlsForm.ChangeInputStateButton.Click();
            Assert.That(dynamicControlsForm.TextInputTextBox.State.WaitForEnabled(), "Text input should be enable after changing state");
            dynamicControlsForm.ChangeInputStateButton.Click();
            Assert.That(dynamicControlsForm.TextInputTextBox.State.WaitForNotEnabled(), "Text input should be disabled after changing state");
        }

        [Test]
        public void Should_BePossibleTo_WaitForElementDisplayedState()
        {
            dynamicControlsForm.RemoveAddExampleButton.Click();
            Assert.That(dynamicControlsForm.ExampleCheckbox.State.WaitForNotDisplayed(), "Checkbox example should not be displayed after removing");
            dynamicControlsForm.RemoveAddExampleButton.Click();
            Assert.That(dynamicControlsForm.ExampleCheckbox.State.WaitForDisplayed(), "Checkbox example should be displayed after adding");
        }

        [Test]
        public void Should_BePossibleTo_WaitForElementExistState()
        {
            dynamicControlsForm.RemoveAddExampleButton.Click();
            Assert.That(dynamicControlsForm.ExampleCheckbox.State.WaitForNotExist(), "Checkbox example should not be exist after removing");
            dynamicControlsForm.RemoveAddExampleButton.Click();
            Assert.That(dynamicControlsForm.ExampleCheckbox.State.WaitForExist(), "Checkbox example should be exist after adding");
        }

        [Test]
        public void Should_BePossibleTo_WaitForElementIsClickable()
        {
            Assert.Throws<WebDriverTimeoutException>(() => dynamicControlsForm.TextInputTextBox.State.WaitForClickable(), "Textbox should not be clickable");
            dynamicControlsForm.ChangeInputStateButton.Click();
            dynamicControlsForm.TextInputTextBox.State.WaitForClickable();
            Assert.That(dynamicControlsForm.TextInputTextBox.State.IsClickable, "Textbox should not be clickable after changing state");
        }
    }
}
