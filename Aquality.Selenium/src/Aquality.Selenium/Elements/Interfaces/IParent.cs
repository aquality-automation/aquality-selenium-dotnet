using OpenQA.Selenium;

namespace Aquality.Selenium.Elements.Interfaces
{
    /// <summary>
    /// Defines behavior of element with child elements.
    /// </summary>
    public interface IParent
    {
        /// <summary>
        /// Finds child element of current element by its locator.
        /// </summary>
        /// <typeparam name="T">Type of child element that has to implement IElement</typeparam>
        /// <param name="childLocator">Locator of child element.</param>
        /// <param name="supplier">Delegate that defines constructor of child element in case of custom element</param>
        /// <param name="state">Child element state</param>
        /// <returns>Instance of child element</returns>
        T FindChildElement<T>(By childLocator, ElementSupplier<T> supplier = null, ElementState state = ElementState.Displayed) where T : IElement;
    }
}
