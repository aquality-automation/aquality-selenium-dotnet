using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System;

namespace Aquality.Selenium.Elements
{
    public class TextBox : Element, ITextBox
    {
        protected TextBox(By locator, string name) : this(locator, name, ElementState.Displayed)
        {
        }

        internal TextBox(By locator, string name, ElementState state) : base(locator, name, state)
        {
        }

        protected override string ElementType => throw new NotImplementedException();

        public string Value => throw new NotImplementedException();

        public void Type(string value, bool secret = false)
        {
            throw new NotImplementedException();
        }

        public void ClearAndType(string value, bool secret = false)
        {
            throw new NotImplementedException();
        }

        public void Submit()
        {
            throw new NotImplementedException();
        }
    }
}
