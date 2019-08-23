using System;
using System.Collections.Generic;
using System.Linq;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Forms
{
    internal class ProductListForm : Form
    {
        private IEnumerable<ILink> LnksProduct => ElementFactory.FindElements<ILink>(By.XPath("//ul[@id='homefeatured']//a[@class='product_img_link']"));

        public ProductListForm() : base(By.XPath("//ul[@id='homefeatured']"), "Product list")
        {
        }

        public IList<ILabel> ProductContainerLabels => ElementFactory.FindElements<ILabel>(By.XPath("//ul[@id='homefeatured']//div[@class='product-container']"), state: ElementState.Displayed, expectedCount: ElementsCount.MoreThenZero);

        public void NavigateToLastProduct()
        {
            BrowserManager.Browser.GoTo(GetLastProduct().Href);
        }

        public ILink GetLastProduct()
        {
            return LnksProduct.Last();
        }

        public int GetNumberOfProductsInContainer()
        {
            return ProductContainerLabels.Count;
        }

        public void AddRandomProductToCart()
        {
            var products = ProductContainerLabels;
            var productToAdd = products[new Random().Next(products.Count)];
            BrowserManager.Browser.ExecuteScript(JavaScript.ScrollToElement, productToAdd.GetElement());
            productToAdd.MouseActions.MoveMouseToElement();
            GetAddCardButton(productToAdd).JsActions.Click();
        }

        private IButton GetAddCardButton(IElement productItem)
        {
            return productItem.FindChildElement<IButton>(By.XPath("//a[contains(@class, 'add_to_cart')]"));
        }
    }
}
