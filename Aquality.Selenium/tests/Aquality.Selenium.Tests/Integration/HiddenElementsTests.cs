using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aquality.Selenium.Tests.Integration
{
    internal class HiddenElementsTests : UITest
    {
        private static readonly HoversForm hoversForm = new HoversForm();

        private static readonly Func<ElementState, ElementsCount, IList<ILabel>>[] ElementListProviders
            = new Func<ElementState, ElementsCount, IList<ILabel>>[]
            {
                (state, count) => HoversForm.GetListElements(state, count),
                (state, count) => hoversForm.GetListElementsByName(state, count),
                (state, count) => hoversForm.GetListElementsByClassName(state, count),
                (state, count) => hoversForm.GetListElementsByCss(state, count),
                (state, count) => hoversForm.GetListElementsByDottedXPath(state, count),
                (state, count) => hoversForm.GetChildElementsByDottedXPath(state, count),
                (state, count) => new List<ILabel> { hoversForm.GetChildElementByNonXPath(state) }
            };

        [SetUp]
        public void BeforeTest()
        {
            hoversForm.Open();
        }

        [Test]
        public void Should_BePossibleTo_CheckThatHiddenElementExists()
        {
            Assert.IsTrue(HoversForm.GetHiddenElement(HoverExample.First, ElementState.ExistsInAnyState).State.IsExist);
        }

        [Test]
        public void Should_BePossibleTo_CheckThatHiddenElementsExist(
            [ValueSource(nameof(ElementListProviders))] Func<ElementState, ElementsCount, IList<ILabel>> elementListProvider)
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
            [ValueSource(nameof(ElementListProviders))] Func<ElementState, ElementsCount, IList<ILabel>> elementListProvider)
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
