﻿using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Elements
{
    /// <summary>
    /// Defines TextBox UI element.
    /// </summary>
    public class TextBox : Element, ITextBox
    {
        private const string SecretMask = "*********";

        protected internal TextBox(By locator, string name, ElementState state) : base(locator, name, state)
        {
        }

        protected override string ElementType => LocalizationManager.GetLocalizedMessage("loc.text.field");

        public string Value => GetAttribute(Attributes.Value);

        public void Clear()
        {
            LogElementAction("loc.text.clearing");
            JsActions.HighlightElement();
            DoWithRetry(() => GetElement().Clear());
        }

        public void Type(string value, bool secret = false)
        {
            LogElementAction("loc.text.typing", secret ? SecretMask : value);
            JsActions.HighlightElement();
            DoWithRetry(() => GetElement().SendKeys(value));
        }

        public void ClearAndType(string value, bool secret = false)
        {
            LogElementAction("loc.text.clearing");
            LogElementAction("loc.text.typing", secret ? SecretMask : value);
            JsActions.HighlightElement();
            DoWithRetry(() =>
            {
                GetElement().Clear();
                GetElement().SendKeys(value);                
            });
        }

        public void Submit()
        {
            LogElementAction("loc.text.submitting");
            DoWithRetry(() => GetElement().Submit());
        }

        public new void Focus()
        {
            LogElementAction("loc.focusing");
            DoWithRetry(() => GetElement().SendKeys(string.Empty));
        }
    }
}
