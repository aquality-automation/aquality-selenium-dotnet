namespace Aquality.Selenium.Elements.Interfaces
{
    /// <summary>
    /// Describes element with different states.
    /// </summary>
    public interface IElementWithState
    {
        /// <summary>
        /// Gets element state.
        /// </summary>
        /// <value>Instance of <see cref="Aquality.Selenium.Elements.Interfaces.IElementStateProvider"/></value>
        IElementStateProvider State { get; }

        /// <summary>
        /// Checks whether element class attribute has desired className or not.
        /// </summary>
        /// <param name="className">Value of class attribute.</param>
        /// <returns>True if contains and false otherwise.</returns>
        bool ContainsClassAttribute(string className);
    }
}
