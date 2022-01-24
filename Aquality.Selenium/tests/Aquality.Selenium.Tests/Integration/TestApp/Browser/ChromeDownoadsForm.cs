using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.Browser.Forms
{
    internal class ChromeDownoadsForm : Form
    {
        private const string Address = "chrome://downloads/";
        public By NestedShadowRootLocator => By.Id("moreActionsMenu");
        public ILabel DownloadsToolbarLabel => FormElement.FindElementInShadowRoot<ILabel>(By.CssSelector("downloads-toolbar"), "Downloads toolbar");
        public ILabel MainContainerLabel => FormElement.FindElementInShadowRoot<ILabel>(By.Id("mainContainer"), "main container");
        public ILabel DownloadsToolbarLabelFromJs => FormElement.JsActions.FindElementInShadowRoot<ILabel>(By.CssSelector("downloads-toolbar"), "Downloads toolbar");
        public ILabel MainContainerLabelFromJs => FormElement.JsActions.FindElementInShadowRoot<ILabel>(By.Id("mainContainer"), "main container");

        public ChromeDownoadsForm() : base(By.TagName("downloads-manager"), "Chrome downloads manager")
        {
        }

        public void Open()
        {
            AqualityServices.Browser.GoTo(Address);
        }

        public ShadowRoot ExpandShadowRoot()
        {
            return FormElement.ExpandShadowRoot();
        }

        public ShadowRoot ExpandShadowRootViaJs()
        {
            return FormElement.JsActions.ExpandShadowRoot();
        }
    }
}
