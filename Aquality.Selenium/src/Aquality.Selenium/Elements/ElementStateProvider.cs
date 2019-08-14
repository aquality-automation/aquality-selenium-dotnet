using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Localization;
using Aquality.Selenium.Waitings;
using OpenQA.Selenium;
using System;
using System.Linq;

namespace Aquality.Selenium.Elements
{
    internal class ElementStateProvider : IElementStateProvider
    {
        private readonly By elementLocator;

        internal ElementStateProvider(By elementLocator)
        {
            this.elementLocator = elementLocator;
        }

        public bool IsDisplayed => WaitForDisplayed(TimeSpan.Zero);

        public bool IsExist => WaitForExist(TimeSpan.Zero);        

        public bool IsEnabled => WaitForEnabled(TimeSpan.Zero);

        public bool IsClickable => IsElementClickable(TimeSpan.Zero);

        public bool WaitForDisplayed(TimeSpan? timeout = null)
        {
            return IsAnyElementFound(timeout, ElementState.Displayed);
        }

        public bool WaitForNotDisplayed(TimeSpan? timeout = null)
        {
            return ConditionalWait.WaitFor(() => !IsDisplayed, timeout);
        }

        public bool WaitForExist(TimeSpan? timeout = null)
        {
            return IsAnyElementFound(timeout, ElementState.ExistsInAnyState);
        }

        public bool WaitForNotExist(TimeSpan? timeout = null)
        {
            return ConditionalWait.WaitFor(() => !IsExist, timeout);
        }

        public bool WaitForEnabled(TimeSpan? timeout = null)
        {
            return IsElementInDesiredState(element => IsElementEnabled(element), "ENABLED", timeout);
        }

        public bool WaitForNotEnabled(TimeSpan? timeout = null)
        {
            return IsElementInDesiredState(element => !IsElementEnabled(element), "NOT ENABLED", timeout);
        }

        bool IsElementEnabled(IWebElement element)
        {
            return element.Enabled && !element.GetAttribute(Attributes.Class).Contains(PopularClassNames.Disabled);
        }

        private bool IsElementInDesiredState(Func<IWebElement, bool> elementStateCondition, string state, TimeSpan? timeout)
        {
            var errorMessage = GetErrorMessage(state);
            var desiredState = new DesiredState(elementStateCondition, errorMessage)
            {
                IsCatchingTimeoutException = true,
                IsThrowingNoSuchElementException = true
            };
            return IsElementInDesiredCondition(timeout, desiredState);
        }

        public void WaitForClickable(TimeSpan? timeout = null)
        {
            if (!IsElementClickable(timeout))
            {
                throw new WebDriverTimeoutException("element is not clickable after wait");
            }
        }

        private bool IsElementClickable(TimeSpan? timeout = null)
        {
            var errorMessage = GetErrorMessage("CLICKABLE");
            var desiredState = new DesiredState(element => element.Displayed && element.Enabled, errorMessage)
            {
                IsCatchingTimeoutException = true
            };
            return IsElementInDesiredCondition(timeout, desiredState);
        }

        private bool IsAnyElementFound(TimeSpan? timeout, ElementState state)
        {
            return ElementFinder.Instance.FindElements(elementLocator, state, timeout).Any();
        }

        private bool IsElementInDesiredCondition(TimeSpan? timeout, DesiredState elementStateCondition)
        {
            return ElementFinder.Instance.FindElements(elementLocator, elementStateCondition, timeout).Any();
        }

        private string GetErrorMessage(string expectedState)
        {
            return LocalizationManager.Instance.GetLocalizedMessage("loc.no.elements.found.in.state", elementLocator.ToString(), expectedState);
        }
    }
}
