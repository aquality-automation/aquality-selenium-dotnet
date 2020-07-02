using Aquality.Selenium.Elements.Actions;
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
        /// <param name="highlightState">Should the element be hightlighted or not. 
        /// Default value is from configuration: <seealso cref="Configurations.IBrowserProfile.IsElementHighlightEnabled"/></param>
        /// <returns>String representation of element text.</returns>
        string GetText(HighlightState highlightState = HighlightState.Default);

        /// <summary>
        /// Gets element attribute value by its name.
        /// </summary>
        /// <param name="attr">Name of attrbiute</param>
        /// <param name="highlightState">Should the element be hightlighted or not. 
        /// Default value is from configuration: <seealso cref="Configurations.IBrowserProfile.IsElementHighlightEnabled"/></param>
        /// <returns>Value of element attribute.</returns>
        string GetAttribute(string attr, HighlightState highlightState = HighlightState.Default);

        /// <summary>
        /// Gets css value of the element.
        /// </summary>
        /// <param name="propertyName">Name of css property</param>
        /// <param name="highlightState">Should the element be hightlighted or not. 
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
    }
}
