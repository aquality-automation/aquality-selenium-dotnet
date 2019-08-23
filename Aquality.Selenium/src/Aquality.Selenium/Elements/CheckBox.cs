using Aquality.Selenium.Elements.Actions;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Localization;
using OpenQA.Selenium;

namespace Aquality.Selenium.Elements
{
    /// <summary>
    /// Defines CheckBox UI element.
    /// </summary>
    public class CheckBox : Element, ICheckBox
    {
        protected internal CheckBox(By locator, string name, ElementState state) : base(locator, name, state)
        {
        }

        protected override string ElementType => LocalizationManager.Instance.GetLocalizedMessage("loc.checkbox");

        public bool IsChecked
        {
            get
            {
                LogElementAction("loc.checkbox.get.state");
                return GetElement().Selected;
            }
        }

        public new CheckBoxJsActions JsActions => new CheckBoxJsActions(this, ElementType);
        
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
            SetState(!IsChecked);
        }

        private void SetState(bool state)
        {
            LogElementAction("loc.setting.value", state);
            if (state != IsChecked)
            {
                Click();
            }
        }
    }
}
