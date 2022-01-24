using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;

namespace Aquality.Selenium.Tests.Integration.Usecases
{
    internal class VisualTests : UITest
    {
        private static readonly DynamicContentForm dynamicContentForm = new DynamicContentForm();

        [SetUp]
        public void BeforeTest()
        {
            dynamicContentForm.Open();
            AqualityServices.Browser.Maximize();
        }

        [Test]
        public void Should_BePossibleTo_CheckVisualState_WhenItHasNotChanged()
        {
            Assume.That(dynamicContentForm.State.WaitForDisplayed(), "Form is not opened");
            Assert.DoesNotThrow(() => dynamicContentForm.Dump.Save(), "Should be possible to save dump");
            Assert.That(dynamicContentForm.Dump.Compare(), Is.EqualTo(0), "Form dump should remain the same");
        }

        [Test]
        public void Should_BePossibleTo_CheckVisualState_WhenItDifferes()
        {
            const string dumpName = "the differed dump";
            
            Assume.That(dynamicContentForm.State.WaitForDisplayed(), "Form is not opened");
            Assert.DoesNotThrow(() => dynamicContentForm.Dump.Save(dumpName), "Should be possible to save named dump");
            AqualityServices.Browser.Refresh();
            Assume.That(dynamicContentForm.State.WaitForDisplayed(), "Form is not opened");
            Assert.That(dynamicContentForm.Dump.Compare(dumpName), Is.GreaterThan(0), "After clicking on slider next button, the form dump should differ");
        }
    }
}
