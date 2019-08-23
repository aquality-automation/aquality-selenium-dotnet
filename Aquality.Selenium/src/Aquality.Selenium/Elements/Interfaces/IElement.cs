﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Aquality.Selenium.Elements.Actions;

namespace Aquality.Selenium.Elements.Interfaces
{
    /// <summary>
    /// Describes behavior of any UI element.
    /// </summary>
    public interface IElement : IParent
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
        /// Gets element state.
        /// </summary>
        /// <value>Instance of <see cref="IElementStateProvider"/></value>
        IElementStateProvider State { get; }

        /// <summary>
        /// Unique locator of element.
        /// </summary>
        /// <value>Instance of <see cref="By"/></value>
        By Locator { get; }

        /// <summary>
        /// Unique name of element.
        /// </summary>
        /// <value>String representation of element name.</value>
        string Name { get; }

        /// <summary>
        /// Finds current element by specified <see cref="Locator"/>
        /// </summary>
        /// <param name="timeout">Timeout to find element. Default: <see cref="Configurations.ITimeoutConfiguration.Condition"/></param>
        /// <returns>Instance of <see cref="RemoteWebElement"/> if found.</returns>
        /// <exception cref="NoSuchElementException">Thrown when no elements found.</exception>
        RemoteWebElement GetElement(TimeSpan? timeout = null);

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
        /// Sends keys to element.
        /// </summary>
        /// <param name="key">Key to send.</param>
        void SendKeys(string key);

        /// <summary>
        /// Clicks the element.
        /// </summary>
        void Click();

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
    }
}
