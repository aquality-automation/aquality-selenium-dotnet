using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Waitings;
using OpenQA.Selenium;

namespace Aquality.Selenium.Elements
{
    internal sealed class ElementFinder : IElementFinder
    {
        private static readonly ThreadLocal<ElementFinder> InstanceHolder = new ThreadLocal<ElementFinder>();

        private ElementFinder()
        {
        }

        public static ElementFinder Instance
        {
            get
            {
                if (!InstanceHolder.IsValueCreated)
                {
                    InstanceHolder.Value = new ElementFinder();
                }
                return InstanceHolder.Value;
            }
        }

        private ITimeoutConfiguration TimeoutConfiguration => Configuration.Instance.TimeoutConfiguration;

        private Browser Browser => BrowserManager.Browser;

        public IWebElement FindElement(By locator, ElementState state = ElementState.ExistsInAnyState, TimeSpan? timeout = null)
        {
            var elementStateCondition = ResolveState(state);
            return FindElement(locator, elementStateCondition, timeout);
        }

        public IWebElement FindElement(By locator, Func<IWebElement, bool> elementStateCondition, TimeSpan? timeout = null)
        {
            var clearTimeout = timeout ?? TimeoutConfiguration.Condition;
            var elements = FindElements(locator, elementStateCondition, clearTimeout);
            if (elements.Any())
            {
                return elements.First();
            }
            throw new NoSuchElementException($"Element was not found in desired state in {clearTimeout.Seconds} seconds by locator {locator}");
        }

        public ReadOnlyCollection<IWebElement> FindElements(By locator, ElementState state = ElementState.ExistsInAnyState, TimeSpan? timeout = null)
        {
            var elementStateCondition = ResolveState(state);
            return FindElements(locator, elementStateCondition, timeout);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By locator, Func<IWebElement, bool> elementStateCondition, TimeSpan? timeout = null)
        {
            Browser.ImplicitWaitTimeout = TimeSpan.Zero;
            var resultElements = new List<IWebElement>();
            ConditionalWait.WaitForTrue(driver =>
            {
                var elements = BrowserManager.Browser.Driver.FindElements(locator).Where(elementStateCondition);
                resultElements.AddRange(elements);
                return elements.Any();
            }, timeout);
            Browser.ImplicitWaitTimeout = TimeoutConfiguration.Implicit;
            return resultElements.AsReadOnly();
        }

        private Func<IWebElement, bool> ResolveState(ElementState state)
        {
            Func<IWebElement, bool> elementStateCondition;
            switch (state)
            {
                case ElementState.Displayed:
                    elementStateCondition = element => element.Displayed;
                    break;
                case ElementState.ExistsInAnyState:
                    elementStateCondition = element => true;
                    break;
                default:
                    throw new InvalidOperationException($"{state} state is not recognized");
            }
            return elementStateCondition;
        }
    }
}
