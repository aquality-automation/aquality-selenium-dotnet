using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.UITests.Forms.TheInternet
{
    public class CheckboxesForm : Form
    {
        private static readonly By FormLocator = By.Id("checkboxes");
        public ICheckBox CbxFirst => ElementFactory.GetCheckBox(By.XPath("//input[1]"), "First checkBox");
        public ICheckBox CbxSecond => ElementFactory.GetCheckBox(By.XPath("//input[2]"), "Second checkBox");

        public CheckboxesForm() : base(FormLocator, "Checkboxes")
        {
        }
    }
}
