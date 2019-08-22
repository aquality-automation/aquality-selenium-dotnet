using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class DropdownForm : TheInternetForm
    {
        public DropdownForm() : base(By.Id("content"), "Dropdown")
        {
        }

        public IComboBox ComboBox => ElementFactory.GetComboBox(By.Id("dropdown"), "Dropdown");

        protected override string UrlPart => "dropdown";
    }
}
