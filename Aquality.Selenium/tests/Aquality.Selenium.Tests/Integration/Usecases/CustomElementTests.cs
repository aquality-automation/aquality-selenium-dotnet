using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Elements;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.Usecases
{
    internal class CustomElementTests : UITest
    {        
        [Test]
        public void ShouldBe_PossibleTo_CreateCustomTextBox()
        {
            var authForm = new AuthenticationForm();
            authForm.Open();
            var userNameTxb = authForm.UserNameTextBox;

            var userNameCustomTxb = new CustomTextBox(userNameTxb.Locator, userNameTxb.Name);
            userNameTxb.Type("wrong");
            userNameCustomTxb.Type("right");
            Assert.That(userNameCustomTxb.Text, Is.EqualTo(userNameTxb.Value));
        }

        private class CustomTextBox : TextBox
        {
            public CustomTextBox(By locator, string name) : base(locator, name, ElementState.ExistsInAnyState)
            {
            }

            public new string Text => Value;
        }
    }    
}
