using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Tests.Integration.TestApp.Utilities;
using OpenQA.Selenium;
using System.ComponentModel;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class WelcomeForm : TheInternetForm
    {
        private const string TmpExampleLoc = "//a[contains(@href,'{0}')]";
        public const string SubTitle = "Available Examples";

        public WelcomeForm() : base(By.XPath("//h1[contains(.,'Welcome to the-internet')]"), "Welcome to the-internet")
        {
        }

        public ILabel SubTitleLbl => ElementFactory.GetLabel(By.XPath("//h2"), "Sub title");

        protected override string UrlPart => string.Empty;

        public void SelectExample(AvailableExample example)
        {
            GetExampleLink(example).Click();
        }

        public ILink GetExampleLink(AvailableExample example)
        {
            var menuItemXpath = string.Format(TmpExampleLoc, example.GetDescription());
            return ElementFactory.GetLink(By.XPath(menuItemXpath), example.ToString());
        }
    }

    internal enum AvailableExample
    {
        [Description("checkboxes")]
        Checkboxes,
        [Description("dropdown")]
        Dropdown,
        [Description("hovers")]
        Hovers,
        [Description("key_presses")]
        KeyPresses,
        [Description("infinite_scroll")]
        InfiniteScroll,
        [Description("add_remove_elements")]
        AddRemoveElements,
        [Description("context_menu")]
        ContextMenu
    }
}
