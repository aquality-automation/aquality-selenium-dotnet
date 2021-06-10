using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Elements;
using Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Forms;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aquality.Selenium.Tests.Integration
{
    internal class HiddenElementsTests : UITest
    {
        private static readonly ProductTabContentForm productsForm = new ProductTabContentForm();

        private static readonly Func<ElementState, ElementsCount, IList<Label>>[] ElementListProviders
            = new Func<ElementState, ElementsCount, IList<Label>>[]
            {
                (state, count) => productsForm.GetListElements(state, count),
                (state, count) => productsForm.GetListElementsById(state, count),
                (state, count) => productsForm.GetListElementsByName(state, count),
                (state, count) => productsForm.GetListElementsByClassName(state, count),
                (state, count) => productsForm.GetListElementsByCss(state, count),
                (state, count) => productsForm.GetListElementsByDottedXPath(state, count),
                (state, count) => productsForm.GetChildElementsByDottedXPath(state, count),
                (state, count) => new List<Label> { productsForm.GetChildElementByNonXPath(state) }
            };

        [SetUp]
        public void BeforeTest()
        {
            OpenAutomationPracticeSite();
        }

        [Test]
        public void Should_BePossibleTo_CheckThatHiddenElementExists()
        {
            Assert.IsTrue(new SliderForm().GetAddToCartBtn(ElementState.ExistsInAnyState).State.IsExist);
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
