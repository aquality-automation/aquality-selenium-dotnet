using Aquality.Selenium.Forms;
using Aquality.Selenium.Tests.Utilities;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.UITests.Forms.AutomationPractice
{
    public class WelcomeForm : Form
    {
        private const string FormName = "Welcom form";
        private const string TmpExampleLoc = "//a[contains(@href,'{0}')]";
        private static readonly By FormLocator = By.XPath("//h1[contains(.,'Welcome to the-internet')]");

        public WelcomeForm() : base(FormLocator, FormName)
        {
        }

        public void SelectExample(AvailableExample example)
        {
            var menuItemXpath = string.Format(TmpExampleLoc, example.GetDescription());
            ElementFactory.GetLink(By.XPath(menuItemXpath), example.ToString()).Click();
        }
    }
}
