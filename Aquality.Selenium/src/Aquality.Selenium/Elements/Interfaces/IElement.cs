using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Elements.Actions;
using OpenQA.Selenium;
using System.Collections.Generic;
using ICoreElement = Aquality.Selenium.Core.Elements.Interfaces.IElement;

namespace Aquality.Selenium.Elements.Interfaces
{
    /// <summary>
    /// Describes behavior of any UI element.
    /// </summary>
    public interface IElement : ICoreElement
    {
        /// <summary>
        /// Gets JavaScript actions that can be performed with an element.
        /// </summary>
        /// <value>Instance of <see cref="Actions.JsActions"/></value>
        JsActions JsActions { get; }

        /// <summary>
        /// Gets Mouse actions that can be performed with an element.
        /// </summary>
        /// <value>Instance of <see cref="Actions.MouseActions"/></value>
        MouseActions MouseActions { get; }

        /// <summary>
        /// Gets element text.
        /// </summary>
        /// <param name="highlightState">Should the element be highlighted or not. 
        /// Default value is from configuration: <seealso cref="Configurations.IBrowserProfile.IsElementHighlightEnabled"/></param>
        /// <returns>String representation of element text.</returns>
        string GetText(HighlightState highlightState = HighlightState.Default);

        /// <summary>
        /// Gets element attribute value by its name.
        /// </summary>
        /// <param name="attr">Name of attribute</param>
        /// <param name="highlightState">Should the element be highlighted or not. 
        /// Default value is from configuration: <seealso cref="Configurations.IBrowserProfile.IsElementHighlightEnabled"/></param>
        /// <returns>Value of element attribute.</returns>
        string GetAttribute(string attr, HighlightState highlightState = HighlightState.Default);

        /// <summary>
        /// Gets css value of the element.
        /// </summary>
        /// <param name="propertyName">Name of css property</param>
        /// <param name="highlightState">Should the element be highlighted or not. 
        /// Default value is from configuration: <seealso cref="Configurations.IBrowserProfile.IsElementHighlightEnabled"/></param>
        /// <returns>Value of element attribute.</returns>
        string GetCssValue(string propertyName, HighlightState highlightState = HighlightState.Default);

        /// <summary>
        /// Clicks the element and waits for page to load.
        /// </summary>
        void ClickAndWait();

        /// <summary>
        /// Waits for page to load and click the element.
        /// </summary>
        void WaitAndClick();

        /// <summary>
        /// Set focus on element.
        /// </summary>
        void Focus();

        /// <summary>
        /// Sets element inner HTML.
        /// </summary>
        /// <param name="value">Value to set.</param>
        void SetInnerHtml(string value);

        /// <summary>
        /// Send key.
        /// </summary>
        /// <param name="key"> Key for sending.</param>
        void SendKey(Key key);

        /// <summary>
        /// Expands shadow root.
        /// </summary>
        /// <returns><see cref="ShadowRoot"/> search context.</returns>
        ShadowRoot ExpandShadowRoot();

        /// <summary>
        /// Finds element in the shadow root of the current element.
        /// </summary>
        /// <typeparam name="T">Type of the target element that has to implement <see cref="IElement"/>.</typeparam>
        /// <param name="locator">Locator of the target element. 
        /// Note that some browsers don't support XPath locator for shadow elements. 
        /// Therefore, we suggest to use CSS selectors.</param>
        /// <param name="name">Name of the target element.</param>
        /// <param name="supplier">Delegate that defines constructor of element.</param>
        /// <param name="state">State of the target element.</param>
        /// <returns>Instance of element.</returns>
        T FindElementInShadowRoot<T>(By locator, string name, ElementSupplier<T> supplier = null, ElementState state = ElementState.Displayed) 
            where T : IElement;

        /// <summary>
        /// Finds elements in the shadow root of the current element.
        /// </summary>
        /// <typeparam name="T">Type of the target elements that has to implement <see cref="IElement"/>.</typeparam>
        /// <param name="locator">Locator of target elements. 
        /// Note that some browsers don't support XPath locator for shadow elements.
        /// Therefore, we suggest to use CSS selectors</param>
        /// <param name="name">Name of target elements.</param>
        /// <param name="supplier">Delegate that defines constructor of element.</param>
        /// <param name="expectedCount">Expected number of elements that have to be found (zero, more then zero, any).</param>
        /// <param name="state">State of target elements.</param>
        /// <returns>List of found elements.</returns>
        IList<T> FindElementsInShadowRoot<T>(By locator, string name = null, ElementSupplier<T> supplier = null, ElementsCount expectedCount = ElementsCount.Any, ElementState state = ElementState.Displayed) 
            where T : IElement;
    }
}
