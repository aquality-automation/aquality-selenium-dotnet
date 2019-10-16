using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System.Drawing;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Localization;

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

        /// <summary>
        /// Locator of specified form.
        /// </summary>
        public By Locator { get; }

        /// <summary>
        /// Name of specified form.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Instance of logger <see cref="Logging.Logger">
        /// </summary>
        /// <value>Logger instance.</value>
        protected LocalizationLogger Logger => BrowserManager.GetRequiredService<LocalizationLogger>();

        /// <summary>
        /// Element factory <see cref="IElementFactory">
        /// </summary>
        /// <value>Element factory.</value>
        protected IElementFactory ElementFactory => BrowserManager.GetRequiredService<IElementFactory>();

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

        private ILabel FormLabel => ElementFactory.GetLabel(Locator, Name);

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
