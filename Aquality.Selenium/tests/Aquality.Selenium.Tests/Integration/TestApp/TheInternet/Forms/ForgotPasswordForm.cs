using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class ForgotPasswordForm : TheInternetForm
    {
        public ForgotPasswordForm() : base(By.Id("checkboxes"), "CheckBoxes")
        {
        }

        public static ITextBox EmailTextBox => ElementFactory.GetTextBox(By.Id("email"), "E-mail");

        public static IButton RetrievePasswordButton => ElementFactory.GetButton(By.Id("form_submit"), "Retrieve password");

        protected override string UrlPart => "forgot_password";
    }
}
