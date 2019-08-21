using System.Collections.Generic;
using Aquality.Selenium.Elements.Actions;

namespace Aquality.Selenium.Elements.Interfaces
{
    /// <summary>
    /// Describes behavior of ComboBox UI element.
    /// </summary>
    public interface IComboBox : IElement
    {
        /// <summary>
        /// Gets ComboBox specific JavaScript actions.
        /// </summary>
        /// <value>Instance of <see cref="ComboBoxJsActions"/></value>
        new ComboBoxJsActions JsActions { get; }

        /// <summary>
        /// Gets text of selected option.
        /// </summary>
        /// <value>Text representation of selected option.</value>
        string SelectedText { get; }

        /// <summary>
        /// Gets text of selected option via JavaScript.
        /// </summary>
        /// <value>Text representation of selected option.</value>
        string SelectedTextByJs { get; }

        /// <summary>
        /// Gets list of available values.
        /// </summary>    
        /// <value>List of available values.</value>
        IList<string> Values { get; }

        /// <summary>
        /// Selects option by its index in the list. 
        /// </summary>
        /// <param name="index">Index of option in the list.</param>
        void SelectByIndex(int index);

        /// <summary>
        /// Selects option by its text. 
        /// </summary>
        /// <param name="text">Text of option.</param>
        void SelectByText(string text);

        /// <summary>
        /// Selects option by its value. 
        /// </summary>
        /// <param name="value">Value of option.</param>
        void SelectByValue(string value);

        /// <summary>
        /// Selects option by partial text. 
        /// </summary>
        /// <param name="text">Partial text of option.</param>
        void SelectByContainingText(string text);

        /// <summary>
        /// Selects option by partial value. 
        /// </summary>
        /// <param name="value">Partial value of option.</param>
        void SelectByContainingValue(string value);
    }
}
