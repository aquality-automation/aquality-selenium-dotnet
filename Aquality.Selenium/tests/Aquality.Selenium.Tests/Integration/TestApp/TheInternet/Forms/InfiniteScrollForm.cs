using System.Collections.Generic;
using System.Linq;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class InfiniteScrollForm : TheInternetForm
    {
        public InfiniteScrollForm() : base(By.XPath("//h3[contains(.,'Infinite Scroll')]"), "Infinite Scroll")
        {
        }

        protected override string UrlPart => "infinite_scroll";

        public IList<ILabel> ExampleLabels => ElementFactory.FindElements(By.XPath("//div[contains(@class,'jscroll-added')]"), ElementFactory.GetLabel);

        public ILabel LastExampleLabel => ExampleLabels.Last();
    }
}
