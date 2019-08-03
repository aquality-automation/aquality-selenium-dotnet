using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System;

namespace Aquality.Selenium.Elements
{
    /// <summary>
    /// Defines Link UI element.
    /// </summary>
    public class Link : Element, ILink
    {
        protected internal Link(By locator, string name, ElementState state) : base(locator, name, state)
        {
        }

        protected override string ElementType => throw new NotImplementedException();

        public string Href => throw new NotImplementedException();
    }
}
