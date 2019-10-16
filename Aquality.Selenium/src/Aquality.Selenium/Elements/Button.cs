using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using ElementState = Aquality.Selenium.Core.Elements.ElementState;

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

        protected override string ElementType => LocalizationManager.GetLocalizedMessage("loc.button");
    }
}
