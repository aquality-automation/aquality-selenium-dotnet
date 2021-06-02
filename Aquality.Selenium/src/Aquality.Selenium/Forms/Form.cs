using System.Drawing;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Logging;
using Aquality.Selenium.Core.Forms;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using IElementStateProvider = Aquality.Selenium.Core.Elements.Interfaces.IElementStateProvider;
using Aquality.Selenium.Core.Configurations;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Waitings;

namespace Aquality.Selenium.Forms
{
    /// <summary>
    /// Defines base class for any UI form.
    /// </summary>
    public abstract class Form : Form<IElement>
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
        /// Visualization configuration used by <see cref="Form{T}.Dump"/>.
        /// </summary>
        protected override IVisualizationConfiguration VisualizationConfiguration => AqualityServices.Get<IVisualizationConfiguration>();

        /// <summary>
        /// Localized logger used by <see cref="Form{T}.Dump"/>.
        /// </summary>
        protected override ILocalizedLogger LocalizedLogger => AqualityServices.LocalizedLogger;

        /// <summary>
        /// Conditional wait <see cref="IConditionalWait"/>
        /// </summary>
        protected static IConditionalWait ConditionalWait => AqualityServices.ConditionalWait;

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
        public override string Name { get; }

        /// <summary>
        /// Provides ability to get form's state (whether it is displayed, exists or not) and respective waiting functions.
        /// </summary>
        public IElementStateProvider State => FormElement.State;

        /// <summary>
        /// Gets size of form element defined by its locator.
        /// </summary>
        public Size Size => FormElement.Visual.Size;

        /// <summary>
        /// Scroll form without scrolling entire page
        /// </summary>
        /// <param name="x">horizontal coordinate</param>
        /// <param name="y">vertical coordinate</param>
        public void ScrollBy(int x, int y)
        {
            FormElement.JsActions.ScrollBy(x, y);
        }
    }
}
