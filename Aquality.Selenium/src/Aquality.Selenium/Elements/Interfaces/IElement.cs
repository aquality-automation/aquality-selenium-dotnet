using Aquality.Selenium.Elements.ElementActions;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;

namespace Aquality.Selenium.Elements.Interfaces
{
    public interface IElement : IParent, IElementWithState
    {
        JsActions JsActions { get; }

        MouseActions MouseActions { get; }

        By Locator { get; }

        string Name { get; }

        RemoteWebElement GetElement(TimeSpan? timeout = null);

        bool IsEnabled(TimeSpan? timeout = null);

        string GetText(HighlightState highlightState = HighlightState.NotHighlight);

        string GetAttribute(string attr, HighlightState highlightState = HighlightState.NotHighlight, TimeSpan? timeout = null);
       
        void SendKeys(string key);        

        void WaitForElementIsClickable(TimeSpan? timeout = null);

        void Click();

        void RightClick();

        void ClickAndWait();

        void WaitAndClick();

        void Focus();

        void SetInnerHtml(string value);
    }
}
