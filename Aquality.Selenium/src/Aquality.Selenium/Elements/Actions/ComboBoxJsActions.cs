using System.Collections.Generic;
using System.Linq;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Elements.Interfaces;

namespace Aquality.Selenium.Elements.Actions
{
    /// <summary>
    /// Allows to perform actions on elements via JavaScript specific for ComboBoxes.
    /// </summary>
    public class ComboBoxJsActions : JsActions
    {
        public ComboBoxJsActions(IElement element, string elementType, ILocalizedLogger logger, IBrowserProfile browserProfile) 
            : base(element, elementType, logger, browserProfile)
        {
        }

        /// <summary>
        /// Gets texts of options from ComboBox.
        /// </summary>
        /// <returns>List of options' texts</returns>
        public IList<string> GetTexts()
        {
            LogElementAction("loc.combobox.get.texts.js");
            var values = ExecuteScript<IList<object>>(JavaScript.GetComboBoxTexts).Select(item => item.ToString()).ToList();
            LogElementAction("loc.combobox.texts", string.Join(", ", values.Select(value => $"'{value}'")));
            return values;
        }

        /// <summary>
        /// Gets text of selected option.
        /// </summary>
        /// <returns>Selected option text.</returns>
        public string GetSelectedText()
        {
            LogElementAction("loc.combobox.get.text.js");
            var text = ExecuteScript<string>(JavaScript.GetComboBoxSelectedText);
            LogElementAction("loc.combobox.selected.text", text);
            return text;
        }

        /// <summary>
        /// Select value by option's text.
        /// </summary>
        /// <param name="text">Target option.</param>
        public void SelectValueByText(string text)
        {
            LogElementAction("loc.combobox.select.by.text.js", text);
            ExecuteScript(JavaScript.SelectComboBoxValueByText, text);
        }
    }
}
