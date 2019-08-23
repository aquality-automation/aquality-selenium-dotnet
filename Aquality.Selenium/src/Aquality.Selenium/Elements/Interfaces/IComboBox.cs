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
        /// Gets value of selected option.
        /// </summary>
        /// <value>Value representation of selected option.</value>
        string SelectedValue { get; }

        /// <summary>
        /// Gets text of selected option.
        /// </summary>
        /// <value>Text representation of selected option.</value>
        string SelectedText { get; }

        /// <summary>
        /// Gets list of text of available options.
        /// </summary>    
        /// <value>List of text of available options.</value>
        IList<string> Texts { get; }

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
