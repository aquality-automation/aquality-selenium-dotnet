using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class ContextMenuForm : TheInternetForm
    {
        public ContextMenuForm() : base(By.XPath("//h3[contains(.,'Context Menu')]"), "Context Menu")
        {
        }

        protected override string UrlPart => "context_menu";

        public ILabel HotSpotLabel => ElementFactory.GetLabel(By.Id("hot-spot"), "Hot spot");
    }
}
