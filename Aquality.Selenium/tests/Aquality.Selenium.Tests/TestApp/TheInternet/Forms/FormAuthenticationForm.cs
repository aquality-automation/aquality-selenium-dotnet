using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.TestApp.TheInternet.Forms
{
    internal class FormAuthenticationForm : Form
    {
        public FormAuthenticationForm() : base(By.Id("login"), "login")
        {
        }

        public ITextBox UserNameTxb => ElementFactory.GetTextBox(By.Id("username"), "username");
    }
}
