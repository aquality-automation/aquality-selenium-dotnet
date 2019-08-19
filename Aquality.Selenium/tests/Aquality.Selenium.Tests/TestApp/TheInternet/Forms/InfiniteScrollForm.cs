using System.Collections.Generic;
using System.Linq;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.TestApp.TheInternet.Forms
{
    internal class InfiniteScrollForm : Form
    {
        private static readonly By FormLocator = By.XPath("//h3[contains(.,'Infinite Scroll')]");
        public IList<ILabel> LblExamples => ElementFactory.FindElements(By.XPath("//div[contains(@class,'jscroll-added')]"), ElementFactory.GetLabel);

        public InfiniteScrollForm() : base(FormLocator, "Infinite Scroll")
        {
        }

        public ILabel GetLastExample()
        {
            return LblExamples.Last();
        }
    }
}
