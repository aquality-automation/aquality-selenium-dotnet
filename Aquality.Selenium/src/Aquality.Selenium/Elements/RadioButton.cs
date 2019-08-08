using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Localization;
using OpenQA.Selenium;

namespace Aquality.Selenium.Elements
{
    /// <summary>
    /// Defines RadioButton UI element.
    /// </summary>
    public class RadioButton : Element, IRadioButton
    {
        protected internal RadioButton(By locator, string name, ElementState state) : base(locator, name, state)
        {
        }

        protected override string ElementType => LocalizationManager.Instance.GetLocalizedMessage("loc.radio");

        public bool IsChecked => GetElement().Selected;
    }
}
