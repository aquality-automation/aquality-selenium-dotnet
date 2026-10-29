using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using System;

namespace Aquality.Selenium.Tests.Integration.Usecases
{
    internal class VisualTests : UITest
    {
        [Test]
        public void Should_BePossibleTo_CheckVisualState_WhenItHasNotChanged()
        {
            var checkboxesForm = new CheckBoxesForm();
            checkboxesForm.Open();
            AqualityServices.Browser.WaitForPageToLoad();
            Assume.That(checkboxesForm.State.WaitForDisplayed(), "Form is not opened");
            Assert.DoesNotThrow(() => checkboxesForm.Dump.Save(), "Should be possible to save dump");
            Assert.That(checkboxesForm.Dump.Compare(), Is.EqualTo(0), "Form dump should remain the same");
        }

        [Test]
        public void Should_BePossibleTo_CheckVisualState_WhenItDifferes()
        {
            var dynamicContentForm = new DynamicContentForm();
            dynamicContentForm.Open();
            AqualityServices.Browser.Maximize();
            const string dumpName = "the differed dump";
            
            Assume.That(dynamicContentForm.State.WaitForDisplayed(), "Form is not opened");
            Assert.DoesNotThrow(() => dynamicContentForm.Dump.Save(dumpName), "Should be possible to save named dump");
            AqualityServices.Browser.Refresh();
            Assume.That(dynamicContentForm.State.WaitForDisplayed(), "Form is not opened");
            Assert.That(dynamicContentForm.Dump.Compare(dumpName), Is.GreaterThan(0), "After clicking on slider next button, the form dump should differ");
        }
    }
}
