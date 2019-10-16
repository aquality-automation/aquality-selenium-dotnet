using System;
using System.Collections.Generic;
using System.Linq;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Forms
{
    internal class ProductListForm : Form
    {
        private readonly Random random = new Random();

        private const string FormLocator = "//ul[@id='homefeatured']";

        public ProductListForm() : base(By.XPath(FormLocator), "Product list")
        {
        }

        private IEnumerable<ILink> LnksProduct => ElementFactory.FindElements<ILink>(By.XPath(FormLocator + "//a[@class='product_img_link']"));
        private IList<ILabel> ProductContainerLabels => ElementFactory.FindElements<ILabel>(By.XPath(FormLocator + "//div[@class='product-container']"), state: ElementState.Displayed, expectedCount: ElementsCount.MoreThenZero);

        private By AddCartButtonLocator => By.XPath("//a[contains(@class, 'add_to_cart')]");

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
            var productToAdd = products[random.Next(products.Count)];
            BrowserManager.Browser.ExecuteScript(JavaScript.ScrollToElement, productToAdd.GetElement());
            productToAdd.MouseActions.MoveToElement();
            GetAddCardButton(productToAdd).JsActions.Click();
        }

        private IButton GetAddCardButton(IElement productItem)
        {
            return productItem.FindChildElement<IButton>(AddCartButtonLocator);
        }
    }
}
