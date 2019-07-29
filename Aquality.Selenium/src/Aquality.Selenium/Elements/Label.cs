using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System;

namespace Aquality.Selenium.Elements
{
    public class Label : Element, ILabel
    {
        protected Label(By locator, string name) : this(locator, name, ElementState.Displayed)
        {
        }

        internal Label(By locator, string name, ElementState state) : base(locator, name, state)
        {
        }

        protected override string ElementType => throw new NotImplementedException();
    }
}
