using System.Collections.Generic;
using System.Linq;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;

namespace Aquality.Selenium.Elements.Actions
{
    /// <summary>
    /// Allows to perform actions on elements via JavaScript specific for ComboBoxes.
    /// </summary>
    public class ComboBoxJsActions : JsActions
    {
        public ComboBoxJsActions(IElement element, string elementType) : base(element, elementType)
        {
        }

        /// <summary>
        /// Gets texts of options from ComboBox.
        /// </summary>
        /// <returns>List of options' texts</returns>
        public IList<string> GetTexts()
        {
            return ExecuteScript<IList<object>>(JavaScript.GetComboBoxTexts).Select(item => item.ToString()).ToList();
        }

        /// <summary>
        /// Gets text of selected option.
        /// </summary>
        /// <returns>Selected option text.</returns>
        public string GetSelectedText()
        {
            return ExecuteScript<string>(JavaScript.GetComboBoxSelectedText);
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
