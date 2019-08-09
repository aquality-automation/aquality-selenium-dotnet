using Aquality.Selenium.Elements.Interfaces;
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

        public bool IsClickable => WaitForIsClickable(TimeSpan.Zero);

        public bool IsEnabled => WaitForEnabled(TimeSpan.Zero);

        public void WaitForClickable(TimeSpan? timeout = null)
        {
            if (!WaitForIsClickable(timeout))
            {
                throw new WebDriverTimeoutException("element is not clickable after wait");
            }
        }

        private bool WaitForIsClickable(TimeSpan? timeout = null)
        {
            return IsElementInDesiredCondition(timeout, element => element.Displayed && element.Enabled);
        }

        public bool WaitForDisplayed(TimeSpan? timeout = null)
        {
            return IsAnyElementFound(timeout, ElementState.Displayed);
        }

        public bool WaitForNotDisplayed(TimeSpan? timeout = null)
        {
            return ConditionalWait.WaitForTrue(driver => !IsDisplayed, timeout);
        }

        public bool WaitForEnabled(TimeSpan? timeout = null)
        {
            bool isElementEnabled(IWebElement element) => element.Enabled && !element.GetAttribute(Attributes.Class).Contains(PopularClassNames.Disabled);
            return IsElementInDesiredCondition(timeout, isElementEnabled);
        }

        public bool WaitForNotEnabled(TimeSpan? timeout = null)
        {
            ElementFinder.Instance.FindElement(elementLocator, timeout: timeout);
            return ConditionalWait.WaitForTrue(driver => !IsEnabled, timeout);
        }

        public bool WaitForExist(TimeSpan? timeout = null)
        {
            return IsAnyElementFound(timeout, ElementState.ExistsInAnyState);
        }

        public bool WaitForNotExist(TimeSpan? timeout = null)
        {
            return ConditionalWait.WaitForTrue(driver => !IsExist, timeout);
        }

        private bool IsAnyElementFound(TimeSpan? timeout, ElementState state)
        {
            return ElementFinder.Instance.FindElements(elementLocator, state, timeout).Any();
        }

        private bool IsElementInDesiredCondition(TimeSpan? timeout, Func<IWebElement, bool> elementStateCondition)
        {
            ElementFinder.Instance.FindElement(elementLocator, timeout: timeout);
            return ElementFinder.Instance.FindElements(elementLocator, elementStateCondition, timeout).Any();
        }
    }
}
