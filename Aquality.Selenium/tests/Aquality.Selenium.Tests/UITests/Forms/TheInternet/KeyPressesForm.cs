using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.UITests.Forms.AutomationPractice
{
    public class KeyPressesForm : Form
    {
        private const string FormName = "Key Presses";
        private static readonly By FormLocator = By.XPath("//h3[contains(.,'Key Presses')]");
        private ITextBox TxtInput => ElementFactory.GetTextBox(By.Id("target"), "Input");

        public KeyPressesForm() : base(FormLocator, FormName)
        {
        }

        public void SetValueViaJs(string text)
        {
            TxtInput.JsActions.SetValue(text);
        }

        public string GetValue()
        {
            return TxtInput.Value;
        }
    }
}
