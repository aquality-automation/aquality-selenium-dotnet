using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class DropdownForm : Form
    {
        public DropdownForm() : base(By.Id("content"), "Dropdown")
        {
        }

        public IComboBox ComboBox => ElementFactory.GetComboBox(By.Id("dropdown"), "Dropdown");
    }
}
