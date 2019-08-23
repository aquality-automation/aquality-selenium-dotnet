using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Forms
{
    internal class AuthenticationForm : Form
    {
        public AuthenticationForm() : base(By.Id("email_create"), "Authentication")
        {
        }

        private IButton CreateAccountButton => ElementFactory.GetButton(By.Id("SubmitCreate"), "Submit Create");

        private ITextBox EmailTextBox => ElementFactory.GetTextBox(By.Id("email_create"), "Email Create");

        public void ClickCreateAccountButton()
        {
            CreateAccountButton.ClickAndWait();
        }

        public void SetEmail(string email)
        {
            EmailTextBox.ClearAndType(email);
        }
    }
}
