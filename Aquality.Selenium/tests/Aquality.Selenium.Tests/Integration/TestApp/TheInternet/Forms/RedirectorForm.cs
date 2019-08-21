using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class RedirectorForm : TheInternetForm
    {
        public RedirectorForm() : base(By.Id("checkboxes"), "Redirector")
        {
        }

        public ILink RedirectLnk => ElementFactory.GetLink(By.Id("redirect"), "Link");

        protected override string UrlPart => "redirector";
    }
}
