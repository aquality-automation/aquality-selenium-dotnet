using System.Collections.Generic;
using System.Linq;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.UITests.Forms.TheInternet
{
    public class InfiniteScrollForm : Form
    {
        private const string FormName = "Infinite Scroll";
        private static readonly By FormLocator = By.XPath("//h3[contains(.,'Infinite Scroll')]");
        public IList<ILabel> LblExamples => ElementFactory.FindElements(By.XPath("//div[contains(@class,'jscroll-added')]"), ElementFactory.GetLabel);

        public InfiniteScrollForm() : base(FormLocator, FormName)
        {
        }

        public ILabel GetLastExample()
        {
            return LblExamples.Last();
        }
    }
}
