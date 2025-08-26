using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Images.Locators;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration
{
    internal class ImageLocatorTests : UITest
    {
        private readonly BrokenImagesForm form = new();

        [Test, Category(RetriesGroup), Retry(RetriesCount)]
        public void Should_BePossibleTo_FindByImage()
        {
            new CheckBoxesForm().Open();
            Assert.That(form.LabelByImage.State.IsDisplayed, Is.False, "Should be impossible to find element on page by image when it is absent");
            form.Open();
            Assert.That(form.LabelByImage.State.IsDisplayed, "Should be possible to find element on page by image");
            Assert.That(form.LabelByImage.GetElement().TagName, Is.EqualTo("img"), "Correct element must be found");

            var childLabels = form.ChildLabelsByImage;
            var docLabels = form.LabelsByImage;
            Assert.That(docLabels.Count, Is.GreaterThan(1), "List of elements should be possible to find by image");
            Assert.That(docLabels.Count, Is.EqualTo(childLabels.Count), "Should be possible to find child elements by image with the same count");

            var documentByTag = AqualityServices.Get<IElementFactory>().GetLabel(By.TagName("body"), "document by tag");
            var fullThreshold = 1;
            var getDocByImage = () => AqualityServices.Get<IElementFactory>().GetLabel(new ByImage(documentByTag.GetElement().GetScreenshot().AsByteArray) { Threshold = fullThreshold },
                    "body screen");
            ILabel documentByImage = getDocByImage();
            AqualityServices.ConditionalWait.WaitForTrue(() =>
            {
                documentByImage = getDocByImage();
                return documentByImage.State.IsDisplayed;
            });
            Assert.That(documentByImage.State.IsDisplayed, "Should be possible to find element by document screenshot");
            Assert.That((documentByImage.Locator as ByImage)?.Threshold, Is.EqualTo(fullThreshold), "Should be possible to get ByImage threshold");
            Assert.That(documentByImage.GetElement().TagName, Is.EqualTo("body"), "Correct element must be found");
        }
    }
}
