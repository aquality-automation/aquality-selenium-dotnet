using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Tests.Integration.TestApp;
using Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Forms;
using NUnit.Framework;
using System;
using System.Linq;

namespace Aquality.Selenium.Tests.Integration
{
    internal class HiddenElementsTests : UITest
    {
        private readonly SliderForm sliderForm = new SliderForm();

        [SetUp]
        public void BeforeTest()
        {
            AqualityServices.Browser.GoTo(Constants.UrlAutomationPractice);
        }

        [Test]
        public void Should_BePossibleTo_CheckThatHiddenElementExists()
        {
            Assert.IsTrue(sliderForm.GetAddToCartBtn(ElementState.ExistsInAnyState).State.IsExist);
        }

        [Test]
        public void Should_BePossibleTo_CheckThatHiddenElementsExist()
        {
            var elements = sliderForm.GetListElements(ElementState.ExistsInAnyState, ElementsCount.MoreThenZero);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(elements.Any());
                Assert.IsTrue(elements.All(element => element.State.WaitForExist()));
            });
        }

        [Test]
        public void Should_BePossibleTo_CheckThatHiddenElementsNotDisplayed()
        {
            var elements = sliderForm.GetListElements(ElementState.ExistsInAnyState, ElementsCount.MoreThenZero);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(elements.Any());
                Assert.IsFalse(elements.Any(element => element.State.WaitForDisplayed(TimeSpan.FromSeconds(1))));
            });
        }
    }
}
