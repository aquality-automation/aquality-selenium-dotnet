using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.ManyTools.Forms
{
    internal class UserAgentForm : ManyToolsForm<UserAgentForm>
    {
        public UserAgentForm() : base(By.Id("maincontent"), "User agent")
        {
        }

        protected override string UrlPart => "http-html-text/user-agent-string/";
    }
}
