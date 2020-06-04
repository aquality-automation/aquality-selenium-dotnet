using System;
using System.Collections.Generic;
using System.Drawing;
using OpenQA.Selenium;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Core.Logging;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using IElementStateProvider = Aquality.Selenium.Core.Elements.Interfaces.IElementStateProvider;

namespace Aquality.Selenium.Forms
{
    /// <summary>
    /// Defines base class for any UI form.
    /// </summary>
    public abstract class Form
    {
        private readonly ILabel formLabel;
        /// <summary>
        /// Constructor with parameters.
        /// </summary>
        /// <param name="locator">Unique locator of the form.</param>
        /// <param name="name">Name of the form.</param>
        protected Form(By locator, string name)
        {
            Locator = locator;
            Name = name;
            formLabel = ElementFactory.GetLabel(Locator, Name);
        }

        /// <summary>
        /// Gets Form element defined by its locator and name.
        /// Could be used to find child elements relative to form element.
        /// </summary>
        protected IElement FormElement => formLabel;

        /// <summary>
        /// Instance of logger <see cref="Core.Logging.Logger"/>
        /// </summary>
        protected static Logger Logger => AqualityServices.Logger;

        /// <summary>
        /// Element factory <see cref="IElementFactory"/>
        /// </summary>
        protected static IElementFactory ElementFactory => AqualityServices.Get<IElementFactory>();

        /// <summary>
        /// Locator of the form.
        /// </summary>
        public By Locator { get; }

        /// <summary>
        /// Name of the form.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Return form state for form locator
        /// </summary>
        /// <value>True - form is opened,
        /// False - form is not opened.</value>
        [Obsolete("This property will be removed in the future release. Use State.WaitForDisplayed() if needed")] 
        public bool IsDisplayed => FormElement.State.WaitForDisplayed();

        /// <summary>
        /// Provides ability to get form's state (whether it is displayed, exists or not) and respective waiting functions.
        /// </summary>
        public IElementStateProvider State => FormElement.State;

        /// <summary>
        /// Gets size of form element defined by its locator.
        /// </summary>
        public Size Size => FormElement.GetElement().Size;

        /// <summary>
        /// Scroll form without scrolling entire page
        /// </summary>
        /// <param name="x">horizontal coordinate</param>
        /// <param name="y">vertical coordinate</param>
        public void ScrollBy(int x, int y)
        {
            FormElement.JsActions.ScrollBy(x, y);
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
        [Obsolete("This method will be removed in the future release. Use FormElement property methods to find child element")]
        protected T FindChildElement<T>(By childLocator, string name = null, ElementSupplier<T> supplier = null, ElementState state = ElementState.Displayed) 
            where T : IElement
        {
            return FormElement.FindChildElement(childLocator, name, supplier, state);
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
        [Obsolete("This method will be removed in the future release. Use FormElement property methods to find child elements")]
        protected IList<T> FindChildElements<T>(By childLocator, string name = null, ElementSupplier<T> supplier = null, ElementsCount expectedCount = ElementsCount.Any, ElementState state = ElementState.Displayed) 
            where T : IElement
        {
            return FormElement.FindChildElements(childLocator, name, supplier, expectedCount, state);
        }
    }
}
