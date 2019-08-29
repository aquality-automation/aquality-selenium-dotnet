using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Actions;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Logging;
using Aquality.Selenium.Utilities;

namespace Aquality.Selenium.Elements
{
    /// <summary>
    /// Defines base class for any UI element.
    /// </summary>
    public abstract class Element : IElement
    {
        private readonly ElementState elementState;

        protected Element(By locator, string name, ElementState state)
        {
            Locator = locator;
            Name = name;
            elementState = state;
        }

        public By Locator { get; }

        public string Name { get; }

        protected abstract string ElementType { get; }

        public JsActions JsActions => new JsActions(this, ElementType);

        public MouseActions MouseActions => new MouseActions(this, ElementType);

        public IElementStateProvider State => new ElementStateProvider(Locator);

        protected Logger Logger => Logger.Instance;

        private Browser Browser => BrowserManager.Browser;

        private IElementFinder Finder => ElementFinder.Instance;

        private IElementFactory ElementFactory => new ElementFactory();

        public RemoteWebElement GetElement(TimeSpan? timeout = null)
        {
            try
            {
                return (RemoteWebElement)Finder.FindElement(Locator, elementState, timeout);
            }
            catch (NoSuchElementException ex)
            {
                Logger.Debug($"Page source:{Environment.NewLine}{Browser.Driver.PageSource}", ex);
                throw;
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
            LogElementAction("loc.clicking");
            JsActions.HighlightElement();
            DoWithRetry(() => GetElement().Click());
        }

        public void Focus()
        {
            LogElementAction("loc.focusing");
            JsActions.SetFocus();
        }

        public string GetAttribute(string attr, HighlightState highlightState = HighlightState.Default)
        {
            LogElementAction("loc.el.getattr", attr);
            JsActions.HighlightElement(highlightState);
            return DoWithRetry(() => GetElement().GetAttribute(attr));
        }

        public string GetCssValue(string propertyName, HighlightState highlightState = HighlightState.Default)
        {
            LogElementAction("loc.el.cssvalue", propertyName);
            JsActions.HighlightElement(highlightState);
            return DoWithRetry(() => GetElement().GetCssValue(propertyName));
        }

        public string GetText(HighlightState highlightState = HighlightState.Default)
        {
            LogElementAction("loc.get.text");
            JsActions.HighlightElement(highlightState);
            return DoWithRetry(() => GetElement().Text);
        }

        public void SendKeys(string key)
        {
            LogElementAction("loc.text.sending.keys", key);
            DoWithRetry(() => GetElement().SendKeys(key));
        }

        public void SetInnerHtml(string value)
        {
            Click();
            LogElementAction("loc.send.text", value);
            Browser.ExecuteScript(JavaScript.SetInnerHTML, GetElement(), value);
        }
        
        public T FindChildElement<T>(By childLocator, ElementSupplier<T> supplier = null, ElementState state = ElementState.Displayed) where T : IElement
        {
            return ElementFactory.FindChildElement(this, childLocator, supplier, state);
        }

        protected void DoWithRetry(Action action)
        {
            ElementActionRetrier.DoWithRetry(action);
        }

        protected T DoWithRetry<T>(Func<T> function)
        {
            return ElementActionRetrier.DoWithRetry(function);
        }

        protected internal void LogElementAction(string messageKey, params object[] args)
        {
            Logger.InfoLocElementAction(ElementType, Name, messageKey, args);
        }
    }
}
