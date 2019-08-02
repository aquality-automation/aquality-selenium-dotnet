using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Waitings;
using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
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
        
        public bool WaitForDisplayed(TimeSpan? timeout = null)
        {
            return FindElements(timeout, ElementState.Displayed).Any();
        }

        public bool WaitForExist(TimeSpan? timeout = null)
        {
            return FindElements(timeout, ElementState.ExistsInAnyState).Any();
        }

        public bool WaitForNotDisplayed(TimeSpan? timeout = null)
        {
            return ConditionalWait.WaitForTrue(driver => !IsDisplayed, timeout);
        }

        public bool WaitForNotExist(TimeSpan? timeout = null)
        {
            return ConditionalWait.WaitForTrue(driver => !IsExist, timeout);
        }

        private ReadOnlyCollection<IWebElement> FindElements(TimeSpan? timeout, ElementState state)
        {
            return ElementFinder.Instance.FindElements(elementLocator, state, timeout);
        }
    }
}
