using System.Collections.Generic;
using System.Linq;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.Forms.AutomationPractice
{
    internal class ProductListForm : Form
    {
        private static readonly By ProductsLocator = By.XPath("//ul[@id='homefeatured']");
        private IEnumerable<ILink> LnksProduct => ElementFactory.FindElements(By.XPath("//ul[@id='homefeatured']//a[@class='product_img_link']"), ElementFactory.GetLink);

        public ProductListForm() : base(ProductsLocator, "Product list")
        {
        }

        public void NavigateToLastProduct()
        {
            BrowserManager.Browser.Navigate().GoToUrl(GetLastProduct().Href);
        }

        public ILink GetLastProduct()
        {
            return LnksProduct.Last();
        }
    }
}
