using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Elements;
using Aquality.Selenium.Tests.Integration.TestApp;
using Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Forms;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aquality.Selenium.Tests.Integration
{
    internal class HiddenElementsTests : UITest
    {
        private static readonly SliderForm sliderForm = new SliderForm();

        private static readonly Func<ElementState, ElementsCount, IList<Label>>[] ElementListProviders
            = new Func<ElementState, ElementsCount, IList<Label>>[]
            {
                (state, count) => sliderForm.GetListElements(state, count),
                (state, count) => sliderForm.GetListElementsByNonXPath(state, count),
                (state, count) => sliderForm.GetListElementsByDottedXPath(state, count)
            };

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
        public void Should_BePossibleTo_CheckThatHiddenElementsExist(
            [ValueSource(nameof(ElementListProviders))] Func<ElementState, ElementsCount, IList<Label>> elementListProvider)
        {
            var elements = elementListProvider(ElementState.ExistsInAnyState, ElementsCount.MoreThenZero);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(elements.Any());
                Assert.IsTrue(elements.All(element => element.State.WaitForExist()));
            });
        }

        [Test]
        public void Should_BePossibleTo_CheckThatHiddenElementsNotDisplayed(
            [ValueSource(nameof(ElementListProviders))] Func<ElementState, ElementsCount, IList<Label>> elementListProvider)
        {
            var elements = elementListProvider(ElementState.ExistsInAnyState, ElementsCount.MoreThenZero);
            Assert.Multiple(() =>
            {
                Assert.IsTrue(elements.Any());
                Assert.IsFalse(elements.Any(element => element.State.WaitForDisplayed(TimeSpan.FromSeconds(1))));
            });
        }
    }
}
