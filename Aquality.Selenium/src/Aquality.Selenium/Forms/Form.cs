using Aquality.Selenium.Elements;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Localization;
using Aquality.Selenium.Logging;
using OpenQA.Selenium;
using System;
using System.Drawing;
using System.Linq;

namespace Aquality.Selenium.Forms
{
    public abstract class Form
    {
        private readonly Logger logger = Logger.Instance;

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
        /// Constructor
        /// </summary>
        protected Form()
        {
            var type = GetType().GetCustomAttributes(false).OfType<PageInfoAttribute>().FirstOrDefault();
            Name = type.PageName;
            Locator = GetLocatorFromPageInfo(type);
            ElementFactory = new ElementFactory();
        }

        /// <summary>
        /// Constructor with parameters
        /// </summary>
        /// <param name="locator">Unique locator of the page.</param>
        /// <param name="name">Name of the page.</param>
        protected Form(By locator, string name)
        {
            Locator = locator;
            Name = name;
            ElementFactory = new ElementFactory();
        }

        protected By GetLocatorFromPageInfo(PageInfoAttribute pageInfo)
        {
            if (!string.IsNullOrWhiteSpace(pageInfo.Xpath))
            {
                return By.XPath(pageInfo.Xpath);
            }
            else if (!string.IsNullOrWhiteSpace(pageInfo.Id))
            {
                return By.Id(pageInfo.Id);
            }
            else if (!string.IsNullOrWhiteSpace(pageInfo.Css))
            {
                return By.CssSelector(pageInfo.Css);
            }
            var message = string.Format(LocalizationManager.Instance.GetLocalizedMessage("loc.baseform.unknown.type"), Name);
            var exception = new ArgumentException(message);
            logger.FatalLoc(message, exception);
            throw exception;
        }

        /// <summary>
        /// Return form state for form locator
        /// </summary>
        /// <returns>True - form is opened,
        /// False - form is not opened</returns>
        public bool IsFormDisplayed()
        {
            return GetPageLabel().State.WaitForDisplayed();
        }

        /// <summary>
        /// Scroll form without scrolling entire page
        /// </summary>
        /// <param name="x">horizontal coordinate</param>
        /// <param name="y">vertical coordinate</param>
        public void ScrollBy(int x, int y)
        {
            GetPageLabel().JsActions.ScrollBy(x, y);
        }

        /// <summary>
        /// Get form size
        /// </summary>
        /// <returns>size</returns>
        public Size GetFormSize()
        {
            return GetPageLabel().GetElement().Size;
        }

        private ILabel GetPageLabel()
        {
            return ElementFactory.GetLabel(Locator, Name);
        }
    }
}
