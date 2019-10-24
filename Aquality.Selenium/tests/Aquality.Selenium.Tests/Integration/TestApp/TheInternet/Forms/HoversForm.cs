using Aquality.Selenium.Core.Elements;
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
            return ElementFactory.GetLabel(By.XPath(string.Format(HiddenElementTmpLoc, (int)example)), $"Hidden element for {example} example", state);
        }

        public ILabel GetExample(HoverExample example)
        {
            return ElementFactory.GetLabel(By.XPath(string.Format(ExampleTmpLoc, (int)example)), $"{example} example");
        }
    }

    public enum HoverExample
    {
        First = 1,
        Second = 2,
        Third = 3
    }
}
