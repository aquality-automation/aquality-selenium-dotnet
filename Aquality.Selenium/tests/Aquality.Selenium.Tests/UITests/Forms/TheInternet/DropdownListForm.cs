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
        public IComboBox CbbDropdown => ElementFactory.GetComboBox(DropdownLocator, "Dropdown");
        public static readonly IDictionary<DropdownValue, string> DropdownValues = new Dictionary<DropdownValue, string>
        {
            {DropdownValue.SelectTest, "Please select an option"},
            {DropdownValue.Option1, "Option 1"},
            {DropdownValue.Option2, "Option 2"}
        };

        public DropdownListForm() : base(DropdownLocator, FormName)
        {
        }
    }

    public enum DropdownValue
    {
        SelectTest,
        Option1,
        Option2
    }
}
