using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Forms;
using NUnit.Framework;

namespace Aquality.Selenium.Tests.Integration.Usecases
{
    internal class VisualTests : UITest
    {
        [SetUp]
        public void BeforeTest()
        {
            OpenAutomationPracticeSite();
            AqualityServices.Browser.Maximize();
        }

        [Test]
        public void Should_BePossibleTo_CheckVisualState_WhenItHasNotChanged()
        {
            var sliderForm = new SliderForm();
            Assume.That(sliderForm.State.WaitForDisplayed(), "Slider Form is not opened");
            var style = sliderForm.Style;
            Assert.DoesNotThrow(() => sliderForm.Dump.Save(), "Should be possible to save dump");
            Assert.That(sliderForm.Dump.Compare() == 0L || sliderForm.Style != style, "Form dump should remain the same, unless the slider has already scrolled");
        }

        [Test]
        public void Should_BePossibleTo_CheckVisualState_WhenItDifferes()
        {
            const string dumpName = "the differed slider";
            var sliderForm = new SliderForm();
            Assume.That(sliderForm.State.WaitForDisplayed(), "Slider Form is not opened");
            var style = sliderForm.Style;
            Assert.DoesNotThrow(() => sliderForm.Dump.Save(dumpName), "Should be possible to save dump");
            sliderForm.ClickNextButton();
            sliderForm.WaitForSliding();
            Assert.That(sliderForm.Dump.Compare(dumpName), Is.GreaterThan(0), "After clicking on slider next button, the form dump should differ");
        }

        [Test]
        public void Should_BePossibleTo_CheckVisualState_WhenItIsTheSame()
        {
            const string dumpName = "the same slider";
            var sliderForm = new SliderForm();
            Assume.That(sliderForm.State.WaitForDisplayed(), "Slider Form is not opened");
            sliderForm.ClickNextButton();
            sliderForm.ClickNextButton();
            sliderForm.WaitForSliding();
            var style = sliderForm.Style;
            Assert.DoesNotThrow(() => sliderForm.Dump.Save(dumpName), "Should be possible to save dump"); 
            sliderForm.ClickNextButton();
            sliderForm.WaitForSliding();
            Assert.That(sliderForm.Dump.Compare(dumpName), Is.GreaterThan(0), "After clicking on slider next button, the form dump should differ");
            AqualityServices.ConditionalWait.WaitForTrue(() =>
            {
                sliderForm.ClickNextButton();
                sliderForm.WaitForSliding();
                return style == sliderForm.Style;
            }, message: "After some sliding, slider should get back to first slide");
            Assert.That(sliderForm.Dump.Compare(dumpName), Is.AtMost(0.02), "After slider returned to initial state, the form dump should be almost the same");
        }
    }
}
