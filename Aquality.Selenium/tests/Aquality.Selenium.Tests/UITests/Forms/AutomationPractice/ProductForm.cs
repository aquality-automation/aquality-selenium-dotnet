using System.Collections.Generic;
using System.Linq;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.UITests.Forms.AutomationPractice
{
    public class ProductForm : Form
    {
        private const string FormName = "Product";
        private static readonly By ProductsLocator = By.Id("product");
        public IEnumerable<ILabel> LblsProductView => ElementFactory.FindElements(By.XPath("//li[contains(@id,'thumbnail_')]//a"), ElementFactory.GetLabel);
        public ITextBox TxtQuantity => ElementFactory.GetTextBox(By.Id("quantity_wanted"), "Quantity");
        
        public ProductForm() : base(ProductsLocator, FormName)
        {
        }

        public ILabel GetLastProductView()
        {
            return LblsProductView.Last();
        }
    }
}
