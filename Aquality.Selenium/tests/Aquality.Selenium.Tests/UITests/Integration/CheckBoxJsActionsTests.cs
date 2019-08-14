using Aquality.Selenium.Tests.UITests.Forms.AutomationPractice;
using NUnit.Framework;

namespace Aquality.Selenium.Tests.UITests.Integration
{
    public class CheckBoxJsActionsTests : UITest
    {
        [Test]
        public void Should_BeAbleGetCheckboxState_WithJsActions()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.SelectExample(AvailableExample.Checkboxes);
            var checkboxesForm = new CheckboxesForm();
            Assert.Multiple(() =>
            {
                Assert.IsFalse(checkboxesForm.GetFirstStateViaJs(), "First checkbox should be unchecked");
                Assert.IsTrue(checkboxesForm.GetSecondStateViaJs(), "Second checkbox should be checked");
            });
        }
    }
}
