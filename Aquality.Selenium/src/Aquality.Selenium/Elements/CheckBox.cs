using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Elements.Actions;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Elements
{
    /// <summary>
    /// Defines CheckBox UI element.
    /// </summary>
    public class CheckBox : CheckableElement, ICheckBox
    {
        protected internal CheckBox(By locator, string name, ElementState state) : base(locator, name, state)
        {
        }

        protected override string ElementType => LocalizationManager.GetLocalizedMessage("loc.checkbox");

        public new CheckBoxJsActions JsActions => new CheckBoxJsActions(this, ElementType, LocalizedLogger, BrowserProfile);
        
        public void Check()
        {
            SetState(true);
        }

        public void Uncheck()
        {
            SetState(false);
        }

        public void Toggle()
        {
            SetState(!GetState());
        }

        private void SetState(bool state)
        {
            LogElementAction("loc.setting.value", state);
            if (state != GetState())
            {
                Click();
            }
        }
    }
}
