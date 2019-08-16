using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.UITests.Forms.TheInternet
{
    public class KeyPressesForm : Form
    {
        private const string FormName = "Key Presses";
        private static readonly By FormLocator = By.XPath("//h3[contains(.,'Key Presses')]");
        public ITextBox TxtInput => ElementFactory.GetTextBox(By.Id("target"), "Input");

        public KeyPressesForm() : base(FormLocator, FormName)
        {
        }
    }
}
