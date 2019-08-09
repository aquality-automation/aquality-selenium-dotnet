using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Localization;
using OpenQA.Selenium;

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

        protected override string ElementType => LocalizationManager.Instance.GetLocalizedMessage("loc.link");

        public string Href => GetAttribute("href");
    }
}
