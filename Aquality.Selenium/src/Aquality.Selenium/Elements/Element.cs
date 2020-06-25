﻿using OpenQA.Selenium;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Actions;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Core.Utilities;
using Aquality.Selenium.Core.Applications;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Waitings;
using CoreElement = Aquality.Selenium.Core.Elements.Element;
using ICoreElementFactory = Aquality.Selenium.Core.Elements.Interfaces.IElementFactory;
using ICoreElementFinder = Aquality.Selenium.Core.Elements.Interfaces.IElementFinder;
using ICoreElementStateProvider = Aquality.Selenium.Core.Elements.Interfaces.IElementStateProvider;
using Aquality.Selenium.Core.Configurations;
using System.Reflection;
using System.Linq;

namespace Aquality.Selenium.Elements
{
    /// <summary>
    /// Defines base class for any UI element.
    /// </summary>
    public abstract class Element : CoreElement, IElement
    {
        protected Element(By locator, string name, ElementState state) : base(locator, name, state)
        {
        }

        public override ICoreElementStateProvider State => new ElementStateProvider(Locator, ConditionalWait, Finder, LogElementState);

        protected IBrowserProfile BrowserProfile => AqualityServices.Get<IBrowserProfile>();

        public JsActions JsActions => new JsActions(this, ElementType, LocalizedLogger, BrowserProfile);

        public MouseActions MouseActions => new MouseActions(this, ElementType, LocalizedLogger, ActionRetrier);

        private Browser Browser => (Browser)Application;

        protected override IApplication Application => AqualityServices.Browser;

        protected override IElementActionRetrier ActionRetrier => AqualityServices.Get<IElementActionRetrier>();

        protected override ICoreElementFactory Factory => CustomFactory;

        protected virtual IElementFactory CustomFactory => AqualityServices.Get<IElementFactory>();

        protected override ICoreElementFinder Finder => AqualityServices.Get<ICoreElementFinder>();

        protected override IElementCacheConfiguration CacheConfiguration => AqualityServices.Get<IElementCacheConfiguration>();

        protected override ILocalizedLogger LocalizedLogger => AqualityServices.LocalizedLogger;

        protected override ILocalizationManager LocalizationManager => AqualityServices.Get<ILocalizationManager>();

        protected override IConditionalWait ConditionalWait => AqualityServices.ConditionalWait;

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

        public new void Click()
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
            var value = DoWithRetry(() => GetElement().GetAttribute(attr));
            LogElementAction("loc.el.attr.value", attr, value);
            return value;
        }

        public string GetCssValue(string propertyName, HighlightState highlightState = HighlightState.Default)
        {
            LogElementAction("loc.el.cssvalue", propertyName);
            JsActions.HighlightElement(highlightState);
            var value = DoWithRetry(() => GetElement().GetCssValue(propertyName));
            LogElementAction("loc.el.attr.value", propertyName, value);
            return value;
        }

        public string GetText(HighlightState highlightState = HighlightState.Default)
        {
            LogElementAction("loc.get.text");
            JsActions.HighlightElement(highlightState);
            var value = DoWithRetry(() => GetElement().Text);
            LogElementAction("loc.text.value", value);
            return value;
        }
        
        public void SetInnerHtml(string value)
        {
            Click();
            LogElementAction("loc.send.text", value);
            Browser.ExecuteScript(JavaScript.SetInnerHTML, GetElement(), value);
        }

        public void SendKey(Key key)
        {
            LogElementAction("loc.text.sending.key", key);
            var keysString = typeof(Keys)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .FirstOrDefault(field => field.Name == key.ToString())?.GetValue(null).ToString();
            DoWithRetry(() => GetElement().SendKeys(keysString));
        }
    }
}
