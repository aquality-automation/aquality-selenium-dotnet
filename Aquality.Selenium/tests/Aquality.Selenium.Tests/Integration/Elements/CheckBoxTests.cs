using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;

namespace Aquality.Selenium.Tests.Integration.Elements
{
    internal class CheckBoxTests : UITest
    {
        private readonly CheckBoxesForm checkBoxesForm = new CheckBoxesForm();

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
            Assert.AreEqual(!checkBox1State, checkBox1.IsChecked);
        }

        [Test]
        public void Should_BePossibleTo_Uncheck()
        {
            var checkBox = checkBoxesForm.SecondCheckBox;
            checkBox.Uncheck();
            Assert.IsFalse(checkBox.IsChecked);
        }

        [Test]
        public void Should_BePossibleTo_Check()
        {
            var checkBox = checkBoxesForm.FirstCheckBox;
            checkBox.Check();
            Assert.IsTrue(checkBox.IsChecked);
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
                Assert.IsTrue(checkBox1.JsActions.GetState());
                Assert.IsFalse(checkBox2.JsActions.GetState());
            });
        }
    }
}
