using Aquality.Selenium.Elements;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.UITests.Forms.TheInternet
{
    public class HoversForm : Form
    {
        private static readonly By FormLocator = By.XPath("//h3[contains(.,'Hovers')]");
        private const string ExampleTmpLoc = "//div[@class='figure'][{0}]";
        private const string HiddenElementTmpLoc = "//a[contains(@href,'users/{0}')]";

        public HoversForm() : base(FormLocator, "Hovers")
        {
        }

        public ILabel GetHiddenElement(HoverExample example, ElementState state = ElementState.Displayed)
        {
            var xpath = string.Format(HiddenElementTmpLoc, ((int)example).ToString());
            return ElementFactory.GetLabel(By.XPath(xpath), $"Hidden element for {example} example", state);
        }

        public ILabel GetExample(HoverExample example)
        {
            var xpath = string.Format(ExampleTmpLoc, ((int) example).ToString());
            return ElementFactory.GetLabel(By.XPath(xpath), $"{example} example");
        }
    }

    public enum HoverExample
    {
        First = 1,
        Second = 2,
        Third = 3
    }
}
