using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Utilities;
using Aquality.Selenium.Elements.Interfaces;

namespace Aquality.Selenium.Elements.Actions
{
    /// <summary>
    /// Allows to perform actions on elements via JavaScript.
    /// </summary>
    public class JsActions
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
        /// Perfroms click on element and waits for page is loaded.
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
        /// Default value is from configuration: <see cref="IBrowserProfile.IsElementHighlightEnabled">
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
        /// Checks whether element on screen or not.
        /// </summary>
        /// <returns>True if element is on screen and false otherwise.</returns>
        public bool IsElementOnScreen()
        {
            LogElementAction("loc.is.present.js");
            return ExecuteScript<bool>(JavaScript.ElementIsOnScreen);
        }

        /// <summary>
        /// Get text from element.
        /// </summary>
        /// <returns>Text from element</returns>
        public string GetElementText()
        {
            LogElementAction("loc.get.text.js");
            return ExecuteScript<string>(JavaScript.GetElementText);
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
            return ExecuteScript<string>(JavaScript.GetElementXPath);
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
