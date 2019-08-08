using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Actions;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Logging;
using Aquality.Selenium.Waitings;

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
            State.WaitForClickable();            
            Click();
        }

        public void Click()
        {
            Logger.InfoLoc("loc.clicking");
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
            Logger.InfoLoc("loc.el.getattr", attr);
            HighlightElement(highlightState);
            return ConditionalWait.WaitFor(driver => GetElement(timeout).GetAttribute(attr), timeout);
        }

        public string GetText(HighlightState highlightState = HighlightState.NotHighlight)
        {
            Logger.InfoLoc("loc.get.text");
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
            Logger.InfoLoc("loc.send.text", value);
            Browser.ExecuteScript(JavaScript.SetInnerHTML, GetElement(), value);
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
