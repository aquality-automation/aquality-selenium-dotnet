using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;

namespace Aquality.Selenium.Elements.Actions
{
    /// <summary>
    /// Allows to perfrom actions on elements via JavaScript specific for CheckBoxes.
    /// </summary>
    public class CheckBoxJsActions : JsActions
    {
        public CheckBoxJsActions(IElement element, string elementType) : base(element, elementType)
        {
        }

        /// <summary>
        /// Gets state of CheckBox using .checked property of element.
        /// </summary>
        /// <returns>True if checked and false otherwise.</returns>
        public bool GetState()
        {
            Logger.InfoLoc("loc.checkbox.get.state");
            return ExecuteScript<bool>(JavaScript.GetCheckBoxState);
        }
    }
}
