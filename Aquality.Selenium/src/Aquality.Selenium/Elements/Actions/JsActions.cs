using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Utilities;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Elements.Actions
{
    /// <summary>
    /// Allows to perform actions on elements via JavaScript.
    /// </summary>
    public class JsActions : IShadowRootExpander
    {
        private readonly IElement element;
        private readonly string elementType;
        private readonly IBrowserProfile browserProfile;

        public JsActions(IElement element, string elementType, ILocalizedLogger logger, IBrowserProfile browserProfile)
        {
            this.element = element;
            this.elementType = elementType;
            this.browserProfile = browserProfile;
            Logger = logger;
        }

        private Browser Browser => AqualityServices.Browser;

        private IElementActionRetrier ActionRetrier => AqualityServices.Get<IElementActionRetrier>();

        protected ILocalizedLogger Logger { get; }

        /// <summary>
        /// Expands shadow root.
        /// </summary>
        /// <returns><see cref="ShadowRoot"/> search context.</returns>
        public ShadowRoot ExpandShadowRoot()
        {
            LogElementAction("loc.shadowroot.expand.js");
            return ExecuteScript<ShadowRoot>(JavaScript.ExpandShadowRoot);
        }

        public T FindElementInShadowRoot<T>(By locator, string name, ElementSupplier<T> supplier = null, ElementState state = ElementState.Displayed)
            where T : IElement
        {
            return this.GetShadowRootElementFactory().Get(locator, name, supplier, state);
        }

        public IList<T> FindElementsInShadowRoot<T>(By locator, string name = null, ElementSupplier<T> supplier = null, ElementsCount expectedCount = ElementsCount.Any, ElementState state = ElementState.Displayed)
            where T : IElement
        {
            return this.GetShadowRootElementFactory().FindElements(locator, name, supplier, expectedCount, state);
        }

        /// <summary>
        /// Performs click on element and waits for page is loaded.
        /// </summary>
        public void ClickAndWait()
        {
            Click();
            Browser.WaitForPageToLoad();
        }

        /// <summary>
        /// Performs click on element.
        /// </summary>
        public void Click()
        {
            LogElementAction("loc.clicking.js");
            HighlightElement();
            ExecuteScript(JavaScript.ClickElement);
        }

        /// <summary>
        /// Highlights the element.
        /// Default value is from configuration: <see cref="IBrowserProfile.IsElementHighlightEnabled"/>
        /// </summary>
        public void HighlightElement(HighlightState highlightState = HighlightState.Default)
        {
            if (browserProfile.IsElementHighlightEnabled || highlightState.Equals(HighlightState.Highlight))
            {
                ExecuteScript(JavaScript.BorderElement);
            }
        }

        /// <summary>
        /// Scrolling page to the element.
        /// </summary>
        public void ScrollIntoView()
        {
            LogElementAction("loc.scrolling.js");
            ExecuteScript(JavaScript.ScrollToElement, true);
        }

        /// <summary>
        /// Scrolling element by coordinates.
        /// </summary>
        /// <remarks>Element have to contains inner scroll bar.</remarks>
        /// <param name="x">Horizontal coordinate</param>
        /// <param name="y">Vertical coordinate</param>
        public void ScrollBy(int x, int y)
        {
            LogElementAction("loc.scrolling.js");
            ExecuteScript(JavaScript.ScrollBy, x, y);
        }

        /// <summary>
        /// Scrolling to the center of element.
        /// Upper bound of element will be in the center of the page after scrolling
        /// </summary>
        public void ScrollToTheCenter()
        {
            LogElementAction("loc.scrolling.center.js");
            ExecuteScript(JavaScript.ScrollToElementCenter);
        }

        /// <summary>
        /// Setting value.
        /// </summary>
        /// <param name="value">Value to set</param>
        public void SetValue(string value)
        {
            LogElementAction("loc.setting.value", value);
            ExecuteScript(JavaScript.SetValue, value);
        }

        /// <summary>
        /// Set focus on element.
        /// </summary>
        public void SetFocus()
        {
            LogElementAction("loc.focusing");
            ExecuteScript(JavaScript.SetFocus);
        }

        /// <summary>
        /// Setting attribute value.
        /// </summary>
        /// <param name="name">Attribute name</param>
        /// <param name="value">Value to set</param>
        public void SetAttribute(string name, string value)
        {
            LogElementAction("loc.el.attr.set", name, value);
            ExecuteScript(JavaScript.SetAttribute, name, value);
        }

        /// <summary>
        /// Checks whether element on screen or not.
        /// </summary>
        /// <returns>True if element is on screen and false otherwise.</returns>
        public bool IsElementOnScreen()
        {
            LogElementAction("loc.is.present.js");
            var value = ExecuteScript<bool>(JavaScript.ElementIsOnScreen);
            LogElementAction("loc.is.present.value", value);
            return value;
        }

        /// <summary>
        /// Get text from element.
        /// </summary>
        /// <returns>Text from element</returns>
        public string GetElementText()
        {
            LogElementAction("loc.get.text.js");
            var value = ExecuteScript<string>(JavaScript.GetElementText);
            LogElementAction("loc.text.value", value);
            return value;
        }

        /// <summary>
        /// Hover mouse over element.
        /// </summary>
        public void HoverMouse()
        {
            LogElementAction("loc.hover.js");
            ExecuteScript(JavaScript.MouseHover);
        }

        /// <summary>
        /// Get element's XPath.
        /// </summary>
        /// <returns>String representation of element's XPath locator.</returns>
        public string GetXPath()
        {
            LogElementAction("loc.get.xpath.js");
            var value = ExecuteScript<string>(JavaScript.GetElementXPath);
            LogElementAction("loc.xpath.value", value);
            return value;
        }

        /// <summary>
        /// Gets element coordinates relative to the View Port.
        /// </summary>
        /// <returns>Point object.</returns>
        public Point GetViewPortCoordinates()
        {
            var coordinates = ExecuteScript<IList<object>>(JavaScript.GetViewPortCoordinates)
                .Select(item => double.Parse(item.ToString()))
                .ToArray();
            return new Point((int)Math.Round(coordinates[0]), (int)Math.Round(coordinates[1]));
        }

        /// <summary>
        /// Executed pinned script against element.
        /// </summary>
        /// <param name="pinnedScript">Instance of script pinned with <see cref="Browser.JavaScriptEngine"/>.</param>
        /// <param name="arguments">Script arguments.</param>
        /// <typeparam name="T">Type of return value.</typeparam>
        /// <returns>Script execution result.</returns>
        public T ExecuteScript<T>(PinnedScript pinnedScript, params object[] arguments)
        {
            LogElementAction("loc.el.execute.pinnedjs");
            var result = ActionRetrier.DoWithRetry(() => pinnedScript.ExecuteScript<T>(ResolveArguments(arguments)));
            LogElementAction("loc.el.execute.pinnedjs.result", result);
            return result;
        }

        /// <summary>
        /// Executed pinned script against element.
        /// </summary>
        /// <param name="pinnedScript">Instance of script pinned with <see cref="Browser.JavaScriptEngine"/>.</param>
        /// <param name="arguments">Script arguments.</param>
        /// <returns>Script execution result.</returns>
        public void ExecuteScript(PinnedScript pinnedScript, params object[] arguments)
        {
            LogElementAction("loc.el.execute.pinnedjs");
            ActionRetrier.DoWithRetry(() => pinnedScript.ExecuteScript(ResolveArguments(arguments)));
        }

        protected T ExecuteScript<T>(JavaScript scriptName, params object[] arguments)
        {
            return ActionRetrier.DoWithRetry(() => Browser.ExecuteScript<T>(scriptName, ResolveArguments(arguments)));
        }

        protected void ExecuteScript(JavaScript scriptName, params object[] arguments)
        {
            ActionRetrier.DoWithRetry(() => Browser.ExecuteScript(scriptName, ResolveArguments(arguments)));
        }

        protected internal void LogElementAction(string messageKey, params object[] args)
        {
            Logger.InfoElementAction(elementType, element.Name, messageKey, args);
        }

        private object[] ResolveArguments(params object[] arguments)
        {
            var args = new ArrayList { element.GetElement() };
            args.AddRange(arguments);
            return args.ToArray();
        }
    }
}
