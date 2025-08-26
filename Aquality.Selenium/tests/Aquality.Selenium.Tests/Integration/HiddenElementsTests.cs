using Aquality.Selenium.Browsers;
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
        private static readonly HoversForm hoversForm = new();

        private static readonly Func<ElementState, ElementsCount, IList<ILabel>>[] ElementListProviders
            =
            [
                HoversForm.GetListElements,
                hoversForm.GetListElementsByName,
                hoversForm.GetListElementsByClassName,
                hoversForm.GetListElementsByCss,
                hoversForm.GetListElementsByDottedXPath,
                hoversForm.GetChildElementsByDottedXPath,
                (state, count) => new List<ILabel> { hoversForm.GetChildElementByNonXPath(state) }
            ];

        [SetUp]
        public void BeforeTest()
        {
            hoversForm.Open();
        }

        [Test]
        public void Should_BePossibleTo_CheckThatHiddenElementExists()
        {
            Assert.That(HoversForm.GetHiddenElement(HoverExample.First, ElementState.ExistsInAnyState).State.IsExist);
        }

        [Test]
        public void Should_BePossibleTo_CheckThatHiddenElementsExist(
            [ValueSource(nameof(ElementListProviders))] Func<ElementState, ElementsCount, IList<ILabel>> elementListProvider)
        {
            var elements = elementListProvider(ElementState.ExistsInAnyState, ElementsCount.MoreThenZero);
            Assert.Multiple(() =>
            {
                Assert.That(elements.Any());
                Assert.That(elements.All(element => element.State.WaitForExist()));
            });
        }

        [Test, Category(RetriesGroup), Retry(RetriesCount)]
        public void Should_BePossibleTo_CheckThatHiddenElementsNotDisplayed(
            [ValueSource(nameof(ElementListProviders))] Func<ElementState, ElementsCount, IList<ILabel>> elementListProvider)
        {
            var elements = elementListProvider(ElementState.ExistsInAnyState, ElementsCount.MoreThenZero);
            Assert.Multiple(() =>
            {
                Assert.That(elements.Any());
                Assert.That(elements.All(element =>
                {
                    var result = element.State.WaitForNotDisplayed();
                    if (!result)
                    {
                        AqualityServices.Browser.Refresh();
                        result = element.State.WaitForNotDisplayed();
                    }
                    return result;
                }));
            });
        }
    }
}
