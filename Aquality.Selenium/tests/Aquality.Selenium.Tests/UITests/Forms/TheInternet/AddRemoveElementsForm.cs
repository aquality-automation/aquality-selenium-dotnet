using System.Collections.Generic;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.UITests.Forms.AutomationPractice
{
    public class AddRemoveElementsForm : Form
    {
        private const string FormName = "Add/Remove Elements";
        private static readonly By FormLocator = By.XPath("//h3[contains(.,'Add/Remove Elements')]");
        public IButton BtnAdd => ElementFactory.GetButton(By.XPath("//button[contains(@onclick,'addElement')]"), "Add element");
        public IList<IButton> BtnsDelete => ElementFactory.FindElements(By.XPath("//button[contains(@class,'added-manually')]"), ElementFactory.GetButton);

        public AddRemoveElementsForm() : base(FormLocator, FormName)
        {
        }
    }
}
