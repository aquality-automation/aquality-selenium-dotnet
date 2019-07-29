using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System;

namespace Aquality.Selenium.Elements
{
    public class Link : Element, ILink
    {
        protected Link(By locator, string name) : this(locator, name, ElementState.Displayed)
        {
        }

        internal Link(By locator, string name, ElementState state) : base(locator, name, state)
        {
        }

        protected override string ElementType => throw new NotImplementedException();

        public string Href => throw new NotImplementedException();
    }
}
