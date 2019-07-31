using Aquality.Selenium.Elements.Actions;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace Aquality.Selenium.Elements
{
    public abstract class Element : IElement
    {
        protected Element(By locator, string name, ElementState state)
        {
            Locator = locator;
            Name = name;
        }

        public By Locator { get; }

        public string Name { get; }

        protected abstract string ElementType { get; }

        public JsActions JsActions => new JsActions(this, ElementType);

        public MouseActions MouseActions => new MouseActions(this);

        public IElementStateProvider State => new ElementStateProvider(Locator);

        public bool IsEnabled(TimeSpan? timeout = null)
        {
            throw new NotImplementedException();
        }

        public RemoteWebElement GetElement(TimeSpan? timeout = null)
        {
            throw new NotImplementedException();
        }

        public void Click()
        {
            throw new NotImplementedException();
        }

        public void ClickAndWait()
        {
            throw new NotImplementedException();
        }

        public void RightClick()
        {
            throw new NotImplementedException();
        }

        public void WaitAndClick()
        {
            throw new NotImplementedException();
        }

        public void Focus()
        {
            throw new NotImplementedException();
        }

        public string GetAttribute(string attr, HighlightState highlightState = HighlightState.NotHighlight, TimeSpan? timeout = null)
        {
            throw new NotImplementedException();
        }

        public string GetText(HighlightState highlightState = HighlightState.NotHighlight)
        {
            throw new NotImplementedException();
        }

        public void SendKeys(string key)
        {
            throw new NotImplementedException();
        }

        public void SetInnerHtml(string value)
        {
            throw new NotImplementedException();
        }

        public void WaitForElementIsClickable(TimeSpan? timeout = null)
        {
            throw new NotImplementedException();
        }

        public T FindChildElement<T>(By childLocator, ElementState state = ElementState.Displayed) where T : IElement
        {
            throw new NotImplementedException();
        }

        public T FindChildElement<T>(By childLocator, ElementSupplier<T> supplier, ElementState state = ElementState.Displayed) where T : IElement
        {
            throw new NotImplementedException();
        }

        public bool HasState(PopularClassNames className)
        {
            throw new NotImplementedException();
        }
    }
}
