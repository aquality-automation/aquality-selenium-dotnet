using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Actions;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Logging;
using Aquality.Selenium.Waitings;
using Aquality.Selenium.Localization;

namespace Aquality.Selenium.Elements
{
    /// <summary>
    /// Defines base class for any UI element.
    /// </summary>
    public abstract class Element : IElement
    {      
        protected Element(By locator, string name, ElementState state)
        {
            Locator = locator;
            Name = name;
            ElementState = state;
        }

        public By Locator { get; }

        public string Name { get; }

        public ElementState ElementState { get; }

        protected abstract string ElementType { get; }

        public JsActions JsActions => new JsActions(this, ElementType);

        public MouseActions MouseActions => new MouseActions(this, ElementType);

        public IElementStateProvider State => new ElementStateProvider(Locator);

        private Logger Logger => Logger.Instance;

        private Browser Browser => BrowserManager.Browser;

        private IElementFinder Finder => ElementFinder.Instance;

        private IElementFactory ElementFactory => new ElementFactory();

        public bool IsEnabled(TimeSpan? timeout = null)
        {
            bool isElementEnabled(IWebElement element) => element.Enabled && !element.GetAttribute(Attributes.Class).Contains(PopularClassNames.Disabled);
            return Finder.FindElements(Locator, isElementEnabled, timeout).Count != 0;
        }

        public RemoteWebElement GetElement(TimeSpan? timeout = null)
        {
            try
            {
                return (RemoteWebElement)Finder.FindElement(Locator, ElementState, timeout);
            }
            catch (NoSuchElementException ex)
            {
                Logger.Debug($"Page source:{Environment.NewLine}{Browser.Driver.PageSource}");
                throw ex;
            }
        }

        public void ClickAndWait()
        {
            Click();
            Browser.WaitForPageToLoad();
        }

        public void WaitAndClick()
        {
            WaitForElementIsClickable();            
            Click();
        }

        public void Click()
        {
            Logger.Info(LocalizationManager.Instance.GetLocalizedMessage("loc.clicking"));
            JsActions.HighlightElement();
            ConditionalWait.WaitFor(driver =>
            {
                GetElement().Click();
                return true;
            });
        }

        public void Focus()
        {
            JsActions.SetFocus();
        }

        public string GetAttribute(string attr, HighlightState highlightState = HighlightState.NotHighlight, TimeSpan? timeout = null)
        {
            Logger.Info(LocalizationManager.Instance.GetLocalizedMessage("loc.el.getattr", attr));
            HighlightElement(highlightState);
            return ConditionalWait.WaitFor(driver => GetElement(timeout).GetAttribute(attr), timeout);
        }

        public string GetText(HighlightState highlightState = HighlightState.NotHighlight)
        {
            Logger.Info(LocalizationManager.Instance.GetLocalizedMessage("loc.get.text"));
            HighlightElement(highlightState);
            return ConditionalWait.WaitFor(driver => GetElement().Text);
        }

        private void HighlightElement(HighlightState highlightState)
        {
            if (highlightState.Equals(HighlightState.Highlight))
            {
                JsActions.HighlightElement();
            }
        }

        public void SendKeys(string key)
        {
            ConditionalWait.WaitFor(driver =>
            {
                GetElement().SendKeys(key);
                return true;
            });
        }

        public void SetInnerHtml(string value)
        {
            Click();
            Logger.Info(LocalizationManager.Instance.GetLocalizedMessage("loc.send.text", value));
            Browser.ExecuteScript(JavaScript.SetInnerHTML, GetElement(), value);
        }

        public void WaitForElementIsClickable(TimeSpan? timeout = null)
        {
            Finder.FindElements(Locator, element => element.Displayed && element.Enabled, timeout);
        }
        
        public T FindChildElement<T>(By childLocator, ElementSupplier<T> supplier = null, ElementState state = ElementState.Displayed) where T : IElement
        {
            return ElementFactory.FindChildElement(this, childLocator, supplier, state);
        }

        public bool ContainsClassAttribute(string className)
        {
            return GetAttribute(Attributes.Class).Contains(className.ToLower());
        }
    }
}
