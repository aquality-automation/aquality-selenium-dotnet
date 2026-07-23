using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using System;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal abstract class TheInternetForm : Form
    {
        private ILink ElementalSeleniumLink => ElementFactory.GetLink(By.XPath("//a[contains(@href,'elementalselenium')]"), "Elemental Selenium");
        private const string BaseUrl = "https://the-internet.herokuapp.com/";

        protected TheInternetForm(By locator, string name) : base(locator, name)
        {
        }

        protected abstract string UrlPart { get; }

        public virtual string Url => BaseUrl + UrlPart;

        public void Open()
        {
            try
            {
                AqualityServices.Browser.GoTo(Url);

            }
            catch (WebDriverException e) when (e.Message.Contains("timed out", StringComparison.InvariantCultureIgnoreCase))
            {
                AqualityServices.Browser.Quit();
                AqualityServices.Browser.GoTo(Url);
            }
            AqualityServices.Browser.WaitForPageToLoad();
        }

        public void ClickElementalSelenium()
        {
            ElementalSeleniumLink.Click();
        }
    }
}
