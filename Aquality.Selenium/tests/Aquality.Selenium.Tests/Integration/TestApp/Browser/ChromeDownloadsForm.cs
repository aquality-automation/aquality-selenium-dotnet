using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Aquality.Selenium.Tests.Integration.TestApp.Browser.Forms
{
    internal class ChromeDownloadsForm : Form
    {
        private const string Address = "chrome://downloads/";

        public static By NestedShadowRootLocator => By.Id("moreActionsMenu");
        public ILabel DownloadsToolbarLabel => FormElement.FindElementInShadowRoot<ILabel>(By.CssSelector("downloads-toolbar"), "Downloads toolbar");
        public IList<ILabel> DivElementLabels => FormElement.FindElementsInShadowRoot<ILabel>(By.CssSelector("div"), "div");
        public IList<ILabel> MainContainerLabels => FormElement.FindElementsInShadowRoot<ILabel>(By.Id("mainContainer"), "main container");
        public ILabel MainContainerLabel => FormElement.FindElementInShadowRoot<ILabel>(By.Id("mainContainer"), "main container");
        public ILabel DownloadsToolbarLabelFromJs => FormElement.JsActions.FindElementInShadowRoot<ILabel>(By.CssSelector("downloads-toolbar"), "Downloads toolbar");
        public IList<ILabel> DivElementLabelsFromJs => FormElement.JsActions.FindElementsInShadowRoot<ILabel>(By.CssSelector("div"), "div");
        public IList<ILabel> MainContainerLabelsFromJs => FormElement.JsActions.FindElementsInShadowRoot<ILabel>(By.Id("mainContainer"), "main container");
        public ILabel MainContainerLabelFromJs => FormElement.JsActions.FindElementInShadowRoot<ILabel>(By.Id("mainContainer"), "Main container");

        public ChromeDownloadsForm() : base(By.TagName("downloads-manager"), "Chrome downloads manager")
        {
        }

        public static void Open()
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
