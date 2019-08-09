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
            return IsAnyElementFound(timeout, element => element.Displayed && element.Enabled);
        }

        public bool WaitForNotClickable(TimeSpan? timeout = null)
        {
            return ConditionalWait.WaitForTrue(driver => !IsClickable, timeout);
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
            return IsAnyElementFound(timeout, isElementEnabled);
        }

        public bool WaitForNotEnabled(TimeSpan? timeout = null)
        {
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

        private bool IsAnyElementFound(TimeSpan? timeout, Func<IWebElement, bool> elementStateCondition)
        {
            return ElementFinder.Instance.FindElements(elementLocator, elementStateCondition, timeout).Any();
        }
    }
}
