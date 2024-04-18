using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Tests.Integration.TestApp.Browser.Forms;
using NUnit.Framework;
using System.Linq;

namespace Aquality.Selenium.Tests.Integration
{
    internal class ShadowRootTests : UITest
    {
        private static readonly ChromeDownloadsForm form = new();

        [SetUp]
        public void OpenDownloads()
        {
            ChromeDownloadsForm.Open();
        }

        [Test]
        public void Should_ExpandShadowRoot_FromElement()
        {
            Assert.That(form.ExpandShadowRoot(), Is.Not.Null, "Should be possible to expand shadow root and get Selenium native ShadowRoot object");
        }

        [Test]
        public void Should_BePossibleTo_FindElement_InShadowRoot()
        {
            Assert.That(form.DownloadsToolbarLabel.GetElement(), Is.Not.Null, "Should be possible do get the element hidden under the shadow");
            Assert.That(form.DownloadsToolbarLabel.FindElementInShadowRoot<ILabel>(ChromeDownloadsForm.NestedShadowRootLocator, "More actions menu").GetElement(), Is.Not.Null,
                "Should be possible to expand the nested shadow root and get the element from it");            
            Assert.That(form.MainContainerLabel.State.IsDisplayed, "Should be possible to check that element under the shadow is displayed");
        }

        [Test]
        public void Should_BePossibleTo_FindElements_InShadowRoot()
        {
            var elementLabels = form.DivElementLabels;
            Assert.That(elementLabels, Has.Count.GreaterThan(1), "Should be possible to find multiple elements hidden under the shadow");
            Assert.That(elementLabels.First().Locator.Mechanism, Contains.Substring("css"), "Unique locator of correct type should be generated");
            Assert.That(elementLabels.First().GetElement().TagName, Is.EqualTo("div"), "Should be possible to work with one of found elements");
            Assert.That(form.MainContainerLabels.First().GetElement().TagName, Is.EqualTo("div"), "Should be possible to work with one of found elements found by id");
        }

        [Test]
        public void ShouldBePossibleTo_ExpandShadowRoot_ViaJs()
        {
            Assert.That(form.ExpandShadowRootViaJs(), Is.Not.Null, "Should be possible to expand shadow root and get Selenium native ShadowRoot object");
            Assert.That(form.DownloadsToolbarLabelFromJs.GetElement(), Is.Not.Null, "Should be possible do get the element hidden under the shadow");
            var elementLabels = form.DivElementLabelsFromJs;
            Assert.That(elementLabels, Has.Count.GreaterThan(1), "Should be possible to find multiple elements hidden under the shadow");
            Assert.That(elementLabels.First().Locator.Mechanism, Contains.Substring("css"), "Unique locator of correct type should be generated");
            Assert.That(elementLabels.First().GetElement().TagName, Is.EqualTo("div"), "Should be possible to work with one of found elements");
            Assert.That(form.MainContainerLabelsFromJs.First().GetElement().TagName, Is.EqualTo("div"), "Should be possible to work with one of found elements found by id");
            Assert.That(form.DownloadsToolbarLabelFromJs.JsActions.FindElementInShadowRoot<ILabel>(ChromeDownloadsForm.NestedShadowRootLocator, "More actions menu").GetElement(), Is.Not.Null, "Should be possible to expand the nested shadow root and get the element from it");
            Assert.That(form.MainContainerLabelFromJs.State.IsDisplayed, "Should be possible to check that element under the shadow is displayed");
        }
    }
}
