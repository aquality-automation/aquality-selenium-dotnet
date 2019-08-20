using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements;
using Aquality.Selenium.Tests.Integration.TestApp;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.Usecases
{
    [TestFixture]
    internal class CustomElementTests
    {        
        [Test]
        public void ShouldBe_PossibleTo_CreateCustomTextBox()
        {
            BrowserManager.Browser.GoTo(TheInternetPage.Login);
            var authForm = new AuthenticationForm();
            var userNameTxb = authForm.UserNameTxb;

            var userNameCustomTxb = new ElementFactory().GetCustomElement((locator, name, state) => new CustomTextBox(locator, name, state), 
                userNameTxb.Locator, userNameTxb.Name, ElementState.ExistsInAnyState);
            userNameTxb.Type("wrong");
            userNameCustomTxb.Type("right");
            Assert.AreEqual(userNameTxb.Value, userNameCustomTxb.Text);
        }

        [TearDown]
        public void TearDown()
        {
            BrowserManager.Browser.Quit();
        }

        private class CustomTextBox : TextBox
        {
            protected internal CustomTextBox(By locator, string name, ElementState state) : base(locator, name, state)
            {
            }

            public string Text => Value;
        }
    }    
}
