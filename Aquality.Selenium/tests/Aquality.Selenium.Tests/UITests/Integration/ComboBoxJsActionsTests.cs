using System.Linq;
using Aquality.Selenium.Tests.UITests.Forms.AutomationPractice;
using NUnit.Framework;

namespace Aquality.Selenium.Tests.UITests.Integration
{
    public class ComboBoxJsActionsTests : UITest
    {
        [Test]
        public void Should_BeAbleGetValues_WithJsActions()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.SelectExample(AvailableExample.Dropdown);

            var dropdownListForm = new DropdownListForm();
            var selectedText = dropdownListForm.GetSelectedTextViaJs();
            var expectedText = DropdownListForm.DropdownValues[DropdownValue.SelectTest];

            var actualValues = dropdownListForm.GetValuesViaJs();
            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedText, selectedText, $"Selected option by default should be '{expectedText}'");
                Assert.AreEqual(DropdownListForm.DropdownValues.Values.ToList(), actualValues, "Dropdown values should be the same as expected.");
            });
        }

        [Test]
        public void Should_BeAbleSelectValue_WithJsActions()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.SelectExample(AvailableExample.Dropdown);

            var dropdownListForm = new DropdownListForm();
            dropdownListForm.SelectByTextViaJs(DropdownValue.Option1);
            var selectedText = dropdownListForm.GetSelectedTextViaJs();
            var expectedText = DropdownListForm.DropdownValues[DropdownValue.Option1];
            Assert.AreEqual(expectedText, selectedText, $"Selected option should be '{expectedText}'");
        }
    }
}
