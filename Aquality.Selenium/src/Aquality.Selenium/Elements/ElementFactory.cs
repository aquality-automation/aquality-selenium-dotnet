using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Waitings;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aquality.Selenium.Elements
{
    /// <summary>
    /// Factory that creates elements.
    /// </summary>
    public class ElementFactory : IElementFactory
    {
        private static readonly string ByXpathIdentifier = "By.XPath";

        /// <summary>
        /// Creates an instance of IButton interface basic implementation (Button).
        /// </summary>
        /// <param name="locator">Element locator</param>
        /// <param name="name">Element name</param>
        /// <param name="state">Element state</param>
        /// <returns>Instance of IButton interface basic implementation</returns>
        public IButton GetButton(By locator, string name, ElementState state = ElementState.Displayed)
        {
            return new Button(locator, name, state);
        }

        /// <summary>
        /// Creates an instance of ICheckBox interface basic implementation (CheckBox).
        /// </summary>
        /// <param name="locator">Element locator</param>
        /// <param name="name">Element name</param>
        /// <param name="state">Element state</param>
        /// <returns>Instance of ICheckBox interface basic implementation</returns>
        public ICheckBox GetCheckBox(By locator, string name, ElementState state = ElementState.Displayed)
        {
            return new CheckBox(locator, name, state);
        }

        /// <summary>
        /// Creates an instance of IComboBox interface basic implementation (ComboBox).
        /// </summary>
        /// <param name="locator">Element locator</param>
        /// <param name="name">Element name</param>
        /// <param name="state">Element state</param>
        /// <returns>Instance of IComboBox interface basic implementation</returns>
        public IComboBox GetComboBox(By locator, string name, ElementState state = ElementState.Displayed)
        {
            return new ComboBox(locator, name, state);
        }

        /// <summary>
        /// Creates an instance of ILabel interface basic implementation (Label).
        /// </summary>
        /// <param name="locator">Element locator</param>
        /// <param name="name">Element name</param>
        /// <param name="state">Element state</param>
        /// <returns>Instance of ILabel interface basic implementation</returns>
        public ILabel GetLabel(By locator, string name, ElementState state = ElementState.Displayed)
        {
            return new Label(locator, name, state);
        }

        /// <summary>
        /// Creates an instance of ILink interface basic implementation (Link).
        /// </summary>
        /// <param name="locator">Element locator</param>
        /// <param name="name">Element name</param>
        /// <param name="state">Element state</param>
        /// <returns>Instance of ILink interface basic implementation</returns>
        public ILink GetLink(By locator, string name, ElementState state = ElementState.Displayed)
        {
            return new Link(locator, name, state);
        }

        /// <summary>
        /// Creates an instance of IRadioButton interface basic implementation (RadioButton).
        /// </summary>
        /// <param name="locator">Element locator</param>
        /// <param name="name">Element name</param>
        /// <param name="state">Element state</param>
        /// <returns>Instance of IRadioButton interface basic implementation</returns>
        public IRadioButton GetRadioButton(By locator, string name, ElementState state = ElementState.Displayed)
        {
            return new RadioButton(locator, name, state);
        }

        /// <summary>
        /// Creates an instance of ITextBox interface basic implementation (TextBox).
        /// </summary>
        /// <param name="locator">Element locator</param>
        /// <param name="name">Element name</param>
        /// <param name="state">Element state</param>
        /// <returns>Instance of ITextBox interface basic implementation</returns>
        public ITextBox GetTextBox(By locator, string name, ElementState state = ElementState.Displayed)
        {
            return new TextBox(locator, name, state);
        }

        /// <summary>
        /// Creates an instance of custom element that implements IElement interface.
        /// </summary>
        /// <typeparam name="T">Type of custom element that has to implement IElement</typeparam>
        /// <param name="elementSupplier">Delegate that defines constructor of element</param>
        /// <param name="locator">Element locator</param>
        /// <param name="name">Element name</param>
        /// <param name="state">Element state</param>
        /// <returns>Instance of custom element that implements IElement interface</returns>
        public T GetCustomElement<T>(ElementSupplier<T> elementSupplier, By locator, string name, ElementState state = ElementState.Displayed) where T : IElement
        {
            return elementSupplier(locator, name, state);
        }

        /// <summary>
        /// Finds child element by its locator relative to parent element.
        /// </summary>
        /// <typeparam name="T">Type of child element that has to implement IElement</typeparam>
        /// <param name="parentElement">Parent element</param>
        /// <param name="childLocator">Locator of child element relative to its parent</param>
        /// <param name="supplier">Delegate that defines constructor of element in case of custom element</param>
        /// <param name="state">Child element state</param>
        /// <returns>Instance of child element</returns>
        public T FindChildElement<T>(IElement parentElement, By childLocator, ElementSupplier<T> supplier = null, ElementState state = ElementState.Displayed) where T : IElement
        {
            var elementSupplier = ResolveSupplier(supplier);
            return elementSupplier(new ByChained(parentElement.Locator, childLocator), $"Child element of {parentElement.Name}", state);
        }

        /// <summary>
        /// Finds list of elements by base locator.
        /// </summary>
        /// <typeparam name="T">Type of elements that have to implement IElement</typeparam>
        /// <param name="locator">Base elements locator</param>
        /// <param name="supplier">Delegate that defines constructor of element in case of custom elements</param>
        /// <param name="expectedCount">Expected number of elements that have to be found (zero ot more then zero)</param>
        /// <param name="state">Elements state</param>
        /// <returns>List of elements that found by locator</returns>
        public IList<T> FindElements<T>(By locator, ElementSupplier<T> supplier = null, ElementsCount expectedCount = ElementsCount.MoreThenZero, ElementState state = ElementState.Displayed) where T : IElement
        {
            var elementSupplier = ResolveSupplier(supplier);
            switch (expectedCount)
            {
                case ElementsCount.Zero:
                    break;
                case ElementsCount.MoreThenZero:
                    ConditionalWait.WaitForTrue(d => d.FindElements(locator).Any());
                    break;
                default:
                    throw new ArgumentOutOfRangeException($"No such expected value: {expectedCount}");
            }

            var webElements = GetBrowser().Driver.FindElements(locator);
            IEnumerable<T> elements = webElements.Select((webElement, index) =>
            {
                var elementIndex = index + 1;
                return supplier(GenerateXpathLocator(locator, webElement, elementIndex), $"element {elementIndex}", state);
            });
            return elements.ToList();
        }

        private By GenerateXpathLocator(By baseLocator, IWebElement webElement, int elementIndex)
        {
            var strBaseLocator = baseLocator.ToString();
            var elementLocator = strBaseLocator.Contains(ByXpathIdentifier)
                    ? $"{strBaseLocator.Split(':')[1].Trim()}[{elementIndex}]"
                    : GetBrowser().ExecuteScript<string>(JavaScript.GetElementXpath, webElement);
            return By.XPath(elementLocator);
        }

        private ElementSupplier<T> ResolveSupplier<T>(ElementSupplier<T> supplier) where T : IElement
        {
            return supplier ?? ((locator, name, state) => (T)Activator.CreateInstance(typeof(T), locator, name, state));
        }

        private Browser GetBrowser()
        {
            return BrowserManager.Browser;
        }
    }
}
