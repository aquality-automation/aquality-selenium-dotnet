namespace Aquality.Selenium.Elements.Interfaces
{
    /// <summary>
    /// Describes behavior of TextBox UI element.
    /// </summary>
    public interface ITextBox : IElement
    {
        /// <summary>
        /// Gets text value of an element.
        /// </summary>
        /// <value>String representation of element's value</value>
        string Value { get; }

        /// <summary>
        /// Type text in an element.
        /// </summary>
        /// <param name="value">Text to type.</param>
        /// <param name="secret">Should the typing text be hidden in logs or not. False by default.</param>
        void Type(string value, bool secret = false);

        /// <summary>
        /// Clear element text and type value.
        /// </summary>
        /// <param name="value">Text to type.</param>
        /// <param name="secret">Should the typing text be hidden in logs or not. False by default.</param>
        void ClearAndType(string value, bool secret = false);

        /// <summary>
        /// Submit typed value.
        /// </summary>
        void Submit();

        /// <summary>
        /// Set focus on element.
        /// </summary>
        new void Focus();
    }
}
