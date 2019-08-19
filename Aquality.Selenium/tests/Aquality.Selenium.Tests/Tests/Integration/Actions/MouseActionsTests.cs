using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Constants;
using Aquality.Selenium.Tests.Integration.Forms.AutomationPractice;
using Aquality.Selenium.Tests.Integration.Forms.TheInternet;
using NUnit.Framework;

namespace Aquality.Selenium.Tests.Integration.Actions
{
    internal class MouseActionsTests : UITest
    {
        [Test]
        public void Should_BeAbleClick_WithMouseActions()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.GetExampleLink(AvailableExample.Dropdown).MouseActions.Click();
            Assert.IsTrue(new DropdownListForm().IsDisplayed, "Dropdown form should be displayed");
        }

        [Test]
        public void Should_BeAbleDoubleClick_WithMouseActions()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.GetExampleLink(AvailableExample.AddRemoveElements).Click();

            var addRemoveElementsForm = new AddRemoveElementsForm();
            addRemoveElementsForm.BtnAdd.MouseActions.DoubleClick();
            var addedButtonsCount = addRemoveElementsForm.BtnsDelete.Count;
            Assert.AreEqual(2, addedButtonsCount, "2 elements should be added after double click");
        }

        [Test]
        public void Should_BeAbleRightClick_WithMouseActions()
        {
            var welcomeForm = new WelcomeForm();
            welcomeForm.GetExampleLink(AvailableExample.ContextMenu).Click();

            var contextMenuForm = new ContextMenuForm();
            contextMenuForm.LblHotSpot.MouseActions.RightClick();
            Assert.DoesNotThrow(()=> BrowserManager.Browser.HandleAlert(AlertAction.Decline), "Alert should be opened after right click");
        }

        [Test]
        public void Should_BeAbleMoveMouse_WithMouseActions()
        {
            BrowserManager.Browser.Navigate().GoToUrl(TestConstants.UrlAutomationPractice);
            var productList = new ProductListForm();
            productList.NavigateToLastProduct();

            var product = new ProductForm();
            product.GetLastProductView().MouseActions.MoveMouseToElement();
            var classAttribute = product.GetLastProductView().GetAttribute("class");
            Assert.IsTrue(classAttribute.Contains("shown"), "Product view should be shown after move mouse");
        }
    }
}
