using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Elements.Interfaces;

namespace Aquality.Selenium.Elements.Actions
{
    /// <summary>
    /// Allows to perform actions on elements via JavaScript specific for CheckBoxes.
    /// </summary>
    public class CheckBoxJsActions : JsActions
    {
        public CheckBoxJsActions(IElement element, string elementType, ILocalizedLogger logger, IBrowserProfile browserProfile) 
            : base(element, elementType, logger, browserProfile)
        {
        }

        /// <summary>
        /// Gets CheckBox state.
        /// </summary>
        /// <returns>True if checked and false otherwise</returns>
        public bool IsChecked()
        {
            LogElementAction("loc.checkbox.get.state");
            return ExecuteScript<bool>(JavaScript.GetCheckBoxState);
        }

        /// <summary>
        /// Performs check action on the element.
        /// </summary>
        public void Check()
        {
            SetState(true);
        }

        /// <summary>
        /// Performs uncheck action on the element.
        /// </summary>
        public void Uncheck()
        {
            SetState(false);
        }

        /// <summary>
        /// Performs toggle action on the element.
        /// </summary>
        public void Toggle()
        {
            SetState(!IsChecked());
        }

        private void SetState(bool state)
        {
            LogElementAction("loc.setting.value", state);
            if (state != IsChecked())
            {
                Click();
            }
        }
    }
}
