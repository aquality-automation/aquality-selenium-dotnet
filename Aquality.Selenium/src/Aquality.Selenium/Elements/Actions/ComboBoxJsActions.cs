using System.Collections.Generic;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;

namespace Aquality.Selenium.Elements.Actions
{
    /// <summary>
    /// Allows to perfrom actions on elements via JavaScript specific for ComboBoxes.
    /// </summary>
    public class ComboBoxJsActions : JsActions
    {
        public ComboBoxJsActions(IElement element, string elementType) : base(element, elementType)
        {
        }

        /// <summary>
        /// Gets values from ComboBox.
        /// </summary>
        /// <returns>List of values</returns>
        public IList<string> GetValues()
        {
            return ExecuteScript<IList<string>>(JavaScript.GetComboBoxValues);
        }

        /// <summary>
        /// Gets text of selected option.
        /// </summary>
        /// <returns>Selected option text.</returns>
        public string GetSelectedText()
        {
            return ExecuteScript<string>(JavaScript.GetComboBoxText);
        }

        /// <summary>
        /// Select value by option's text.
        /// </summary>
        /// <param name="text">Target option.</param>
        public void SelectValueByText(string text)
        {
            ExecuteScript(JavaScript.SelectComboBoxValueByText, text);
        }
    }
}
