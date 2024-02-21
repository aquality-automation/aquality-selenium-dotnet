using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Linq;

namespace Aquality.Selenium.Elements
{
    /// <summary>
    /// Defines MultiChoiceBox UI element.
    /// </summary>
    public class MultiChoiceBox : ComboBox, IMultiChoiceBox
    {
        private const string LocDeselectingValue = "loc.deselecting.value";

        protected internal MultiChoiceBox(By locator, string name, ElementState state) : base(locator, name, state)
        {
        }

        protected override string ElementType => LocalizationManager.GetLocalizedMessage("loc.multichoicebox");

        public IList<string> SelectedValues
        {
            get
            {
                LogElementAction("loc.combobox.getting.selected.value");
                var values = DoWithRetry(() => new SelectElement(GetElement()).AllSelectedOptions.Select(option => option.GetAttribute(Attributes.Value)).ToList());
                LogElementAction("loc.combobox.selected.value", string.Join(", ", values.Select(value => $"'{value}'")));
                return values;
            }
        }

        public IList<string> SelectedTexts
        {
            get
            {
                LogElementAction("loc.combobox.getting.selected.text");
                var texts = DoWithRetry(() => new SelectElement(GetElement()).AllSelectedOptions.Select(option => option.Text).ToList());
                LogElementAction("loc.combobox.selected.text", string.Join(", ", texts.Select(value => $"'{value}'")));
                return texts;
            }
        }

        public void DeselectAll()
        {
            LogElementAction("loc.multichoicebox.deselect.all");
            DoWithRetry(() => new SelectElement(GetElement()).DeselectAll());
        }

        public void DeselectByContainingText(string text)
        {
            LogElementAction("loc.multichoicebox.deselect.by.text", text);
            ApplyFunctionToOptionsThatContain(element => element.Text,
                    (select, option) => select.DeselectByText(option),
                    text);
        }

        public void DeselectByContainingValue(string value)
        {
            LogElementAction(LocDeselectingValue, value);
            ApplyFunctionToOptionsThatContain(element => element.GetAttribute(Attributes.Value),
                    (select, option) => select.DeselectByValue(option),
                    value);
        }

        public void DeselectByIndex(int index)
        {
            LogElementAction(LocDeselectingValue, $"#{index}");
            DoWithRetry(() => new SelectElement(GetElement()).DeselectByIndex(index));
        }

        public void DeselectByText(string text)
        {
            LogElementAction("loc.multichoicebox.deselect.by.text", text);
            DoWithRetry(() => new SelectElement(GetElement()).DeselectByText(text));
        }

        public void DeselectByValue(string value)
        {
            LogElementAction(LocDeselectingValue, value);
            DoWithRetry(() => new SelectElement(GetElement()).DeselectByValue(value));
        }

        public void SelectAll()
        {
            LogElementAction("loc.multichoicebox.select.all");
            ApplyFunctionToOptionsThatContain(element => element.GetAttribute(Attributes.Value),
                    (select, value) => select.SelectByValue(value),
                    string.Empty);
        }
    }
}
