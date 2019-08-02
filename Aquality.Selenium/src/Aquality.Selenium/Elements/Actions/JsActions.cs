using System;
using System.Drawing;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Logging;

namespace Aquality.Selenium.Elements.Actions
{
    /// <summary>
    /// Allows to perfrom actions on elements via JavaScript.
    /// </summary>
    public class JsActions
    {
        private readonly IConfiguration Configuration = Configurations.Configuration.Instance;
        private readonly Logger logger = Logger.Instance;

        private readonly IElement element;
        private readonly string elementType;

        public JsActions(IElement element, string elementType)
        {
            this.element = element;
            this.elementType = elementType;
        }

        private Browser Browser => BrowserManager.Browser;

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
            logger.InfoLoc("loc.clicking.js");
            HighlightElement();
            ExecuteScript(JavaScript.ClickElement);
        }

        /// <summary>
        /// Highlights the element.
        /// </summary>
        public void HighlightElement()
        {
            if (Configuration.BrowserProfile.IsElementHighlightEnabled)
            {
                ExecuteScript(JavaScript.BorderElement);
            }
        }

        /// <summary>
        /// Scrolling page to the element.
        /// </summary>
        public void ScrollIntoView()
        {
            logger.InfoLoc("loc.scrolling.js");
            ExecuteScript(JavaScript.ScrollToElement, true);
        }

        /// <summary>
        /// Scrolling element by coordinates.
        /// </summary>
        /// <param name="x">Horizontal coordinate</param>
        /// <param name="y">Verticale coordinate</param>
        public void ScrollBy(int x, int y)
        {
            logger.InfoLoc("loc.scrolling.js");
            ExecuteScript(JavaScript.ScrollBy, x, y);
        }

        /// <summary>
        /// Scrolling to the center of element.
        /// </summary>
        public void ScrollToTheCenter()
        {
            logger.InfoLoc("loc.scrolling.center.js");
            ExecuteScript(JavaScript.ScrollToElementCenter);
        }

        /// <summary>
        /// Setting value.
        /// </summary>
        /// <param name="value">Value to set</param>
        public void SetValue(string value)
        {
            logger.InfoLoc("loc.setting.value");
            ExecuteScript(JavaScript.SetValue, value);
        }

        /// <summary>
        /// Set focus on element.
        /// </summary>
        public void SetFocus()
        {
            logger.InfoLoc("loc.focusing");
            ExecuteScript(JavaScript.SetFocus);
        }

        /// <summary>
        /// Checks whether element on screen or not.
        /// </summary>
        /// <returns>True if element is on screen and false otherwise.</returns>
        public bool IsElementOnScreen()
        {
            logger.InfoLoc("loc.is.present.js");
            return ExecuteScript<bool>(JavaScript.ElementIsOnScreen);
        }

        /// <summary>
        /// Get text from element.
        /// </summary>
        /// <returns>Text from element</returns>
        public string GetElementText()
        {
            logger.InfoLoc("loc.get.text.js");
            return ExecuteScript<string>(JavaScript.GetElementText);
        }

        /// <summary>
        /// Hover mouse over element.
        /// </summary>
        public void HoverMouse()
        {
            logger.InfoLoc("loc.hover.js");
            ExecuteScript(JavaScript.MouseHover);
        }

        /// <summary>
        /// Get element's XPath.
        /// </summary>
        /// <returns>String representation of element's XPath locator.</returns>
        public string GetXPath()
        {
            logger.InfoLoc("loc.get.xpath.js");
            return ExecuteScript<string>(JavaScript.GetElementXPath);
        }

        /// <summary>
        /// Gets element coordinates relative to the View Port.
        /// </summary>
        /// <returns>Point object.</returns>
        public Point GetViewPortCoordinates()
        {
            var coordinates = ExecuteScript<double []>(JavaScript.GetViewPortCoordinates);
            return new Point((int)Math.Round(coordinates[0]), (int)Math.Round(coordinates[1]));
        }

        private T ExecuteScript<T>(JavaScript scriptName, params object[] arguments)
        {
            return Browser.ExecuteScript<T>(scriptName, element.GetElement(), arguments);
        }

        private void ExecuteScript(JavaScript scriptName, params object[] arguments)
        {
            Browser.ExecuteScript(scriptName, element.GetElement(), arguments);
        }
    }
}
