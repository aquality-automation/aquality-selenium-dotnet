using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Tests.Integration.TestApp;
using Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Forms;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Aquality.Selenium.Tests.Integration.Usecases
{
    internal class ElementExistsButNotDisplayedTests : UITest
    {
        private readonly SliderForm sliderForm = new SliderForm();
        private readonly By fakeElement = By.XPath("//fake");
        private readonly TimeSpan smallTimeout = TimeSpan.FromSeconds(1);

        [SetUp]
        public void BeforeTest()
        {
            OpenAutomationPracticeSite();
        }

        [Test]
        public void Should_BePossibleTo_WaitForElement_WhichExistsButNotDisplayed()
        {
            var button = new SliderForm().GetAddToCartBtn(ElementState.ExistsInAnyState);
            Assert.IsTrue(AqualityServices.ConditionalWait.WaitFor(() => button.State.IsExist && !button.State.IsDisplayed));
        }

        [Test]
        public void Should_ThrowWebDriverTimeoutException_WhenElementNotInDesiredState()
        {
            Assert.Throws<WebDriverTimeoutException>(() => sliderForm.GetAddToCartBtn(ElementState.Displayed).GetElement(smallTimeout));
        }

        [Test]
        public void Should_ThrowNoSuchElementException_WhenElementNotFound_ButExpectedToBeDisplayed()
        {
            var elementFactory = AqualityServices.Get<IElementFactory>();
            Assert.Throws<NoSuchElementException>(() => elementFactory.GetButton(fakeElement, "Fake", ElementState.Displayed).GetElement(smallTimeout));
        }

        [Test]
        public void Should_ThrowNoSuchElementException_WhenElementNotFound_ButExpectedToExist()
        {
            var elementFactory = AqualityServices.Get<IElementFactory>();
            Assert.Throws<NoSuchElementException>(() => elementFactory.GetButton(fakeElement, "Fake", ElementState.ExistsInAnyState).GetElement(smallTimeout));
        }
    }
}
