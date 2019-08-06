using System;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using SeleniumActions = OpenQA.Selenium.Interactions.Actions;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Logging;
using Aquality.Selenium.Waitings;
using Aquality.Selenium.Localization;

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

        private Logger Logger => Logger.Instance;

        private JsActions JsActions => new JsActions(element, elementType);

        /// <summary>
        /// Performs click on element.
        /// </summary>
        public void Click()
        {
            Logger.Info(LocalizationManager.Instance.GetLocalizedMessage("loc.clicking"));
            JsActions.HighlightElement();
            ConditionalWait.WaitFor(driver => PerformAction(element => MoveToElement(driver, element).Click(element)));
        }

        /// <summary>
        /// Performs double click on element.
        /// </summary>
        public void DoubleClick()
        {
            Logger.Info(LocalizationManager.Instance.GetLocalizedMessage("loc.clicking.double"));
            ConditionalWait.WaitFor(driver => PerformAction(element => MoveToElement(driver, element).DoubleClick(element)));
        }

        /// <summary>
        /// Perfroms right click on element.
        /// </summary>
        public void RightClick()
        {
            Logger.Info(LocalizationManager.Instance.GetLocalizedMessage("loc.clicking.right"));
            ConditionalWait.WaitFor(driver => PerformAction(element => MoveToElement(driver, element).ContextClick(element)));
        }

        /// <summary>
        /// Moves mouse to the element.
        /// </summary>
        public void MoveMouseToElement()
        {
            Logger.Info(LocalizationManager.Instance.GetLocalizedMessage("loc.moving"));
            JsActions.ScrollIntoView(); // TODO: check on Safari
            ConditionalWait.WaitFor(driver => PerformAction(element => MoveToElement(driver, element)));
        }

        private SeleniumActions MoveToElement(IWebDriver driver, IWebElement element)
        {
            return new SeleniumActions(driver).MoveToElement(element);
        }

        private bool PerformAction(Func<RemoteWebElement, SeleniumActions> action)
        {
            try
            {
                action(element.GetElement()).Build().Perform();
                return true;
            }
            catch (WebDriverException ex)
            {
                Logger.Debug(ex.Message);
                return false;
            }
        }
    }
}
