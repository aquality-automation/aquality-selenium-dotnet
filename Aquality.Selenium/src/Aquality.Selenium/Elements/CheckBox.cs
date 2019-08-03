using Aquality.Selenium.Elements.Actions;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System;

namespace Aquality.Selenium.Elements
{
    /// <summary>
    /// Defines CheckBox UI element.
    /// </summary>
    public class CheckBox : Element, ICheckBox
    {
        protected internal CheckBox(By locator, string name, ElementState state) : base(locator, name, state)
        {
        }

        protected override string ElementType => throw new NotImplementedException();

        public bool IsChecked => throw new NotImplementedException();

        CheckBoxJsActions ICheckBox.JsActions => new CheckBoxJsActions(this, ElementType);

        public void Check()
        {
            throw new NotImplementedException();
        }

        public void Toggle()
        {
            throw new NotImplementedException();
        }

        public void Uncheck()
        {
            throw new NotImplementedException();
        }
    }
}
