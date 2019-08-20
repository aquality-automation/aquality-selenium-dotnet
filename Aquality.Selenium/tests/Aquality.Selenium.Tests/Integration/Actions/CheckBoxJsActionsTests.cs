using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;

namespace Aquality.Selenium.Tests.Integration.Actions
{
    internal class CheckBoxJsActionsTests : UITest
    {
        [Test]
        public void Should_BeAbleGetCheckboxState_WithJsActions()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.SelectExample(AvailableExample.Checkboxes);
            var checkboxesForm = new CheckboxesForm();
            Assert.Multiple(() =>
            {
                Assert.IsFalse(checkboxesForm.CbxFirst.JsActions.GetState(), "First checkbox should be unchecked");
                Assert.IsTrue(checkboxesForm.CbxSecond.JsActions.GetState(), "Second checkbox should be checked");
            });
        }
    }
}
