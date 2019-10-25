using OpenQA.Selenium;
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
using CoreElementFactory = Aquality.Selenium.Core.Elements.Interfaces.IElementFactory;
using CoreElementFinder = Aquality.Selenium.Core.Elements.Interfaces.IElementFinder;
using CoreElementStateProvider = Aquality.Selenium.Core.Elements.Interfaces.IElementStateProvider;

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

        public override CoreElementStateProvider State => new ElementStateProvider(Locator, ConditionalWait, Finder);

        protected IBrowserProfile BrowserProfile => BrowserManager.GetRequiredService<IBrowserProfile>();

        public JsActions JsActions => new JsActions(this, ElementType, LocalizedLogger, BrowserProfile);

        public MouseActions MouseActions => new MouseActions(this, ElementType, LocalizedLogger, ActionRetrier);

        private Browser Browser => (Browser)Application;

        protected override IApplication Application => BrowserManager.Browser;

        protected override ElementActionRetrier ActionRetrier => BrowserManager.GetRequiredService<ElementActionRetrier>();

        protected override CoreElementFactory Factory => CustomFactory;

        protected virtual IElementFactory CustomFactory => BrowserManager.GetRequiredService<IElementFactory>();

        protected override CoreElementFinder Finder => BrowserManager.GetRequiredService<CoreElementFinder>();

        protected override ILocalizedLogger LocalizedLogger => BrowserManager.GetRequiredService<ILocalizedLogger>();

        protected ILocalizationManager LocalizationManager => BrowserManager.GetRequiredService<ILocalizationManager>();

        protected override ConditionalWait ConditionalWait => BrowserManager.GetRequiredService<ConditionalWait>();

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
        
        public void SetInnerHtml(string value)
        {
            Click();
            LogElementAction("loc.send.text", value);
            Browser.ExecuteScript(JavaScript.SetInnerHTML, GetElement(), value);
        }
    }
}
