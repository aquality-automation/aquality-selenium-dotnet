using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Elements;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Forms
{
    internal sealed class ProductTabContentForm : Form
    {
        private static readonly By DottedXPath = By.XPath(".//ul[@id='blockbestsellers']//li[not(@style='display:none')]");
        private static readonly By BestSellersById = By.Id("blockbestsellers");

        public ProductTabContentForm() : base(By.ClassName("tab-content"), "Product tab content")
        {
        }

        public IList<Label> GetListElements(ElementState state, ElementsCount count)
        {
            return ElementFactory.FindElements<Label>(By.XPath("//ul[@id='blockbestsellers']//li"), state: state, expectedCount: count);
        }

        public IList<Label> GetListElementsByNonXPath(ElementState state, ElementsCount count)
        {
            return ElementFactory.FindElements<Label>(BestSellersById, state: state, expectedCount: count);
        }

        public Label GetChildElementByNonXPath(ElementState state)
        {
            return FindChildElement<Label>(BestSellersById, state: state);
        }

        public IList<Label> GetListElementsByDottedXPath(ElementState state, ElementsCount count)
        {
            return ElementFactory.FindElements<Label>(DottedXPath, state: state, expectedCount: count);
        }

        public IList<Label> GetChildElementsByDottedXPath(ElementState state, ElementsCount count)
        {
            return FindChildElements<Label>(DottedXPath, state: state, expectedCount: count);
        }
    }
}
