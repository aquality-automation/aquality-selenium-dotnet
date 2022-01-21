using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using OpenQA.Selenium;
using System;

namespace Aquality.Selenium.Tests.Integration.Usecases
{
    internal class ElementExistsButNotDisplayedTests : UITest
    {
        private static readonly HoversForm hoversForm = new HoversForm();
        private readonly By fakeElement = By.XPath("//fake");
        private readonly TimeSpan smallTimeout = TimeSpan.FromSeconds(1);

        [SetUp]
        public void BeforeTest()
        {
            hoversForm.Open();
        }

        [Test]
        public void Should_BePossibleTo_WaitForElement_WhichExistsButNotDisplayed()
        {
            var button = hoversForm.GetHiddenElement(HoverExample.First, ElementState.ExistsInAnyState);
            Assert.IsTrue(AqualityServices.ConditionalWait.WaitFor(() => button.State.IsExist && !button.State.IsDisplayed));
        }

        [Test]
        public void Should_ThrowWebDriverTimeoutException_WhenElementNotInDesiredState()
        {
            Assert.Throws<WebDriverTimeoutException>(() => hoversForm.GetHiddenElement(HoverExample.First, ElementState.Displayed).GetElement(smallTimeout));
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
