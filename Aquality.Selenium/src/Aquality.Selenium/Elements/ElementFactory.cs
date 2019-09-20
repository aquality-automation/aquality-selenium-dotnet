using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Core.Elements.Interfaces;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Waitings;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using CoreFactory = Aquality.Selenium.Core.Elements.ElementFactory;
using IElement = Aquality.Selenium.Core.Elements.Interfaces.IElement;
using IElementFactory = Aquality.Selenium.Elements.Interfaces.IElementFactory;


namespace Aquality.Selenium.Elements
{
    /// <summary>
    /// Factory that creates elements.
    /// </summary>
    public class ElementFactory : CoreFactory, IElementFactory
    {
        private static readonly string ByXpathIdentifier = "By.XPath";        

        private Browser Browser => BrowserManager.Browser;

        public ElementFactory(ConditionalWait conditionalWait, IElementFinder elementFinder, LocalizationManager localizationManager) 
            : base(conditionalWait, elementFinder, localizationManager)
        {
        }

        public IButton GetButton(By locator, string name, ElementState state = ElementState.Displayed)
        {
            return new Button(locator, name, state);
        }

        public ICheckBox GetCheckBox(By locator, string name, ElementState state = ElementState.Displayed)
        {
            return new CheckBox(locator, name, state);
        }

        public IComboBox GetComboBox(By locator, string name, ElementState state = ElementState.Displayed)
        {
            return new ComboBox(locator, name, state);
        }

        public ILabel GetLabel(By locator, string name, ElementState state = ElementState.Displayed)
        {
            return new Label(locator, name, state);
        }

        public ILink GetLink(By locator, string name, ElementState state = ElementState.Displayed)
        {
            return new Link(locator, name, state);
        }

        public IRadioButton GetRadioButton(By locator, string name, ElementState state = ElementState.Displayed)
        {
            return new RadioButton(locator, name, state);
        }

        public ITextBox GetTextBox(By locator, string name, ElementState state = ElementState.Displayed)
        {
            return new TextBox(locator, name, state);
        }

        public T GetCustomElement<T>(ElementSupplier<T> elementSupplier, By locator, string name, ElementState state = ElementState.Displayed) where T : IElement
        {
            return elementSupplier(locator, name, state);
        }

        public T FindChildElement<T>(IElement parentElement, By childLocator, ElementSupplier<T> supplier = null, ElementState state = ElementState.Displayed) where T : IElement
        {
            var elementSupplier = ResolveSupplier(supplier);
            return elementSupplier(new ByChained(parentElement.Locator, childLocator), $"Child element of {parentElement.Name}", state);
        }

        public IList<T> FindElements<T>(By locator, ElementSupplier<T> supplier = null, ElementsCount expectedCount = ElementsCount.MoreThenZero, ElementState state = ElementState.Displayed) where T : IElement
        {
            var elementSupplier = ResolveSupplier(supplier);
            switch (expectedCount)
            {
                case ElementsCount.Zero:
                    ConditionalWait.WaitFor(driver => !driver.FindElements(locator).Any());
                    break;
                case ElementsCount.MoreThenZero:
                    ConditionalWait.WaitFor(driver => driver.FindElements(locator).Any());
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"No such expected value: {expectedCount}");
            }
            
            var webElements = ElementFinder.Instance.FindElements(locator, state, TimeSpan.Zero);
            IEnumerable<T> elements = webElements.Select((webElement, index) =>
            {
                var elementIndex = index + 1;
                return elementSupplier(GenerateXpathLocator(locator, webElement, elementIndex), $"element {elementIndex}", state);
            });
            return elements.ToList();
        }

        /// <summary>
        /// Gets map between elements interfaces and their implementations.
        /// Can be extended for custom elements with custom interfaces.
        /// </summary>
        /// <returns>Dictionary where key is interface and value is its implementation.</returns>
        protected virtual IDictionary<Type, Type> GetElementTypesMap()
        {
            return new Dictionary<Type, Type>
            {
                { typeof(IButton), typeof(Button) },
                { typeof(ICheckBox), typeof(CheckBox) },
                { typeof(IComboBox), typeof(ComboBox) },
                { typeof(ILabel), typeof(Label) },
                { typeof(ILink), typeof(Link) },
                { typeof(IRadioButton), typeof(RadioButton) },
                { typeof(ITextBox), typeof(TextBox) }
            };
        }
    }
}
