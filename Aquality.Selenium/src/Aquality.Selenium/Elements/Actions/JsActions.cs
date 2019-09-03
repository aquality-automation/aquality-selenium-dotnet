﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Logging;

namespace Aquality.Selenium.Elements.Actions
{
    /// <summary>
    /// Allows to perform actions on elements via JavaScript.
    /// </summary>
    public class JsActions
    {
        private readonly IElement element;
        private readonly string elementType;

        public JsActions(IElement element, string elementType)
        {
            this.element = element;
            this.elementType = elementType;
        }

        private IConfiguration Configuration => Configurations.Configuration.Instance;

        private Browser Browser => BrowserManager.Browser;

        protected Logger Logger => Logger.Instance;

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
            if (Configuration.BrowserProfile.IsElementHighlightEnabled || highlightState.Equals(HighlightState.Highlight))
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
        /// Element have to contains inner scroll bar.
        /// </summary>
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
            var coordinates = ExecuteScript<IList<object>>(JavaScript.GetViewPortCoordinates).Select(item => double.Parse(item.ToString())).ToArray();
            return new Point((int)Math.Round(coordinates[0]), (int)Math.Round(coordinates[1]));
        }

        protected T ExecuteScript<T>(JavaScript scriptName, params object[] arguments)
        {
            return Browser.ExecuteScript<T>(scriptName, element.GetElement(), arguments);
        }

        protected void ExecuteScript(JavaScript scriptName, params object[] arguments)
        {
            Browser.ExecuteScript(scriptName, element.GetElement(), arguments);
        }

        protected internal void LogElementAction(string messageKey, params object[] args)
        {
            Logger.InfoLocElementAction(elementType, element.Name, messageKey, args);
        }
    }
}
