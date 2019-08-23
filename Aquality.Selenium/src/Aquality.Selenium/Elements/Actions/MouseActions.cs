using System;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using SeleniumActions = OpenQA.Selenium.Interactions.Actions;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Logging;
using Aquality.Selenium.Utilities;
using Aquality.Selenium.Browsers;

namespace Aquality.Selenium.Elements.Actions
{
    /// <summary>
    /// Allows to perform actions on elements via Selenium Actions class.
    /// </summary>
    public class MouseActions
    {
        private readonly IElement element;
        private readonly string elementType;

        public MouseActions(IElement element, string elementType)
        {
            this.element = element;
            this.elementType = elementType;
        }

        private JsActions JsActions => new JsActions(element, elementType);

        /// <summary>
        /// Performs click on element.
        /// </summary>
        public void Click()
        {
            LogElementAction("loc.clicking");
            JsActions.HighlightElement();
            ElementActionRetrier.DoWithRetry(() => PerformAction(element => MoveToElement(element).Click(element)));
        }

        /// <summary>
        /// Performs double click on element.
        /// </summary>
        public void DoubleClick()
        {
            LogElementAction("loc.clicking.double");
            ElementActionRetrier.DoWithRetry(() => PerformAction(element => MoveToElement(element).DoubleClick(element)));
        }

        /// <summary>
        /// Perfroms right click on element.
        /// </summary>
        public void RightClick()
        {
            LogElementAction("loc.clicking.right");
            ElementActionRetrier.DoWithRetry(() => PerformAction(element => MoveToElement(element).ContextClick(element)));
        }

        /// <summary>
        /// Moves mouse to the element.
        /// </summary>
        public void MoveToElement()
        {
            LogElementAction("loc.moving");
            JsActions.ScrollIntoView(); // TODO: check on Safari
            ElementActionRetrier.DoWithRetry(() => PerformAction(element => MoveToElement(element)));
        }

        private SeleniumActions MoveToElement(IWebElement element)
        {
            return new SeleniumActions(BrowserManager.Browser.Driver).MoveToElement(element);
        }

        private void PerformAction(Func<RemoteWebElement, SeleniumActions> action)
        {
            action(element.GetElement()).Build().Perform();
        }

        protected internal void LogElementAction(string messageKey, params object[] args)
        {
            Logger.Instance.InfoLocElementAction(elementType, element.Name, messageKey, args);
        }
    }
}
