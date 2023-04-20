using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Elements.Actions;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aquality.Selenium.Elements
{
    /// <summary>
    /// Defines ComboBox UI element.
    /// </summary>
    public class ComboBox : Element, IComboBox
    {
        protected internal ComboBox(By locator, string name, ElementState state) : base(locator, name, state)
        {
        }

        protected override string ElementType => LocalizationManager.GetLocalizedMessage("loc.combobox");

        public string SelectedText
        {
            get
            {
                LogElementAction("loc.combobox.getting.selected.text");
                var text = DoWithRetry(() => new SelectElement(GetElement()).SelectedOption.Text);
                LogElementAction("loc.combobox.selected.text", text);
                return text;
            }
        }

        public string SelectedValue
        {
            get
            {
                LogElementAction("loc.combobox.getting.selected.value");
                var value = DoWithRetry(() => new SelectElement(GetElement()).SelectedOption.GetAttribute(Attributes.Value));
                LogElementAction("loc.combobox.selected.value", value);
                return value;
            }
        }

        public IList<string> Texts
        {
            get
            {
                LogElementAction("loc.combobox.get.texts");
                var values = DoWithRetry(() =>  new SelectElement(GetElement()).Options.Select(option => option.Text).ToList());
                LogElementAction("loc.combobox.texts", string.Join(", ", values.Select(value => $"'{value}'")));
                return values;
            }
        }

        public IList<string> Values
        {
            get
            {
                LogElementAction("loc.combobox.get.values");
                var values = DoWithRetry(() => new SelectElement(GetElement()).Options.Select(option => option.GetAttribute(Attributes.Value)).ToList());
                LogElementAction("loc.combobox.values", string.Join(", ", values.Select(value => $"'{value}'")));
                return values;
            }
        }

        public new ComboBoxJsActions JsActions => new ComboBoxJsActions(this, ElementType, LocalizedLogger, BrowserProfile);

        public void SelectByContainingText(string text)
        {
            LogElementAction("loc.combobox.select.by.text", text);
            ApplyFunctionToOptionsThatContain(element => element.Text, (select, option) => select.SelectByText(text), text);
        }

        public void SelectByContainingValue(string value)
        {
            LogElementAction("loc.selecting.value", value);
            ApplyFunctionToOptionsThatContain(
                element => element.GetAttribute(Attributes.Value), (select, option) => select.SelectByValue(option), value);
        }

        protected void ApplyFunctionToOptionsThatContain(Func<IWebElement, string> getValueFunction, Action<SelectElement, string> selectFunction, string value)
        {
            DoWithRetry(() =>
            {
                var select = new SelectElement(GetElement());
                var isSelected = false;
                foreach (var element in select.Options)
                {
                    var elementValue = getValueFunction(element);
                    if (elementValue.ToLower().Contains(value.ToLower()))
                    {
                        selectFunction(select, elementValue);
                        isSelected = true;
                    }
                }
                if (!isSelected)
                {
                    throw new InvalidElementStateException($"Failed to find option that contains [{value}]");
                }
            });
        }

        public void SelectByIndex(int index)
        {
            LogElementAction("loc.selecting.value", $"#{index}");
            DoWithRetry(() => new SelectElement(GetElement()).SelectByIndex(index));
        }

        public void SelectByText(string text)
        {
            LogElementAction("loc.combobox.select.by.text", text);
            DoWithRetry(() => new SelectElement(GetElement()).SelectByText(text));
        }

        public void SelectByValue(string value)
        {
            LogElementAction("loc.selecting.value", value);
            DoWithRetry(() => new SelectElement(GetElement()).SelectByValue(value));
        }
    }
}
