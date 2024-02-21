using System.Collections.Generic;

namespace Aquality.Selenium.Elements.Interfaces
{
    /// <summary>
    /// Describes behavior of MultiChoice ComboBox UI element.
    /// </summary>
    public interface IMultiChoiceBox : IComboBox
    {
        /// <summary>
        /// Gets value of all selected options.
        /// </summary>
        /// <value>Selected values.</value>
        IList<string> SelectedValues { get; }

        /// <summary>
        /// Gets text of all selected options.
        /// </summary>
        /// <value>Selected texts.</value>
        IList<string> SelectedTexts { get; }

        /// <summary>
        /// Select all options
        /// </summary>
        void SelectAll();
                
        /// <summary>
        /// De-select all options
        /// </summary>
        void DeselectAll();

        /// <summary>
        /// De-select selected option by index
        /// </summary>
        /// <param name="index">Number of selected option.</param>
        void DeselectByIndex(int index);

        /// <summary>
        /// De-select selected option by value.
        /// </summary>
        /// <param name="value">Option value.</param>
        void DeselectByValue(string value);

        /// <summary>
        /// De-select selected option by containing value.
        /// </summary>
        /// <param name="value">Partial option's value.</param>
        void DeselectByContainingValue(string value);

        /// <summary>
        /// De-select selected option by visible text.
        /// </summary>
        /// <param name="text">Text to be deselected.</param>
        void DeselectByText(string text);

        /// <summary>
        /// De-select selected option by containing visible text.
        /// </summary>
        /// <param name="text">Partial option's text.</param>
        void DeselectByContainingText(string text);
    }
}
