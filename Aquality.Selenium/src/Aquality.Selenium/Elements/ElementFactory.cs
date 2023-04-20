using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Core.Elements.Interfaces;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Waitings;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using CoreFactory = Aquality.Selenium.Core.Elements.ElementFactory;
using IElementFactory = Aquality.Selenium.Elements.Interfaces.IElementFactory;


namespace Aquality.Selenium.Elements
{
    /// <summary>
    /// Factory that creates elements.
    /// </summary>
    public class ElementFactory : CoreFactory, IElementFactory
    {
        private static readonly IDictionary<string, string> LocatorToXPathTemplateMap = new Dictionary<string, string>
        {
            { "By.ClassName", "//*[contains(@class,'{0}')]" },
            { "By.Name", "//*[@name='{0}']" },
            { "By.Id", "//*[@id='{0}']" }
        };

        public ElementFactory(IConditionalWait conditionalWait, IElementFinder elementFinder, ILocalizationManager localizationManager)
            : base(conditionalWait, elementFinder, localizationManager)
        {
        }

        public IButton GetButton(By locator, string name, ElementState state = ElementState.Displayed)
        {
            return ResolveSupplier<IButton>()(locator, name, state);
        }

        public ICheckBox GetCheckBox(By locator, string name, ElementState state = ElementState.Displayed)
        {
            return ResolveSupplier<ICheckBox>()(locator, name, state);
        }

        public IComboBox GetComboBox(By locator, string name, ElementState state = ElementState.Displayed)
        {
            return ResolveSupplier<IComboBox>()(locator, name, state);
        }

        public IMultiChoiceBox GetMultiChoiceBox(By locator, string name, ElementState state = ElementState.Displayed)
        {
            return ResolveSupplier<IMultiChoiceBox>()(locator, name, state);
        }

        public ILabel GetLabel(By locator, string name, ElementState state = ElementState.Displayed)
        {
            return ResolveSupplier<ILabel>()(locator, name, state);
        }

        public ILink GetLink(By locator, string name, ElementState state = ElementState.Displayed)
        {
            return ResolveSupplier<ILink>()(locator, name, state);
        }

        public IRadioButton GetRadioButton(By locator, string name, ElementState state = ElementState.Displayed)
        {
            return ResolveSupplier<IRadioButton>()(locator, name, state);
        }

        public ITextBox GetTextBox(By locator, string name, ElementState state = ElementState.Displayed)
        {
            return ResolveSupplier<ITextBox>()(locator, name, state);
        }

        public T Get<T>(By locator, string name, ElementSupplier<T> supplier = null, ElementState state = ElementState.Displayed) where T : Interfaces.IElement
        {
            return ResolveSupplier(supplier)(locator, name, state);
        }

        private ElementSupplier<T> ResolveSupplier<T>() where T : Interfaces.IElement
        {
            return ResolveSupplier<T>(null);
        }

        protected override ElementSupplier<T> ResolveSupplier<T>(ElementSupplier<T> supplier)
        {
            var baseSupplier = base.ResolveSupplier(supplier);
            return (loc, name, state) =>
            {
                var element = baseSupplier.Invoke(loc, name, state);
                if (element is Element baseElement)
                {
                    baseElement.CustomFinder = ElementFinder;
                }                
                return element;
            };
        }

        /// <summary>
        /// Gets map between elements interfaces and their implementations.
        /// Can be extended for custom elements with custom interfaces.
        /// </summary>
        /// <returns>Dictionary where key is interface and value is its implementation.</returns>
        protected override IDictionary<Type, Type> ElementTypesMap
        {
            get
            {
                return new Dictionary<Type, Type>
                {
                    { typeof(IButton), typeof(Button) },
                    { typeof(ICheckBox), typeof(CheckBox) },
                    { typeof(IComboBox), typeof(ComboBox) },
                    { typeof(IMultiChoiceBox), typeof(MultiChoiceBox) },
                    { typeof(ILabel), typeof(Label) },
                    { typeof(ILink), typeof(Link) },
                    { typeof(IRadioButton), typeof(RadioButton) },
                    { typeof(ITextBox), typeof(TextBox) }
                };
            }
        }

        /// <summary>
        /// Generates xpath locator for target element
        /// </summary>
        /// <param name="baseLocator">locator of parent element</param>
        /// <param name="webElement">target element</param>
        /// <param name="elementIndex">index of target element</param>
        /// <returns>target element's locator</returns>
        protected override By GenerateXpathLocator(By baseLocator, IWebElement webElement, int elementIndex)
        {
            return IsLocatorSupportedForXPathExtraction(baseLocator)
                ? base.GenerateXpathLocator(baseLocator, webElement, elementIndex)
                : By.XPath(ConditionalWait.WaitFor(driver => driver.ExecuteJavaScript<string>(
                    JavaScript.GetElementXPath.GetScript(), webElement), message: "XPath generation failed"));
        }

        /// <summary>
        /// Defines is the locator can be transformed to xpath or not.
        /// Current implementation works only with ByXPath.class and ByTagName locator types,
        /// but you can implement your own for the specific WebDriver type.
        /// </summary>
        /// <param name="locator">locator to transform</param>
        /// <returns>true if the locator can be transformed to xpath, false otherwise.</returns>
        protected override bool IsLocatorSupportedForXPathExtraction(By locator)
        {
            return LocatorToXPathTemplateMap.Keys.Any(locType => locator.ToString().StartsWith(locType))
                || base.IsLocatorSupportedForXPathExtraction(locator);
        }
        
        /// <summary>
        /// Extracts XPath from passed locator.
        /// Current implementation works only with ByXPath.class and ByTagName locator types,
        /// but you can implement your own for the specific WebDriver type.
        /// </summary>
        /// <param name="locator">locator to get xpath from.</param>
        /// <returns>extracted XPath.</returns>
        protected override string ExtractXPathFromLocator(By locator)
        {
            var locatorString = locator.ToString();
            var supportedLocatorType = LocatorToXPathTemplateMap.Keys.FirstOrDefault(locType => locatorString.StartsWith(locType));
            return supportedLocatorType == null
                ? base.ExtractXPathFromLocator(locator)
                : string.Format(LocatorToXPathTemplateMap[supportedLocatorType], locatorString.Substring(locatorString.IndexOf(':') + 1).Trim());
        }
    }
}
