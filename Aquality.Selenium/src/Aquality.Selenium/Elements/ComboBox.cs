﻿using Aquality.Selenium.Elements.Actions;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Localization;
using Aquality.Selenium.Logging;
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

        protected override string ElementType => LocalizationManager.Instance.GetLocalizedMessage("loc.combobox");

        public string SelectedText => DoWithRetry(() => new SelectElement(GetElement()).SelectedOption.Text);

        public string SelectedTextByJs => JsActions.GetSelectedText();

        public IList<string> Values
        {
            get
            {
                Logger.InfoLoc("loc.combobox.get.values");
                return DoWithRetry(() =>
                {
                    var options = new SelectElement(GetElement()).Options;
                    return options.Select(option => string.IsNullOrEmpty(option.Text) ? option.GetAttribute(Attributes.Value) : option.Text).ToList();
                });
            }
        }

        public new ComboBoxJsActions JsActions => new ComboBoxJsActions(this, ElementType);

        public void SelectByContainingText(string text)
        {
            Logger.InfoLoc("loc.selecting.value");
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
            Logger.InfoLoc("loc.selecting.value");
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
            Logger.InfoLoc("loc.selecting.value");
            DoWithRetry(() => new SelectElement(GetElement()).SelectByIndex(index));
        }

        public void SelectByText(string text)
        {
            Logger.InfoLoc("loc.selecting.value");
            DoWithRetry(() => new SelectElement(GetElement()).SelectByText(text));
        }

        public void SelectByValue(string value)
        {
            Logger.InfoLoc("loc.selecting.value");
            DoWithRetry(() => new SelectElement(GetElement()).SelectByValue(value));
        }
    }
}
