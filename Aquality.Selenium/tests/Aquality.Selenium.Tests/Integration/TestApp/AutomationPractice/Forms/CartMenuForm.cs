using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Forms
{
    internal class CartMenuForm : Form
    {
        public CartMenuForm() : base(By.Id("button_order_cart"), "Cart Menu")
        {
        }

        private IButton ProductsButton => ElementFactory.GetButton(By.XPath("//span[@class='ajax_cart_product_txt_s']"), "Products");
        private IButton OrderButton => ElementFactory.GetButton(By.XPath("//a[@id='button_order_cart']"), "Order Card");

        public void OpenCartMenu()
        {
            ProductsButton.MouseActions.MoveMouseToElement();
        }

        public void ClickCheckoutButton()
        {
            OrderButton.ClickAndWait();
        }
    }
}
