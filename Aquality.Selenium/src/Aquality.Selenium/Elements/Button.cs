using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System;

namespace Aquality.Selenium.Elements
{
    /// <summary>
    /// Defines Button UI element.
    /// </summary>
    public class Button : Element, IButton
    {
        protected internal Button(By locator, string name, ElementState state) : base(locator, name, state)
        {
        }

        protected override string ElementType => throw new NotImplementedException();
    }
}
