using Aquality.Selenium.Elements;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System.Drawing;
using Aquality.Selenium.Logging;

namespace Aquality.Selenium.Forms
{
    /// <summary>
    /// Defines base class for any UI form.
    /// </summary>
    public abstract class Form
    {
        /// <summary>
        /// Instance of logger <see cref="Aquality.Selenium.Logging.Logger">
        /// </summary>
        /// <value>Logger instance.</value>
        protected Logger Logger => Logger.Instance;

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
        /// <value>Element factory.</value>
        protected IElementFactory ElementFactory => new ElementFactory();

        private ILabel FormLabel => ElementFactory.GetLabel(Locator, Name);

        /// <summary>
        /// Get form size
        /// </summary>
        /// <value>Size.</value>
        public Size Size => FormLabel.GetElement().Size;

        /// <summary>
        /// Return form state for form locator
        /// </summary>
        /// <value>True - form is opened,
        /// False - form is not opened.</value>
        public bool IsDisplayed => FormLabel.State.WaitForDisplayed();

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="locator">Unique locator of the form.</param>
        /// <param name="name">Name of the form.</param>
        protected Form(By locator, string name)
        {
            Locator = locator;
            Name = name;
        }

        /// <summary>
        /// Scroll form without scrolling entire page
        /// </summary>
        /// <param name="x">horizontal coordinate</param>
        /// <param name="y">vertical coordinate</param>
        public void ScrollBy(int x, int y)
        {
            FormLabel.JsActions.ScrollBy(x, y);
        }
    }
}
