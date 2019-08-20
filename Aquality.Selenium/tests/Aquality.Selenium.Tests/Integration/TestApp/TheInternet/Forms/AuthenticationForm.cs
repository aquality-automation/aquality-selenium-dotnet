using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class AuthenticationForm : Form
    {
        public AuthenticationForm() : base(By.Id("login"), "login")
        {
        }

        public ITextBox UserNameTxb => ElementFactory.GetTextBox(By.Id("username"), "username");
    }
}
