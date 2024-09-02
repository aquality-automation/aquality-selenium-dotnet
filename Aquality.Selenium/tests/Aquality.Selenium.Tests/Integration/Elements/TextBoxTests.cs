using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements;
using Aquality.Selenium.Elements.Actions;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Aquality.Selenium.Tests.Integration.Elements
{
    internal class TextBoxTests : UITest
    {
        private readonly AuthenticationForm authForm = new();

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
            Assert.That(usernameTxb.Value, Is.EqualTo(text));
        }

        [Test]
        public void Should_BePossibleTo_Clear_WhenWasEmpty()
        {
            var usernameTxb = authForm.UserNameTextBox;
            usernameTxb.Clear();
            Assert.That(usernameTxb.Value, Is.Empty);
        }

        [Test]
        public void Should_BePossibleTo_Clear_WhenWasFilled()
        {
            var initialText = "initial value";
            var usernameTxb = authForm.UserNameTextBox;
            usernameTxb.Type(initialText);
            usernameTxb.Clear();
            Assert.That(usernameTxb.Value, Is.Empty);
        }

        [Test]
        public void Should_BePossibleTo_Clear_Twice()
        {
            var usernameTxb = authForm.UserNameTextBox;
            usernameTxb.Clear();
            usernameTxb.Clear();
            Assert.That(usernameTxb.Value, Is.Empty);
        }

        [Test]
        public void Should_BePossibleTo_ClearAndType()
        {
            var initialText = "initial value";
            var targetText = "target value";
            var usernameTxb = authForm.UserNameTextBox;
            usernameTxb.Type(initialText);
            usernameTxb.ClearAndType(targetText);
            Assert.That(usernameTxb.Value, Is.EqualTo(targetText));
        }

        [Test]
        public void Should_BePossibleTo_SendKey()
        {
            var passwordTxb = authForm.PasswordTextBox;
            passwordTxb.SendKey(Key.NumberPad0);
            Assert.That(passwordTxb.Value, Is.EqualTo("0"));
        }

        [Test]
        public void Should_BePossibleTo_SendKeys()
        {
            var passwordTxb = authForm.PasswordTextBox;
            passwordTxb.SendKeys("00");
            Assert.That(passwordTxb.Value, Is.EqualTo("00"));
        }

        [Test]
        public void Should_BePossibleTo_Submit()
        {
            var passwordTxb = authForm.PasswordTextBox;
            passwordTxb.Submit();
            Assert.That(AqualityServices.ConditionalWait.WaitFor(() => passwordTxb.Value.Equals("", StringComparison.OrdinalIgnoreCase)));
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
                Assert.That(usernameTxb.State.WaitForNotExist());
                Assert.That(authForm.GetCustomElementBasedOnLoginLabel("/p[.='123123']").State.WaitForDisplayed());
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
                Assert.That(usernameTxb.GetCssValue(propertyName).Contains(expectedCssValue));
                Assert.That(usernameTxb.GetCssValue(propertyName, HighlightState.Highlight).Contains(expectedCssValue));
            });
        }

        [Test]
        public void Should_ThrowNoSuchElementException_ForNotExistElement_OnSendKeys()
        {
            Assert.Throws<NoSuchElementException>(() => authForm.NotExistTextBox.SendKeys(Keys.Backspace));
        }
    }
}
