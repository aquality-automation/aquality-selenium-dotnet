using Aquality.Selenium.Elements;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System.Drawing;

namespace Aquality.Selenium.Forms
{
    /// <summary>
    /// Defines base class for any UI form.
    /// </summary>
    public abstract class Form
    {
        /// <summary>
        /// Locator for specified form
        /// </summary>
        protected readonly By Locator;

        /// <summary>
        /// Name of specified form
        /// </summary>
        protected readonly string Name;

        /// <summary>
        /// Element factory <see cref="Aquality.Selenium.Elements.Interfaces.IElementFactory">
        /// </summary>
        protected readonly IElementFactory ElementFactory;
        
        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="locator">Unique locator of the form.</param>
        /// <param name="name">Name of the form.</param>
        protected Form(By locator, string name)
        {
            Locator = locator;
            Name = name;
            ElementFactory = new ElementFactory();
        }

        /// <summary>
        /// Return form state for form locator
        /// </summary>
        /// <returns>True - form is opened,
        /// False - form is not opened</returns>
        public bool IsFormDisplayed()
        {
            return GetFormLabel().State.WaitForDisplayed();
        }

        /// <summary>
        /// Scroll form without scrolling entire page
        /// </summary>
        /// <param name="x">horizontal coordinate</param>
        /// <param name="y">vertical coordinate</param>
        public void ScrollBy(int x, int y)
        {
            GetFormLabel().JsActions.ScrollBy(x, y);
        }

        /// <summary>
        /// Get form size
        /// </summary>
        /// <returns>size</returns>
        public Size GetFormSize()
        {
            return GetFormLabel().GetElement().Size;
        }

        private ILabel GetFormLabel()
        {
            return ElementFactory.GetLabel(Locator, Name);
        }
    }
}
