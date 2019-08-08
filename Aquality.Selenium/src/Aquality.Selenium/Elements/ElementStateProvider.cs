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

        public bool IsClickable => WaitForClickable(TimeSpan.Zero);

        public bool IsEnabled => WaitForEnabled(TimeSpan.Zero);

        public bool WaitForClickable(TimeSpan? timeout = null)
        {
            return AreAnyElementsFound(timeout, element => element.Displayed && element.Enabled);
        }

        public bool WaitForDisplayed(TimeSpan? timeout = null)
        {
            return AreAnyElementsFound(timeout, ElementState.Displayed);
        }

        public bool WaitForEnabled(TimeSpan? timeout = null)
        {
            bool isElementEnabled(IWebElement element) => element.Enabled && !element.GetAttribute(Attributes.Class).Contains(PopularClassNames.Disabled);
            return AreAnyElementsFound(timeout, isElementEnabled);
        }

        public bool WaitForExist(TimeSpan? timeout = null)
        {
            return AreAnyElementsFound(timeout, ElementState.ExistsInAnyState);
        }

        public bool WaitForNotDisplayed(TimeSpan? timeout = null)
        {
            return ConditionalWait.WaitForTrue(driver => !IsDisplayed, timeout);
        }

        public bool WaitForNotExist(TimeSpan? timeout = null)
        {
            return ConditionalWait.WaitForTrue(driver => !IsExist, timeout);
        }

        private bool AreAnyElementsFound(TimeSpan? timeout, ElementState state)
        {
            return ElementFinder.Instance.FindElements(elementLocator, state, timeout).Any();
        }

        private bool AreAnyElementsFound(TimeSpan? timeout, Func<IWebElement, bool> elementStateCondition)
        {
            return ElementFinder.Instance.FindElements(elementLocator, elementStateCondition, timeout).Any();
        }
    }
}
