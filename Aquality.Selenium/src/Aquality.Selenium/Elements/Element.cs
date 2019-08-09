﻿using System;
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

        public string GetAttribute(string attr, HighlightState highlightState = HighlightState.Default, TimeSpan? timeout = null)
        {
            Logger.InfoLoc("loc.el.getattr", attr);
            JsActions.HighlightElement(highlightState);
            return ConditionalWait.WaitFor(driver => GetElement(timeout).GetAttribute(attr), timeout);
        }

        public string GetCssValue(string propertyName, HighlightState highlightState = HighlightState.Default, TimeSpan? timeout = null)
        {
            Logger.InfoLoc("loc.el.cssvalue", propertyName);
            JsActions.HighlightElement(highlightState);
            return ConditionalWait.WaitFor(driver => GetElement(timeout).GetCssValue(propertyName), timeout);
        }

        public string GetText(HighlightState highlightState = HighlightState.Default)
        {
            Logger.InfoLoc("loc.get.text");
            JsActions.HighlightElement(highlightState);
            return ConditionalWait.WaitFor(driver => GetElement().Text);
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
    }
}
