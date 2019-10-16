using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Core.Elements.Interfaces;
using Aquality.Selenium.Core.Localization;
using Aquality.Selenium.Core.Waitings;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using CoreFactory = Aquality.Selenium.Core.Elements.ElementFactory;
using IElementFactory = Aquality.Selenium.Elements.Interfaces.IElementFactory;


namespace Aquality.Selenium.Elements
{
    /// <summary>
    /// Factory that creates elements.
    /// </summary>
    public class ElementFactory : CoreFactory, IElementFactory
    {
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

        /// <summary>
        /// Gets map between elements interfaces and their implementations.
        /// Can be extended for custom elements with custom interfaces.
        /// </summary>
        /// <returns>Dictionary where key is interface and value is its implementation.</returns>
        protected override IDictionary<Type, Type> ElementTypesMap
        {
            get
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
}
