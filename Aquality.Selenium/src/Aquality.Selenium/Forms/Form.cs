using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System.Drawing;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Elements;
using System.Collections.Generic;

namespace Aquality.Selenium.Forms
{
    /// <summary>
    /// Defines base class for any UI form.
    /// </summary>
    public abstract class Form
    {
        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="locator">Unique locator of the form.</param>
        /// <param name="name">Name of the form.</param>
        protected Form(By locator, string name)
        {
            Locator = locator;
            Name = name;
        }

        private ILabel FormLabel => ElementFactory.GetLabel(Locator, Name);

        /// <summary>
        /// Instance of logger <see cref="Logging.Logger">
        /// </summary>
        /// <value>Logger instance.</value>
        protected ILocalizedLogger Logger => AqualityServices.LocalizedLogger;

        /// <summary>
        /// Element factory <see cref="IElementFactory">
        /// </summary>
        /// <value>Element factory.</value>
        protected IElementFactory ElementFactory => AqualityServices.Get<IElementFactory>();

        /// <summary>
        /// Locator of specified form.
        /// </summary>
        public By Locator { get; }

        /// <summary>
        /// Name of specified form.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Return form state for form locator
        /// </summary>
        /// <value>True - form is opened,
        /// False - form is not opened.</value>
        public bool IsDisplayed => FormLabel.State.WaitForDisplayed();

        /// <summary>
        /// Gets size of form element defined by its locator.
        /// </summary>
        public Size Size => FormLabel.GetElement().Size;

        /// <summary>
        /// Scroll form without scrolling entire page
        /// </summary>
        /// <param name="x">horizontal coordinate</param>
        /// <param name="y">vertical coordinate</param>
        public void ScrollBy(int x, int y)
        {
            FormLabel.JsActions.ScrollBy(x, y);
        }

        /// <summary>
        /// Finds child element of current form by its locator.
        /// </summary>
        /// <typeparam name="T">Type of child element that has to implement IElement.</typeparam>
        /// <param name="childLocator">Locator of child element relative to form.</param>
        /// <param name="name">Child element name.</param>
        /// <param name="supplier">Delegate that defines constructor of child element in case of custom element.</param>
        /// <param name="state">Child element state.</param>
        /// <returns>Instance of child element.</returns>
        protected T FindChildElement<T>(By childLocator, string name = null, ElementSupplier<T> supplier = null, ElementState state = ElementState.Displayed) where T : Core.Elements.Interfaces.IElement
        {
            return FormLabel.FindChildElement(childLocator, name, supplier, state);
        }

        /// <summary>
        /// Finds child elements of current form by their locator.
        /// </summary>
        /// <typeparam name="T">Type of child elements that has to implement IElement.</typeparam>
        /// <param name="childLocator">Locator of child elements relative to form.</param>
        /// <param name="name">Child elements name.</param>
        /// <param name="supplier">Delegate that defines constructor of child element in case of custom element type.</param>
        /// <param name="expectedCount">Expected number of elements that have to be found (zero, more then zero, any).</param>
        /// <param name="state">Child elements state.</param>
        /// <returns>List of child elements.</returns>
        protected IList<T> FindChildElements<T>(By childLocator, string name = null, ElementSupplier<T> supplier = null, ElementsCount expectedCount = ElementsCount.Any, ElementState state = ElementState.Displayed) where T : Core.Elements.Interfaces.IElement
        {
            return FormLabel.FindChildElements(childLocator, name, supplier, expectedCount, state);
        }
    }
}
