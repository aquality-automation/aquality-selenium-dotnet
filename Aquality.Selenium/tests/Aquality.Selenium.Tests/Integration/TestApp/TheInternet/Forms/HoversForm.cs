﻿using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class HoversForm : TheInternetForm
    {
        private const string ExampleTmpLoc = "//div[@class='figure'][{0}]";
        private const string HiddenElementTmpLoc = "//a[contains(@href,'users/{0}')]";
        private const string HiddenElementClass = "figcaption";
        private readonly By DottedXPath = By.XPath($".//div[@class='{HiddenElementClass}']");
        private readonly By ItemByName = By.Name("viewport");
        private readonly By ItemByCssSelector = By.CssSelector($".{HiddenElementClass}");
        private readonly By ItemByClassName = By.ClassName(HiddenElementClass);

        public HoversForm() : base(By.XPath("//body[.//h3[contains(.,'Hovers')]]"), "Hovers")
        {
        }

        protected override string UrlPart => "hovers";

        public ILabel GetHiddenElement(HoverExample example, ElementState state = ElementState.Displayed)
        {
            return ElementFactory.GetLabel(By.XPath(string.Format(HiddenElementTmpLoc, (int)example)), $"Hidden element for {example} example", state);
        }

        public ILabel GetExample(HoverExample example)
        {
            return ElementFactory.GetLabel(By.XPath(string.Format(ExampleTmpLoc, (int)example)), $"{example} example");
        }

        public IList<ILabel> GetListElements(ElementState state, ElementsCount count)
        {
            return ElementFactory.FindElements<ILabel>(By.XPath(string.Format(HiddenElementTmpLoc, string.Empty)), state: state, expectedCount: count);
        }

        public IList<ILabel> GetListElementsByName(ElementState state, ElementsCount count)
        {
            return ElementFactory.FindElements<ILabel>(ItemByName, state: state, expectedCount: count);
        }

        public IList<ILabel> GetListElementsByClassName(ElementState state, ElementsCount count)
        {
            return ElementFactory.FindElements<ILabel>(ItemByClassName, state: state, expectedCount: count);
        }

        public IList<ILabel> GetListElementsByCss(ElementState state, ElementsCount count)
        {
            return ElementFactory.FindElements<ILabel>(ItemByCssSelector, state: state, expectedCount: count);
        }

        public ILabel GetChildElementByNonXPath(ElementState state)
        {
            return FormElement.FindChildElement<ILabel>(ItemByCssSelector, state: state);
        }

        public IList<ILabel> GetListElementsByDottedXPath(ElementState state, ElementsCount count)
        {
            return ElementFactory.FindElements<ILabel>(DottedXPath, state: state, expectedCount: count);
        }

        public IList<ILabel> GetChildElementsByDottedXPath(ElementState state, ElementsCount count)
        {
            return FormElement.FindChildElements<ILabel>(DottedXPath, state: state, expectedCount: count);
        }
    }

    public enum HoverExample
    {
        First = 1,
        Second = 2,
        Third = 3
    }
}
