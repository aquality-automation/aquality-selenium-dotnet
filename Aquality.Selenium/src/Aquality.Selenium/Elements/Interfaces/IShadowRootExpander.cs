using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Core.Localization;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Aquality.Selenium.Elements.Interfaces
{
    /// <summary>
    /// Shadow Root expander.
    /// </summary>
    public interface IShadowRootExpander
    {
        /// <summary>
        /// Expands shadow root.
        /// </summary>
        /// <returns>ShadowRoot search context.</returns>
        ShadowRoot ExpandShadowRoot();
    }

    /// <summary>
    /// Extensions for Shadow Root expander (like element or JS Actions).
    /// </summary>
    public static class ShadowRootExpanderExtensions
    {
        /// <summary>
        /// Provides <see cref="IElementFactory"/> to find elements in the shadow root of the current element.
        /// </summary>
        public static IElementFactory GetShadowRootElementFactory(this IShadowRootExpander shadowRootExpander)
        {
            var shadowRootRelativeFinder = new RelativeElementFinder(AqualityServices.LocalizedLogger, AqualityServices.ConditionalWait, shadowRootExpander.ExpandShadowRoot);
            return new ElementFactory(AqualityServices.ConditionalWait, shadowRootRelativeFinder, AqualityServices.Get<ILocalizationManager>());
        }

        /// <summary>
        /// Finds element in the shadow root of the current element.
        /// </summary>
        /// <typeparam name="T">Type of the target element that has to implement <see cref="IElement"/>.</typeparam>
        /// <param name="shadowRootExpander">Current instance of the Shadow root expander.</param>
        /// <param name="locator">Locator of the target element. 
        /// Note that some browsers don't support XPath locator for shadow elements (e.g. Chrome).</param>
        /// <param name="name">Name of the target element.</param>
        /// <param name="supplier">Delegate that defines constructor of element.</param>
        /// <param name="state">State of the target element.</param>
        /// <returns>Instance of element.</returns>
        public static T FindElementInShadowRoot<T>(this IShadowRootExpander shadowRootExpander, By locator, string name, ElementSupplier<T> supplier = null, ElementState state = ElementState.Displayed)
            where T : IElement
        {
            return shadowRootExpander.GetShadowRootElementFactory().Get(locator, name, supplier, state);
        }

        /// <summary>
        /// Finds elements in the shadow root of the current element.
        /// </summary>
        /// <typeparam name="T">Type of the target elements that has to implement <see cref="IElement"/>.</typeparam>
        /// <param name="shadowRootExpander">Current instance of the Shadow root expander.</param>
        /// <param name="locator">Locator of target elements. 
        /// Note that some browsers don't support XPath locator for shadow elements.
        /// Therefore, we suggest to use CSS selectors</param>
        /// <param name="name">Name of target elements.</param>
        /// <param name="supplier">Delegate that defines constructor of element.</param>
        /// <param name="expectedCount">Expected number of elements that have to be found (zero, more then zero, any).</param>
        /// <param name="state">State of target elements.</param>
        /// <returns>List of found elements.</returns>
        public static IList<T> FindElementsInShadowRoot<T>(this IShadowRootExpander shadowRootExpander, By locator, string name = null, ElementSupplier<T> supplier = null, ElementsCount expectedCount = ElementsCount.Any, ElementState state = ElementState.Displayed)
            where T : IElement
        {
            return shadowRootExpander.GetShadowRootElementFactory().FindElements(locator, name, supplier, expectedCount, state);
        }
    }
}
