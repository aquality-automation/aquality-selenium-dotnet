using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class DynamicContentForm : Form
    {
        private static readonly string ContentItemXpathTmpl = "//div[@id='content']//div[@class='large-10 columns'][{0}]";

        public DynamicContentForm() : base(By.Id("content"), "Dynamic Content")
        {
        }

        public ILabel GetContentItem(int index)
        {
            return ElementFactory.GetLabel(By.XPath(string.Format(ContentItemXpathTmpl, index)), $"Content item #{index}");
        }
    }
}
