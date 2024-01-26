using Aquality.Selenium.Browsers;
using NUnit.Framework;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Locators;
using By = OpenQA.Selenium.By;
using Aquality.Selenium.Tests.Integration.TestApp.LoginFormForRelativeLocators.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration
{
    internal class RelativeLocatorsForCustomFormTests : UITest
    {
        private readonly LoginForm loginForm = new();
        private const string buttonLocator = "//button";
        private const string inputLocator = "//input";

        private static IElementFactory ElementFactory => AqualityServices.Get<IElementFactory>();
        private const string Above = "above";
        private const string Below = "below";
        private const string Left = "left";
        private const string Right = "right";
        private const string Near = "near";
        private const string SeleniumRelative = "selenium relative";
        private const string Xpath = "xpath";
        private const string WebElement = "web element";
        private const string AqualityElement = "aquality element";

        private static string MessageError(string gotWith, string gotBy) => $"Text of element got with [{gotWith}] by [{gotBy}] is not expected";
        private static string МessageForInputField(string logicKey) => $"Message text with key - [{logicKey}]";

        [SetUp]
        public void Before()
        {
            loginForm.Open();
        }

        [Test]
        public void Should_BePossibleTo_AboveWithDifferentParametersType()
        {
            var userNameTextBox = loginForm.UserNameTextBox;
            userNameTextBox.SendKeys(МessageForInputField("UserName"));
            var universityTextBox = loginForm.UniversityTextBox;

            var actualUniversityTextBoxGotWithByXpath =
                    ElementFactory.GetTextBox(RelativeAqualityBy
                    .WithLocator(By.XPath(inputLocator))
                    .Above(By.XPath(LoginForm.IdLocatorUniversityTextBox)),
                    LoginForm.ElementNameUserNameTextBox);

            var actualUniversityTextBoxGotWithWebElement =
                    ElementFactory.GetTextBox(RelativeAqualityBy
                    .WithLocator(By.XPath(inputLocator))
                    .Above(universityTextBox.GetElement()),
                            LoginForm.ElementNameUserNameTextBox);

            var actualUniversityTextBoxGotWithAqualityElement =
                    ElementFactory.GetTextBox(RelativeAqualityBy.WithLocator(By.XPath(inputLocator)).Above(universityTextBox),
                            LoginForm.ElementNameUserNameTextBox);

            var actualWebElementUniversityTextBoxGotBySeleniumRelative =
                    AqualityServices.Browser.Driver.FindElement(RelativeBy
                            .WithLocator(By.XPath(inputLocator))
                            .Above(By.XPath(LoginForm.IdLocatorUniversityTextBox)));

            var expectedText = userNameTextBox.Value;
            var _expectedText = actualWebElementUniversityTextBoxGotBySeleniumRelative.GetAttribute("value");

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedText, actualUniversityTextBoxGotWithByXpath.Value, MessageError(Above, SeleniumRelative));
                Assert.AreEqual(expectedText, actualUniversityTextBoxGotWithWebElement.Value, MessageError(Above, Xpath));
                Assert.AreEqual(expectedText, actualUniversityTextBoxGotWithAqualityElement.Value, MessageError(Above, WebElement));
                Assert.AreEqual(expectedText, actualWebElementUniversityTextBoxGotBySeleniumRelative.GetAttribute("value"), MessageError(Above, AqualityElement));
            });
        }

        [Test]
        public void Should_BePossibleTo_BelowWithDifferentParametersType()
        {
            var userNameTextBox = loginForm.UserNameTextBox;
            var universityTextBox = loginForm.UniversityTextBox;
            universityTextBox.SendKeys(МessageForInputField("University"));

            var actualUniversityTextBoxGotWithByXpath =
                    ElementFactory.GetTextBox(RelativeAqualityBy
                    .WithLocator(By.XPath(inputLocator))
                    .Below(By.XPath(LoginForm.IdLocatorUserNameTextBox)),
                    LoginForm.ElementNameUniversityTextBox);

            var actualUniversityTextBoxGotWithWebElement =
                    ElementFactory.GetTextBox(RelativeAqualityBy
                    .WithLocator(By.XPath(inputLocator))
                    .Below(userNameTextBox.GetElement()),
                     LoginForm.ElementNameUniversityTextBox);

            var actualUniversityTextBoxGotWithAqualityElement =
                    ElementFactory.GetTextBox(RelativeAqualityBy.WithLocator(By.XPath(inputLocator)).Below(userNameTextBox),
                    LoginForm.ElementNameUniversityTextBox);

            var actualWebElementUniversityTextBoxGotBySeleniumRelative =
                    AqualityServices.Browser.Driver.FindElement(RelativeBy
                            .WithLocator(By.XPath(inputLocator))
                            .Below(By.XPath(LoginForm.IdLocatorUserNameTextBox)));

            var expectedText = universityTextBox.Value;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedText, actualUniversityTextBoxGotWithByXpath.Value, MessageError(Below, Xpath ));
                Assert.AreEqual(expectedText, actualUniversityTextBoxGotWithWebElement.Value, MessageError(Below, WebElement));
                Assert.AreEqual(expectedText, actualUniversityTextBoxGotWithAqualityElement.Value, MessageError(Below, AqualityElement));
                Assert.AreEqual(expectedText, actualWebElementUniversityTextBoxGotBySeleniumRelative.GetAttribute("value"), MessageError(Below, SeleniumRelative));
            });
        }

        [Test]
        public void Should_BePossibleTo_LeftWithDifferentParametersType()
        {
            var loginButton = loginForm.LoginButton;
            var cancelButton = loginForm.CancelButton;

            var actualLoginButtonGotWithByXpath =
                    ElementFactory.GetLabel(RelativeAqualityBy.WithLocator(By.XPath(buttonLocator))
                   .Left(By.XPath(LoginForm.IdLocatorCancelButton)), "actualLoginButtonGotWithByXpath");

            var actualLoginButtonGotWithWebElement =
                    ElementFactory.GetLabel(RelativeAqualityBy.WithLocator(By.XPath(buttonLocator))
                    .Left(cancelButton.GetElement()), "actualLoginButtonGotWithWebElemen");

            var actualLoginButtonGotWithAqualityElement =
                    ElementFactory.GetLabel(RelativeAqualityBy.WithLocator(By.XPath(buttonLocator))
                    .Left(cancelButton), "actualLoginButtonGotWithAqualityElement");

            var actualWebElementLoginButtonGotBySeleniumRelative =
                    AqualityServices.Browser.Driver.FindElement(RelativeBy
                    .WithLocator(By.XPath(buttonLocator))
                            .LeftOf(By.XPath(LoginForm.IdLocatorCancelButton)));

            var expectedText = loginButton.Text;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedText, actualLoginButtonGotWithByXpath.Text, MessageError(Left, SeleniumRelative));
                Assert.AreEqual(expectedText, actualLoginButtonGotWithWebElement.Text, MessageError(Left, Xpath));
                Assert.AreEqual(expectedText, actualLoginButtonGotWithAqualityElement.Text, MessageError(Left, WebElement));
                Assert.AreEqual(expectedText, actualWebElementLoginButtonGotBySeleniumRelative.Text, MessageError(Left, AqualityElement));
            });
        }

        [Test]
        public void Should_BePossibleTo_RightWithDifferentParametersType()
        {
            var loginButton = loginForm.LoginButton;
            var cancelButton = loginForm.CancelButton;

            var actualLoginButtonGotWithByXpath =
                    ElementFactory.GetLabel(RelativeAqualityBy.WithLocator(By.XPath(buttonLocator))
                   .Right(By.XPath(LoginForm.IdLocatorLoginButton)), "actualCancelButtonGotWithByXpath");

            var actualLoginButtonGotWithWebElement =
                    ElementFactory.GetLabel(RelativeAqualityBy.WithLocator(By.XPath(buttonLocator))
                    .Right(loginButton.GetElement()), "actualCancelButtonGotWithWebElemen");

            var actualLoginButtonGotWithAqualityElement =
                    ElementFactory.GetLabel(RelativeAqualityBy.WithLocator(By.XPath(buttonLocator))
                    .Right(loginButton), "actualCancelButtonGotWithAqualityElement");

            var actualWebElementLoginButtonGotBySeleniumRelative =
                    AqualityServices.Browser.Driver.FindElement(RelativeBy
                    .WithLocator(By.XPath(buttonLocator))
                            .RightOf(By.XPath(LoginForm.IdLocatorLoginButton)));

            var expectedText = cancelButton.Text;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedText, actualLoginButtonGotWithByXpath.Text, MessageError(Right, SeleniumRelative));
                Assert.AreEqual(expectedText, actualLoginButtonGotWithWebElement.Text, MessageError(Right, Xpath));
                Assert.AreEqual(expectedText, actualLoginButtonGotWithAqualityElement.Text, MessageError(Right, WebElement));
                Assert.AreEqual(expectedText, actualWebElementLoginButtonGotBySeleniumRelative.Text, MessageError(Right, AqualityElement));
            });
        }

        [Test]
        public void Should_BePossibleTo_NearWithDifferentParametersType() 
        {
            //TODO!
        }

        [Test]
        public void Should_BePossibleTo_AboveBelowLeftWrightAboveWithDifferentParametersType()
        {
            //TODO!
        }
    }
}
