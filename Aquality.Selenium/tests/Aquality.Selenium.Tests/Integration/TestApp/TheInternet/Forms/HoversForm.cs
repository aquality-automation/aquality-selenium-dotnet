using Aquality.Selenium.Elements;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class HoversForm : TheInternetForm
    {
        private const string ExampleTmpLoc = "//div[@class='figure'][{0}]";
        private const string HiddenElementTmpLoc = "//a[contains(@href,'users/{0}')]";

        public HoversForm() : base(By.XPath("//h3[contains(.,'Hovers')]"), "Hovers")
        {
        }

        protected override string UrlPart => "hovers";

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
