using System.Collections.Generic;
using System.Linq;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.UITests.Forms.AutomationPractice
{
    public class InfiniteScrollForm : Form
    {
        private const string FormName = "Infinite Scroll";
        private static readonly By FormLocator = By.XPath("//h3[contains(.,'Infinite Scroll')]");
        private IList<ILabel> LblExamples => ElementFactory.FindElements(By.XPath("//div[contains(@class,'jscroll-added')]"), ElementFactory.GetLabel);

        public InfiniteScrollForm() : base(FormLocator, FormName)
        {
        }

        public int GetExamplesCount()
        {
            return LblExamples.Count;
        }

        public void ScrollIntoViewToLastExample()
        {
            LblExamples.Last().JsActions.ScrollIntoView();
        }

        public void ScrollToTheCenterOfLastExample()
        {
            LblExamples.Last().JsActions.ScrollToTheCenter();
        }

        public void ScrollByCoordinates(int x, int y)
        {
            LblExamples.Last().JsActions.ScrollBy(x, y);
        }
    }
}
