using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements;
using Aquality.Selenium.Tests.TestApp;
using Aquality.Selenium.Tests.TestApp.TheInternet.Forms;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Tests.Usecases
{
    [TestFixture]
    internal class CustomElementTests
    {        
        [Test]
        public void ShouldBe_PossibleTo_CreateCustomTextBox()
        {
            BrowserManager.Browser.Navigate().GoToUrl(TheInternetPage.Login);
            var authForm = new FormAuthenticationForm();
            var userNameTxb = authForm.UserNameTxb;

            var userNameCustomTxb = new ElementFactory().GetCustomElement((locator, name, state) => new CustomTextBox(locator, name, state), 
                userNameTxb.Locator, userNameTxb.Name, ElementState.ExistsInAnyState);
            userNameTxb.Type("wrong");
            userNameCustomTxb.Type("right");
            Assert.AreEqual(userNameTxb.Value, userNameCustomTxb.Text);
        }
    }

    class CustomTextBox : TextBox
    {
        protected internal CustomTextBox(By locator, string name, ElementState state) : base(locator, name, state)
        {
        }

        public string Text => Value;
    }
}
