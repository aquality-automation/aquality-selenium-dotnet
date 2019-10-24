using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Waitings;
using Aquality.Selenium.Elements;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Aquality.Selenium.Tests.Integration.Elements
{
    internal class TextBoxTests : UITest
    {
        private readonly AuthenticationForm authForm = new AuthenticationForm();

        [SetUp]
        public void BeforeTest()
        {
            authForm.Open();
        }

        [Test]
        public void Should_BePossibleTo_FocusAndType()
        {
            var text = "wrong";
            var usernameTxb = authForm.UserNameTextBox;
            usernameTxb.Type(text);
            Assert.AreEqual(text, usernameTxb.Value);
        }

        [Test]
        public void Should_BePossibleTo_ClearAndType()
        {
            var initialText = "initial value";
            var targetText = "target value";
            var usernameTxb = authForm.UserNameTextBox;
            usernameTxb.Type(initialText);
            usernameTxb.ClearAndType(targetText);
            Assert.AreEqual(targetText, usernameTxb.Value);
        }

        [Test]
        public void Should_BePossibleTo_SendKeys()
        {
            var passwordTxb = authForm.PasswordTextBox;
            passwordTxb.SendKeys(Keys.NumberPad0);
            Assert.AreEqual("0", passwordTxb.Value);
        }

        [Test]
        public void Should_BePossibleTo_Submit()
        {
            var passwordTxb = authForm.PasswordTextBox;
            passwordTxb.Submit();
            Assert.IsTrue(BrowserManager.GetRequiredService<ConditionalWait>().WaitFor(() => passwordTxb.Value.Equals("", StringComparison.OrdinalIgnoreCase)));
        }

        [Test]
        public void Should_BePossibleTo_SetInnerHtml()
        {
            var usernameTxb = authForm.UserNameTextBox;
            usernameTxb.State.WaitForDisplayed();
            var loginLbl = authForm.LoginLabel;
            loginLbl.SetInnerHtml("<p>123123</p>");

            Assert.Multiple(() =>
            {
                Assert.IsTrue(usernameTxb.State.WaitForNotExist());
                Assert.IsTrue(authForm.GetCustomElementBasedOnLoginLabel("/p[.='123123']").State.WaitForDisplayed());
            });
        }

        [Test]
        public void Should_BePossibleTo_GetCssValue()
        {
            var propertyName = "font-family";
            var expectedCssValue = "Helvetica";
            var usernameTxb = authForm.UserNameTextBox;
            Assert.Multiple(() =>
            {
                Assert.IsTrue(usernameTxb.GetCssValue(propertyName).Contains(expectedCssValue));
                Assert.IsTrue(usernameTxb.GetCssValue(propertyName, HighlightState.Highlight).Contains(expectedCssValue));
            });
        }

        [Test]
        public void Should_ThrowNoSuchElementException_ForNotExistElement_OnSendKeys()
        {
            Assert.Throws<NoSuchElementException>(() => authForm.NotExistTextBox.SendKeys(Keys.Backspace));
        }
    }
}
