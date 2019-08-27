using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements;
using Aquality.Selenium.Tests.Integration.TestApp;
using Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Forms;
using Aquality.Selenium.Waitings;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Aquality.Selenium.Tests.Integration.Usecases
{
    internal class ElementExistsButNotDisplayedTests : UITest
    {
        private readonly SliderForm sliderForm = new SliderForm();
        private readonly By fakeElement = By.XPath("//fake");
        private readonly ElementFactory elementFactory = new ElementFactory();
        private readonly TimeSpan smallTimeout = TimeSpan.FromSeconds(1);

        [SetUp]
        public void BeforeTest()
        {
            BrowserManager.Browser.GoTo(Constants.UrlAutomationPractice);
        }

        [Test]
        public void Should_BePossibleTo_WaitForElement_WhichExistsButNotDisplayed()
        {
            var button = new SliderForm().GetAddToCartBtn(ElementState.ExistsInAnyState);
            Assert.IsTrue(ConditionalWait.WaitFor(() => button.State.IsExist && !button.State.IsDisplayed));
        }

        [Test]
        public void Should_ThrowWebDriverTimeoutException_WhenElementNotInDesiredState()
        {
            Assert.Throws<WebDriverTimeoutException>(() => sliderForm.GetAddToCartBtn(ElementState.Displayed).GetElement(smallTimeout));
        }

        [Test]
        public void Should_ThrowNoSuchElementException_WhenElementNotFound_ButExpectedToBeDisplayed()
        {
            Assert.Throws<NoSuchElementException>(() => elementFactory.GetButton(fakeElement, "Fake", ElementState.Displayed).GetElement(smallTimeout));
        }

        [Test]
        public void Should_ThrowNoSuchElementException_WhenElementNotFound_ButExpectedToExist()
        {
            Assert.Throws<NoSuchElementException>(() => elementFactory.GetButton(fakeElement, "Fake", ElementState.ExistsInAnyState).GetElement(smallTimeout));
        }
    }
}
