using System.Collections.Generic;
using System.Linq;
using Aquality.Selenium.Elements;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using Aquality.Selenium.Waitings;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.UITests.Forms.AutomationPractice
{
    public class ProductListForm : Form
    {
        private const string FormName = "Product list";
        private static readonly By ProductsLocator = By.XPath("//ul[@id='homefeatured']");
        
        private IEnumerable<ILabel> LblsProduct => ElementFactory.FindElements(
            By.XPath("//ul[@id='homefeatured']//li[contains(@class,'product')]"), ElementFactory.GetLabel, ElementsCount.MoreThenZero, ElementState.ExistsInAnyState);

        private ILabel LblFooter => ElementFactory.GetLabel(By.XPath("//section[contains(@class,'bottom-footer')]"),
            "Footer", ElementState.ExistsInAnyState);

        public ProductListForm() : base(ProductsLocator, FormName)
        {
            ConditionalWait.WaitForTrue(driver => IsDisplayed);
        }

        public void ScrollToLastProduct()
        {
            GetLastProduct().JsActions.ScrollToTheCenter();
        }

        public bool IsLastProductVisible()
        {
            return GetLastProduct().State.IsDisplayed;
        }

        public bool IsFooterVisible()
        {
            return LblFooter.State.IsDisplayed;
        }

        private ILabel GetLastProduct()
        {
            return LblsProduct.Last();
        }
    }
}
