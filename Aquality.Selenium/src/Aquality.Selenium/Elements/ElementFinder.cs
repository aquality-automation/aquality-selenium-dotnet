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
        private static readonly ThreadLocal<ElementFinder> instanceHolder = new ThreadLocal<ElementFinder>();

        public static ElementFinder Instance
        {
            get
            {
                if (!instanceHolder.IsValueCreated)
                {
                    instanceHolder.Value = new ElementFinder();
                }

                return instanceHolder.Value;
            }
        }

        private ElementFinder()
        {
        }

        public IWebElement FindElement(By locator, TimeSpan? timeout = null, ElementState state = ElementState.ExistsInAnyState)
        {
            var clearTimeout = timeout ?? DefaultTimeout;
            var elements = FindElements(locator, clearTimeout, state);
            if (elements.Any())
            {
                return elements.First();
            }

            throw new NoSuchElementException($"Element was not found in {clearTimeout.Seconds} seconds in state {state} by locator {locator}");
        }

        public IWebElement FindElement(By by)
        {
            return FindElement(by, timeout: null);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By locator, TimeSpan? timeout = null, ElementState state = ElementState.ExistsInAnyState)
        {
            var resultElements = new List<IWebElement>();
            var zeroTimeout = TimeSpan.FromSeconds(0);
            Browser.ImplicitWaitTimeout = zeroTimeout;
            ConditionalWait.WaitForTrue(driver =>
            {
                var foundElements = driver.FindElements(locator);
                var filteredElements = FilterByState(foundElements, state);
                resultElements.AddRange(filteredElements);
                return filteredElements.Any();
            }, timeout ?? DefaultTimeout);
            Browser.ImplicitWaitTimeout = TimeoutConfiguration.Implicit;
            return resultElements.ToList().AsReadOnly();
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return FindElements(by, timeout: null);
        }

        private IList<IWebElement> FilterByState(IList<IWebElement> foundElements, ElementState state)
        {
            var filteredElements = new List<IWebElement>();
            if (foundElements.Any())
            {
                switch (state)
                {
                    case ElementState.Displayed:
                        filteredElements.AddRange(foundElements.Where(element => element.Displayed));
                        break;
                    case ElementState.ExistsInAnyState:
                        filteredElements.AddRange(foundElements);
                        break;
                    default:
                        throw new InvalidOperationException($"{state} state is not recognized");
                }
            }

            return filteredElements;
        }

        private ITimeoutConfiguration TimeoutConfiguration => Configuration.Instance.TimeoutConfiguration;

        private TimeSpan DefaultTimeout => TimeoutConfiguration.Condition;

        private Browser Browser => BrowserManager.Browser;
    }
}
