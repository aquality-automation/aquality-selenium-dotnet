using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Localization;
using Aquality.Selenium.Logging;
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

        private Logger Logger => Logger.Instance;

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

        public IWebElement FindElement(By locator, ElementState state = ElementState.ExistsInAnyState, TimeSpan? timeout = null)
        {
            var elementStateCondition = ResolveState(state);
            return FindElement(locator, elementStateCondition, timeout);
        }

        public IWebElement FindElement(By locator, Func<IWebElement, bool> elementStateCondition, TimeSpan? timeout = null)
        {
            var desiredState = new DesiredState(elementStateCondition, "desired")
            {
                IsCatchingTimeoutException = false,
                IsThrowingNoSuchElementException = true
            };
            return FindElements(locator, desiredState, timeout).First();
        }

        public ReadOnlyCollection<IWebElement> FindElements(By locator, ElementState state = ElementState.ExistsInAnyState, TimeSpan? timeout = null)
        {
            var elementStateCondition = ResolveState(state);
            return FindElements(locator, elementStateCondition, timeout);
        }

        public ReadOnlyCollection<IWebElement> FindElements(By locator, Func<IWebElement, bool> elementStateCondition, TimeSpan? timeout = null)
        {
            var desiredState = new DesiredState(elementStateCondition, "desired")
            {
                IsCatchingTimeoutException = true
            };
            return FindElements(locator, desiredState, timeout);
        }

        internal ReadOnlyCollection<IWebElement> FindElements(By locator, DesiredState desiredState, TimeSpan? timeout = null)
        {
            var foundElements = new List<IWebElement>();
            var resultElements = new List<IWebElement>();
            try
            {
                ConditionalWait.WaitFor(driver =>
                {
                    foundElements = driver.FindElements(locator).ToList();
                    resultElements = foundElements.Where(desiredState.ElementStateCondition).ToList();
                    return resultElements.Any();
                }, timeout);
            }
            catch (WebDriverTimeoutException ex)
            {
                HandleTimeoutException(ex, desiredState, locator, foundElements);
            }
            return resultElements.AsReadOnly();
        }

        private void HandleTimeoutException(WebDriverTimeoutException ex, DesiredState desiredState, By locator, List<IWebElement> foundElements)
        {
            var message = LocalizationManager.Instance.GetLocalizedMessage("loc.no.elements.found.in.state", locator.ToString(), desiredState.StateName);
            if (desiredState.IsCatchingTimeoutException)
            {
                if (!foundElements.Any())
                {
                    if (desiredState.IsThrowingNoSuchElementException)
                    {
                        throw new NoSuchElementException(message);
                    }
                    Logger.Debug(message);
                }
                else
                {
                    Logger.DebugLoc("loc.elements.were.found.but.not.in.state", null, locator.ToString(), desiredState.StateName);
                }
            }
            else
            {
                var combinedMessage = $"{ex.Message}: {message}";
                if (desiredState.IsThrowingNoSuchElementException && !foundElements.Any())
                {
                    throw new NoSuchElementException(combinedMessage);                    
                }
                throw new WebDriverTimeoutException(combinedMessage);
            }
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
