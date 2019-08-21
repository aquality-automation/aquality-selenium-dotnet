using Aquality.Selenium.Elements;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using Aquality.Selenium.Waitings;
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
            var usernameTxb = authForm.UserNameTxb;
            usernameTxb.Type(text);
            Assert.AreEqual(text, usernameTxb.Value);
        }

        [Test]
        public void Should_BePossibleTo_ClearAndType()
        {
            var initialText = "initial value";
            var targetText = "target value";
            var usernameTxb = authForm.UserNameTxb;
            usernameTxb.Type(initialText);
            usernameTxb.ClearAndType(targetText);
            Assert.AreEqual(targetText, usernameTxb.Value);
        }

        [Test]
        public void Should_BePossibleTo_SendKeys()
        {
            var passwordTxb = authForm.PasswordTxb;
            passwordTxb.SendKeys(Keys.NumberPad0);
            Assert.AreEqual("0", passwordTxb.Value);
        }

        [Test]
        public void Should_BePossibleTo_Submit()
        {
            var passwordTxb = authForm.PasswordTxb;
            passwordTxb.Submit();
            Assert.IsTrue(ConditionalWait.WaitFor(() => passwordTxb.Value.Equals("", StringComparison.OrdinalIgnoreCase)));
        }

        [Test]
        public void Should_BePossibleTo_SetInnerHtml()
        {
            var usernameTxb = authForm.UserNameTxb;
            usernameTxb.State.WaitForDisplayed();
            var loginLbl = authForm.LoginLbl;
            loginLbl.SetInnerHtml("<p>123123</p>");

            Assert.Multiple(() =>
            {
                Assert.IsTrue(usernameTxb.State.WaitForNotExist());
                Assert.IsTrue(authForm.GetCustomElementBasedOnLoginLbl("/p[.='123123']").State.WaitForDisplayed());
            });
        }

        [Test]
        public void Should_BePossibleTo_GetCssValue()
        {
            var propertyName = "font-family";
            var expectedCssValue = "Helvetica";
            var usernameTxb = authForm.UserNameTxb;
            Assert.Multiple(() =>
            {
                Assert.IsTrue(usernameTxb.GetCssValue(propertyName).Contains(expectedCssValue));
                Assert.IsTrue(usernameTxb.GetCssValue(propertyName, HighlightState.Highlight).Contains(expectedCssValue));
            });
        }

        [Test]
        public void Should_ThrowNoSuchElementException_ForNotExistElement_OnSendKeys()
        {
            Assert.Throws<NoSuchElementException>(() => authForm.NotExistTxb.SendKeys(Keys.Backspace));
        }
    }
}
