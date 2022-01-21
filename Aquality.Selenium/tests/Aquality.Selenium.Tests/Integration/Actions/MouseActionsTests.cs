using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;

namespace Aquality.Selenium.Tests.Integration.Actions
{
    internal class MouseActionsTests : UITest
    {
        [Test]
        public void Should_BePossibleTo_Click()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.Open();
            welcomeForm.GetExampleLink(AvailableExample.Dropdown).MouseActions.Click();
            Assert.IsTrue(new DropdownForm().State.WaitForDisplayed(), "Dropdown form should be displayed");
        }

        [Test]
        public void Should_BePossibleTo_DoubleClick()
        {
            var addRemoveElementsForm = new AddRemoveElementsForm();
            addRemoveElementsForm.Open();
            addRemoveElementsForm.AddButton.MouseActions.DoubleClick();
            var addedButtonsCount = addRemoveElementsForm.ListOfDeleteButtons.Count;
            Assert.AreEqual(2, addedButtonsCount, "2 elements should be added after double click");
        }

        [Test]
        public void Should_BePossibleTo_RightClick()
        {
            var contextMenuForm = new ContextMenuForm();
            contextMenuForm.Open();
            contextMenuForm.HotSpotLabel.MouseActions.RightClick();
            Assert.DoesNotThrow(() => AqualityServices.Browser.HandleAlert(AlertAction.Decline), "Alert should be opened after right click");
        }

        [Test]
        public void Should_BePossibleTo_MoveToElement()
        {
            var menuForm = new JQueryMenuForm();
            menuForm.Open();
            menuForm.EnabledButton.MouseActions.MoveToElement();
            Assert.IsTrue(menuForm.IsEnabledButtonFocused, "Element should be focused after move mouse");
        }
    }
}
