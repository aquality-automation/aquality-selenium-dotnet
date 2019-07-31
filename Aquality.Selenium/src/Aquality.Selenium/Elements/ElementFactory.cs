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
