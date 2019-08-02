using OpenQA.Selenium;
using SeleniumActions = OpenQA.Selenium.Interactions.Actions;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Logging;
using Aquality.Selenium.Waitings;

namespace Aquality.Selenium.Elements.Actions
{
    /// <summary>
    /// Allows to perfrom actions on elements via Selenium Actions class.
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
            Logger.InfoLoc("loc.clicking");
            JsActions.HighlightElement();
            ConditionalWait.WaitFor(driver => PerformAction(new SeleniumActions(driver).Click(element.GetElement())));
        }

        /// <summary>
        /// Performs double click on element.
        /// </summary>
        public void DoubleClick()
        {
            Logger.InfoLoc("loc.clicking.double");
            ConditionalWait.WaitFor(driver => {
                var currentElement = element.GetElement();
                return PerformAction(new SeleniumActions(driver).MoveToElement(currentElement).DoubleClick(currentElement));
            });
        }

        /// <summary>
        /// Perfroms right click on element.
        /// </summary>
        public void RightClick()
        {
            Logger.InfoLoc("loc.clicking.right");
            ConditionalWait.WaitFor(driver =>
            {
                var currentElement = element.GetElement();
                return PerformAction(new SeleniumActions(driver).MoveToElement(currentElement).ContextClick(currentElement));
            });
        }

        /// <summary>
        /// Moves mouse to the element.
        /// </summary>
        public void MoveMouseToElement()
        {
            Logger.InfoLoc("loc.moving");
            JsActions.ScrollIntoView(); // TODO: check on Safari
            ConditionalWait.WaitFor(driver => PerformAction(new SeleniumActions(driver).MoveToElement(element.GetElement())));
        }

        private bool PerformAction(SeleniumActions actions)
        {
            try
            {
                actions.Build().Perform();
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
