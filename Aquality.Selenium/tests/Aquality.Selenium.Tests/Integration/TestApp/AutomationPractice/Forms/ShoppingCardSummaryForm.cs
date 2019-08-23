using Aquality.Selenium.Elements;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Forms
{
    internal class ShoppingCardSummaryForm : Form
    {
        private const string QuantityLabelLocatorTemplate = "//input[contains(@name, 'quantity')][@value='{0}']";

        public ShoppingCardSummaryForm() 
            : base(By.XPath("//input[contains(@name, 'quantity')]"), "Shopping Card Summary")
        {
        }

        private IButton PlusButton => ElementFactory.GetButton(By.XPath("//i[@class='icon-plus']"), "Plus");
        private IButton ProceedToCheckoutButton => ElementFactory.GetButton(By.XPath("//a[contains(@class, 'checkout')]"), "Proceed Checkout");        

        public void ClickPlusButton()
        {
            PlusButton.JsActions.Click();
        }

        public int WaitForQuantityAndGetValue(int expectedQuantity)
        {
            var quantityLabel = ElementFactory.GetLabel(By.XPath(string.Format(QuantityLabelLocatorTemplate, expectedQuantity)), "Count of items", ElementState.ExistsInAnyState);
            return int.Parse(quantityLabel.GetAttribute("value"));
        }

        public void ClickProceedToCheckoutButton()
        {
            ProceedToCheckoutButton.JsActions.Click();
        }
    }
}
