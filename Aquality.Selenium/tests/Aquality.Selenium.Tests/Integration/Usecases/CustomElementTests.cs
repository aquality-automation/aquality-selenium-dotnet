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
            Assert.AreEqual(userNameTxb.Value, userNameCustomTxb.Text);
        }

        private class CustomTextBox : TextBox
        {
            public CustomTextBox(By locator, string name) : base(locator, name, ElementState.ExistsInAnyState)
            {
            }

            public string Text => Value;
        }
    }    
}
