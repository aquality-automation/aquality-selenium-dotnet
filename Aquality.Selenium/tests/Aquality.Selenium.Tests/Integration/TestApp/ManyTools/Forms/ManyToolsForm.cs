﻿using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.ManyTools.Forms
{
    internal abstract class ManyToolsForm<T> : Form where T : ManyToolsForm<T>
    {
        private const string BaseUrl = "https://manytools.org/";

        protected ManyToolsForm(By locator, string name) : base(locator, name)
        {
        }
        private ILabel ValueLabel => FormElement.FindChildElement<ILabel>(By.XPath(".//code"), Name);

        protected abstract string UrlPart { get; }

        public string Url => BaseUrl + UrlPart;

        public string Value => ValueLabel.GetText();

        public T Open()
        {
            AqualityServices.Browser.GoTo(Url);
            AqualityServices.Browser.WaitForPageToLoad();
            return (T) this;
        }
    }
}
