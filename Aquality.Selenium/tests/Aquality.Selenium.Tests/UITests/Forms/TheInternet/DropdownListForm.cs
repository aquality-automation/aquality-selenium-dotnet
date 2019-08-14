using System.Collections.Generic;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.UITests.Forms.AutomationPractice
{
    public class DropdownListForm : Form
    {
        private const string FormName = "Dropdown List";
        private static readonly By DropdownLocator = By.Id("dropdown");
        private IComboBox CbbDropdown => ElementFactory.GetComboBox(DropdownLocator, "Dropdown");
        public static readonly IDictionary<DropdownValue, string> DropdownValues = new Dictionary<DropdownValue, string>
        {
            {DropdownValue.SelectTest, "Please select an option"},
            {DropdownValue.Option1, "Option 1"},
            {DropdownValue.Option2, "Option 2"}
        };

        public DropdownListForm() : base(DropdownLocator, FormName)
        {
        }

        public IEnumerable<string> GetValuesViaJs()
        {
            return CbbDropdown.JsActions.GetValues();
        }

        public string GetSelectedTextViaJs()
        {
            return CbbDropdown.JsActions.GetSelectedText();
        }

        public void SelectByTextViaJs(DropdownValue option)
        {
            CbbDropdown.JsActions.SelectValueByText(DropdownValues[option]);
        }
    }

    public enum DropdownValue
    {
        SelectTest,
        Option1,
        Option2
    }
}
