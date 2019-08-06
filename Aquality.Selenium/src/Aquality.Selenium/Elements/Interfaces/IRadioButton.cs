namespace Aquality.Selenium.Elements.Interfaces
{
    /// <summary>
    /// Describes behavior of RadioButton UI element.
    /// </summary>
    public interface IRadioButton : IElement
    {
        /// <summary>
        /// Gets RadioButton state.
        /// </summary>
        /// <value>True if checked and false otherwise.</value>
        bool IsChecked { get; }
    }
}
