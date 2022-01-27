using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Tests.Integration.TestApp.Browser.Forms;
using NUnit.Framework;

namespace Aquality.Selenium.Tests.Integration
{
    internal class ShadowRootTests : UITest
    {
        private static readonly ChromeDownloadsForm form = new ChromeDownloadsForm();

        [SetUp]
        public void OpenDownloads()
        {
            ChromeDownoadsForm.Open();
        }

        [Test]
        public void Should_ExpandShadowRoot_FromElement()
        {
            Assert.IsNotNull(form.ExpandShadowRoot(), "Should be possible to expand shadow root and get Selenium native ShadowRoot object");            
            Assert.IsNotNull(form.DownloadsToolbarLabel.GetElement(), "Should be possible do get the element hidden under the shadow");
            Assert.IsNotNull(form.DownloadsToolbarLabel.FindElementInShadowRoot<ILabel>(ChromeDownloadsForm.NestedShadowRootLocator, "More actions menu").GetElement(),
                "Should be possible to expand the nested shadow root and get the element from it");
            Assert.IsTrue(form.MainContainerLabel.State.IsDisplayed, "Should be possible to check that element under the shadow is displayed");
        }

        [Test]
        public void ShouldBePossibleTo_ExpandShadowRoot_ViaJs()
        {
            Assert.IsNotNull(form.ExpandShadowRootViaJs(), "Should be possible to expand shadow root and get Selenium native ShadowRoot object");
            Assert.IsNotNull(form.DownloadsToolbarLabelFromJs.GetElement(), "Should be possible do get the element hidden under the shadow");
            Assert.IsNotNull(form.DownloadsToolbarLabelFromJs.JsActions.FindElementInShadowRoot<ILabel>(ChromeDownloadsForm.NestedShadowRootLocator, "More actions menu").GetElement(),
                "Should be possible to expand the nested shadow root and get the element from it");
            Assert.IsTrue(form.MainContainerLabelFromJs.State.IsDisplayed, "Should be possible to check that element under the shadow is displayed");
        }
    }
}
