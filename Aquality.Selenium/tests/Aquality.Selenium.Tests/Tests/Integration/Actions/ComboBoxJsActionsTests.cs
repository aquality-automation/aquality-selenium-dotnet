using Aquality.Selenium.Tests.Integration.Forms.TheInternet;
using NUnit.Framework;

namespace Aquality.Selenium.Tests.Integration.Actions
{
    internal class ComboBoxJsActionsTests : UITest
    {
        [Test]
        public void Should_BeAbleGetValues_WithJsActions()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.SelectExample(AvailableExample.Dropdown);

            var dropdownListForm = new DropdownListForm();
            var selectedText = dropdownListForm.CbbDropdown.JsActions.GetSelectedText();
            var expectedText = dropdownListForm.GetDropdownText(DropdownValue.SelectTest);

            var actualValues = dropdownListForm.CbbDropdown.JsActions.GetValues();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedText, selectedText, $"Selected option by default should be '{expectedText}'");
                Assert.AreEqual(dropdownListForm.GetDropdownValues(), actualValues, "Dropdown values should be the same as expected.");
            });
        }

        [Test]
        public void Should_BeAbleSelectValue_WithJsActions()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.SelectExample(AvailableExample.Dropdown);

            var dropdownListForm = new DropdownListForm();
            var expectedText = dropdownListForm.GetDropdownText(DropdownValue.Option1);
            dropdownListForm.CbbDropdown.JsActions.SelectValueByText(expectedText);
            var selectedText = dropdownListForm.CbbDropdown.JsActions.GetSelectedText();
            Assert.AreEqual(expectedText, selectedText, $"Selected option should be '{expectedText}'");
        }
    }
}
