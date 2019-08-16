using System;
using System.Collections.Generic;
using System.Linq;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.UITests.Forms.TheInternet
{
    public class DropdownListForm : Form
    {
        private static readonly By DropdownLocator = By.Id("dropdown");
        public IComboBox CbbDropdown => ElementFactory.GetComboBox(DropdownLocator, "Dropdown");
        private static readonly IDictionary<DropdownValue, string> DropdownValues = new Dictionary<DropdownValue, string>
        {
            {DropdownValue.SelectTest, "Please select an option"},
            {DropdownValue.Option1, "Option 1"},
            {DropdownValue.Option2, "Option 2"}
        };

        public DropdownListForm() : base(DropdownLocator, "Dropdown List")
        {
        }

        public string GetDropdownText(DropdownValue value)
        {
            if (!DropdownValues.TryGetValue(value, out var result))
            {
                throw new ArgumentOutOfRangeException($"{value} could not be processed for dropdown.");
            }
            return result;
        }

        public IList<string> GetDropdownValues()
        {
            return DropdownValues.Values.ToList();
        }
    }

    public enum DropdownValue
    {
        SelectTest,
        Option1,
        Option2
    }
}
