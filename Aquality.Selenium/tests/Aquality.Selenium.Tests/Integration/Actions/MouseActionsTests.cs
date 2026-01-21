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
            Assert.That(new DropdownForm().State.WaitForDisplayed(), "Dropdown form should be displayed");
        }

        [Test]
        public void Should_BePossibleTo_DoubleClick()
        {
            var addRemoveElementsForm = new AddRemoveElementsForm();
            addRemoveElementsForm.Open();
            addRemoveElementsForm.AddButton.MouseActions.DoubleClick();
            var addedButtonsCount = addRemoveElementsForm.ListOfDeleteButtons.Count;
            Assert.That(addedButtonsCount, Is.EqualTo(2), "2 elements should be added after double click");
        }

        [Test]
        public void Should_BePossibleTo_RightClick()
        {
            var contextMenuForm = new ContextMenuForm();
            contextMenuForm.Open();
            contextMenuForm.HotSpotLabel.MouseActions.RightClick();
            Assert.DoesNotThrow(() => AqualityServices.Browser.HandleAlert(AlertAction.Decline), "Alert should be opened after right click");
        }

        [Test, Category(RetriesGroup), Retry(RetriesCount)]
        public void Should_BePossibleTo_MoveToElement()
        {
            var menuForm = new JQueryMenuForm();
            menuForm.Open();
            JQueryMenuForm.EnabledButton.MouseActions.MoveToElement();
            Assert.That(JQueryMenuForm.IsEnabledButtonFocused, "Element should be focused after move mouse");
        }

        [Test]
        public void Should_BePossibleTo_ScrollToElement()
        {
            var infiniteScrollForm = new InfiniteScrollForm();
            infiniteScrollForm.Open();
            infiniteScrollForm.WaitForPageToLoad();
            var size = infiniteScrollForm.LastExampleLabel.Visual.Size;
            AqualityServices.Browser.SetWindowSize(size.Width, size.Height);
            var defaultCount = infiniteScrollForm.ExampleLabels.Count;
            Assert.DoesNotThrow(
                () => AqualityServices.ConditionalWait.WaitForTrue(() =>
                {
                    infiniteScrollForm.LastExampleLabel.MouseActions.ScrollToElement();
                    return infiniteScrollForm.ExampleLabels.Count > defaultCount;
                }), "Some examples should be added after scroll");
        }

        [Test]
        public void Should_BePossibleTo_ScrollFromOrigin()
        {
            var infiniteScrollForm = new InfiniteScrollForm();
            infiniteScrollForm.Open();
            infiniteScrollForm.WaitForPageToLoad();
            var defaultCount = infiniteScrollForm.ExampleLabels.Count;
            Assert.DoesNotThrow(
                () => AqualityServices.ConditionalWait.WaitForTrue(() =>
                {
                    var formHeight = infiniteScrollForm.Size.Height;
                    infiniteScrollForm.LastExampleLabel.MouseActions.ScrollFromOrigin(0, formHeight);
                    return infiniteScrollForm.ExampleLabels.Count > defaultCount;
                }), "Some examples should be added after scroll");
        }
    }
}
