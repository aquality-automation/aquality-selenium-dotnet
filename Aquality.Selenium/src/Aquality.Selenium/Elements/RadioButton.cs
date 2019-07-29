using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System;

namespace Aquality.Selenium.Elements
{
    public class RadioButton : Element, IRadioButton
    {
        protected RadioButton(By locator, string name) : this(locator, name, ElementState.Displayed)
        {
        }

        internal RadioButton(By locator, string name, ElementState state) : base(locator, name, state)
        {
        }

        public bool IsChecked => throw new NotImplementedException();

        protected override string ElementType => throw new NotImplementedException();
    }
}
