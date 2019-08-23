using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Modals
{
    internal class ProceedToCheckoutModal : Form
    {
        public ProceedToCheckoutModal() : base(By.XPath("//a[@title='Proceed to checkout']"), "Proceed To Checkout Modal")
        {
        }

        private IButton ProceedToCheckoutButton => ElementFactory.GetButton(By.XPath("//a[@title='Proceed to checkout']"), "Proceed Checkout");

        public void ClickProceedToCheckoutButton()
        {
            ProceedToCheckoutButton.JsActions.ClickAndWait();
        }
    }
}
