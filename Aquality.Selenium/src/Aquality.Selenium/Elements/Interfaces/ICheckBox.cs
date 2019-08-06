using Aquality.Selenium.Elements.Actions;

namespace Aquality.Selenium.Elements.Interfaces
{
    /// <summary>
    /// Describes behavior of CheckBox UI element.
    /// </summary>
    public interface ICheckBox : IElement
    {
        /// <summary>
        /// Gets CheckBox specific JavaScript actions.
        /// </summary>
        /// <value>Instance of <see cref="Aquality.Selenium.Elements.Actions.CheckBoxJsActions"/></value>
        new CheckBoxJsActions JsActions { get; }

        /// <summary>
        /// Gets CheckBox state.
        /// </summary>
        /// <value>True if checked and false otherwise.</value>
        bool IsChecked { get; }

        /// <summary>
        /// Performs check action on the element.
        /// </summary>
        void Check();

        /// <summary>
        /// Performs uncheck action on the element.
        /// </summary>
        void Uncheck();

        /// <summary>
        /// Performs toggle action on the element.
        /// </summary>
        void Toggle();
    }
}
