using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;

namespace Aquality.Selenium.Elements.Interfaces
{
    /// <summary>
    /// Provides ability to find elements in desired ElementState
    /// </summary>
    public interface IElementFinder : ISearchContext
    {
        /// <summary>
        /// Finds elements in desired ElementState
        /// </summary>
        /// <param name="locator">elements locator</param>
        /// <param name="timeout">timeout for search</param>
        /// <param name="state">desired ElementState</param>
        /// <returns>List of found elements</returns>
        ReadOnlyCollection<IWebElement> FindElements(By locator, TimeSpan? timeout = null, ElementState state = ElementState.ExistsInAnyState);

        /// <summary>
        /// Finds element in desired ElementState
        /// </summary>
        /// <param name="locator">elements locator</param>
        /// <param name="timeout">timeout for search</param>
        /// <param name="state">desired ElementState</param>
        /// <exception cref="OpenQA.Selenium.NoSuchElementException">Thrown if element was not found in time in desired state</exception> 
        /// <returns>Found element</returns>
        IWebElement FindElement(By locator, TimeSpan? timeout = null, ElementState state = ElementState.ExistsInAnyState);
    }
}
