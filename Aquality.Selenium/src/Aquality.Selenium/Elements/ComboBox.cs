using Aquality.Selenium.Elements.Actions;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace Aquality.Selenium.Elements
{
    public class ComboBox : Element, IComboBox
    {
        protected ComboBox(By locator, string name) : this(locator, name, ElementState.Displayed)
        {
        }

        internal ComboBox(By locator, string name, ElementState state) : base(locator, name, state)
        {
        }

        protected override string ElementType => throw new NotImplementedException();

        public string SelectedText => throw new NotImplementedException();

        public string SelectedTextByJs => throw new NotImplementedException();

        public IList<string> Values => throw new NotImplementedException();

        ComboBoxJsActions IComboBox.JsActions => new ComboBoxJsActions(this, ElementType);

        public void SelectByContainingText(string partialText)
        {
            throw new NotImplementedException();
        }

        public void SelectByContainingValue(string partialValue)
        {
            throw new NotImplementedException();
        }

        public void SelectByIndex(int index)
        {
            throw new NotImplementedException();
        }

        public void SelectByText(string text)
        {
            throw new NotImplementedException();
        }

        public void SelectByValue(string value)
        {
            throw new NotImplementedException();
        }
    }
}
