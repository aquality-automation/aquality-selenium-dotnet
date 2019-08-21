using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Integration.TestApp;
using Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Forms;
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
            Assert.IsTrue(new DropdownForm().IsDisplayed, "Dropdown form should be displayed");
        }

        [Test]
        public void Should_BePossibleTo_DoubleClick()
        {
            var addRemoveElementsForm = new AddRemoveElementsForm();
            addRemoveElementsForm.Open();
            addRemoveElementsForm.AddBtn.MouseActions.DoubleClick();
            var addedButtonsCount = addRemoveElementsForm.ListOfDeleteBtns.Count;
            Assert.AreEqual(2, addedButtonsCount, "2 elements should be added after double click");
        }

        [Test]
        public void Should_BePossibleTo_RightClick()
        {
            var contextMenuForm = new ContextMenuForm();
            contextMenuForm.Open();
            contextMenuForm.LblHotSpot.MouseActions.RightClick();
            Assert.DoesNotThrow(() => BrowserManager.Browser.HandleAlert(AlertAction.Decline), "Alert should be opened after right click");
        }

        [Ignore("should be updated")]
        [Test]
        public void Should_BePossibleTo_MoveMouseToElement()
        {
            BrowserManager.Browser.GoTo(Constants.UrlAutomationPractice);
            var productList = new ProductListForm();
            productList.NavigateToLastProduct();

            var product = new ProductForm();
            product.GetLastProductView().MouseActions.MoveMouseToElement();
            var classAttribute = product.GetLastProductView().GetAttribute("class");
            Assert.IsTrue(classAttribute.Contains("shown"), "Product view should be shown after move mouse");
        }
    }
}
