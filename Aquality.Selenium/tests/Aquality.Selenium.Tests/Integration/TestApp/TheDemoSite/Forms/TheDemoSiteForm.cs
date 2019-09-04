﻿using Aquality.Selenium.Browsers;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal abstract class TheDemoSiteForm : Form
    {
        private const string BaseUrl = "http://thedemosite.co.uk/";

        protected TheDemoSiteForm(By locator, string name) : base(locator, name)
        {
        }

        protected abstract string UrlPart { get; }

        public string Url => BaseUrl + UrlPart;

        public void Open()
        {
            BrowserManager.Browser.GoTo(Url);
            BrowserManager.Browser.WaitForPageToLoad();
        }
    }
}
