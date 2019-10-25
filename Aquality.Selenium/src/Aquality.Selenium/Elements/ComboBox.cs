using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Elements.Actions;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
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

        public string SelectedText => DoWithRetry(() => new SelectElement(GetElement()).SelectedOption.Text);

        public string SelectedValue => DoWithRetry(() => new SelectElement(GetElement()).SelectedOption.GetAttribute(Attributes.Value));

        public IList<string> Texts
        {
            get
            {
                LogElementAction("loc.combobox.get.texts");
                return DoWithRetry(() =>  new SelectElement(GetElement()).Options.Select(option => option.Text).ToList());
            }
        }

        public IList<string> Values
        {
            get
            {
                LogElementAction("loc.combobox.get.values");
                return DoWithRetry(() => new SelectElement(GetElement()).Options.Select(option => option.GetAttribute(Attributes.Value)).ToList());
            }
        }

        public new ComboBoxJsActions JsActions => new ComboBoxJsActions(this, ElementType, LocalizedLogger, BrowserProfile);

        public void SelectByContainingText(string text)
        {
            LogElementAction("loc.selecting.value");
            DoWithRetry(() =>
            {
                var select = new SelectElement(GetElement());
                foreach (var element in select.Options)
                {
                    var elementText = element.Text;
                    if (elementText.ToLower().Contains(text.ToLower()))
                    {
                        select.SelectByText(elementText);
                        return;
                    }
                }
                throw new InvalidElementStateException($"Failed to select option that contains text {text}");
            });
        }

        public void SelectByContainingValue(string value)
        {
            LogElementAction("loc.selecting.value");
            DoWithRetry(() =>
            {
                var select = new SelectElement(GetElement());
                foreach (var element in select.Options)
                {
                    var elementValue = element.GetAttribute(Attributes.Value);
                    if (elementValue.ToLower().Contains(value.ToLower()))
                    {
                        select.SelectByValue(elementValue);
                        return;
                    }
                }
                throw new InvalidElementStateException($"Failed to select option that contains value {value}");
            });
        }

        public void SelectByIndex(int index)
        {
            LogElementAction("loc.selecting.value");
            DoWithRetry(() => new SelectElement(GetElement()).SelectByIndex(index));
        }

        public void SelectByText(string text)
        {
            LogElementAction("loc.selecting.value");
            DoWithRetry(() => new SelectElement(GetElement()).SelectByText(text));
        }

        public void SelectByValue(string value)
        {
            LogElementAction("loc.selecting.value");
            DoWithRetry(() => new SelectElement(GetElement()).SelectByValue(value));
        }
    }
}
