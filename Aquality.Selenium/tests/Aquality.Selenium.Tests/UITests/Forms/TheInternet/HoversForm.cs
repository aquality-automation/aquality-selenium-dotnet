using System.Collections.Generic;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.UITests.Forms.AutomationPractice
{
    public class HoversForm : Form
    {
        private const string FormName = "Hovers";
        private static readonly By FormLocator = By.XPath("//h3[contains(.,'Hovers')]");
        private const string ExampleTmpLoc = "//div[@class='figure'][{0}]";
        private const string HiddenElementTmpLoc = "//a[contains(@href,'users/{0}')]";

        public HoversForm() : base(FormLocator, FormName)
        {
        }

        public void SetFocusViaJs(HoverExample example)
        {
            GetExample(example).JsActions.SetFocus();
        }

        public bool IsHiddenElementVisible(HoverExample example)
        {
            return GetHiddenElement(example).State.IsDisplayed;
        }

        private ILabel GetHiddenElement(HoverExample example)
        {
            var xpath = string.Format(HiddenElementTmpLoc, ((int)example).ToString());
            return ElementFactory.GetLabel(By.XPath(xpath), $"Hidden element for {example} example");
        }

        private ILabel GetExample(HoverExample example)
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
