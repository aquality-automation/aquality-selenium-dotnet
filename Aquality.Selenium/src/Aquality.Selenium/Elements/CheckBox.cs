using Aquality.Selenium.Elements.Actions;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Localization;
using Aquality.Selenium.Logging;
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
                Logger.InfoLoc("loc.checkbox.get.state");
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
            Logger.InfoLoc("loc.setting.value", state.ToString());
            if (state && !IsChecked)
            {
                Logger.InfoLoc("loc.checkbox.check", Name, ElementType, bool.TrueString);
                Click();
            }
            else if (!state && IsChecked)
            {
                Logger.InfoLoc("loc.checkbox.uncheck", Name, ElementType, bool.FalseString);
                Click();
            }
        }
    }
}
