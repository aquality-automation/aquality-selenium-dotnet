using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;

namespace Aquality.Selenium.Tests.Integration.Elements
{
    internal class CheckBoxTests : UITest
    {
        private readonly CheckBoxesForm checkBoxesForm = new();

        [SetUp]
        public void BeforeTest()
        {
            checkBoxesForm.Open();
        }

        [Test]
        public void Should_BePossibleTo_Toggle()
        {
            var checkBox1 = checkBoxesForm.FirstCheckBox;
            var checkBox1State = checkBox1.IsChecked;
            checkBox1.Toggle();
            Assert.That(checkBox1.IsChecked, Is.EqualTo(!checkBox1State));
        }

        [Test]
        public void Should_BePossibleTo_Uncheck()
        {
            var checkBox = checkBoxesForm.SecondCheckBox;
            checkBox.Uncheck();
            Assert.That(checkBox.IsChecked, Is.False);
        }

        [Test]
        public void Should_BePossibleTo_Check()
        {
            var checkBox = checkBoxesForm.FirstCheckBox;
            checkBox.Check();
            Assert.That(checkBox.IsChecked);
        }

        [Test]
        public void Should_BePossibleTo_GetStateViaJsActions()
        {
            var checkboxesForm = new CheckBoxesForm();
            var checkBox1 = checkBoxesForm.FirstCheckBox;
            checkBox1.Check();
            var checkBox2 = checkBoxesForm.SecondCheckBox;
            checkBox2.Uncheck();
            
            Assert.Multiple(() =>
            {
                Assert.That(checkBox1.JsActions.IsChecked());
                Assert.That(checkBox2.JsActions.IsChecked(), Is.False);
            });
        }
    }
}
