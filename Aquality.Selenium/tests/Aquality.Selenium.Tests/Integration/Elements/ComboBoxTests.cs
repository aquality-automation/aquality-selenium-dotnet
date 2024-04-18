using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;

namespace Aquality.Selenium.Tests.Integration.Elements
{
    internal class ComboBoxTests : UITest
    {
        private static readonly string Option2 = "Option 2";

        private readonly DropdownForm dropdownForm = new();

        [SetUp]
        public void BeforeTest()
        {
            dropdownForm.Open();
        }

        [Test]
        public void Should_BePossibleTo_SelectValueByIndex()
        {
            var comboBox = dropdownForm.ComboBox;
            var values = comboBox.Values;
            var itemIndex = values.Count - 1;
            comboBox.SelectByIndex(itemIndex);
            Assert.That(comboBox.SelectedValue, Is.EqualTo(values[itemIndex]));
        }

        [Test]
        public void Should_BePossibleTo_SelectByText()
        {
            var comboBox = dropdownForm.ComboBox;
            var selectedText = comboBox.SelectedText;
            comboBox.SelectByText(Option2);
            AqualityServices.ConditionalWait.WaitFor(() => !selectedText.Equals(comboBox.SelectedText));
            Assert.That(comboBox.JsActions.GetSelectedText(), Is.EqualTo(comboBox.Texts[2]));
        }

        [Test]
        public void Should_BePossibleTo_SelectByValue()
        {
            var comboBox = dropdownForm.ComboBox;
            var selectedText = comboBox.SelectedText;
            comboBox.SelectByValue("2");
            AqualityServices.ConditionalWait.WaitFor(() => !selectedText.Equals(comboBox.SelectedText));
            Assert.That(comboBox.JsActions.GetSelectedText(), Is.EqualTo(comboBox.Texts[2]));
        }

        [Test]
        public void Should_BePossibleTo_SelectByContainingText()
        {
            var comboBox = dropdownForm.ComboBox;
            comboBox.SelectByContainingText("1");
            Assert.That(comboBox.SelectedText, Is.EqualTo(comboBox.Texts[1]));
        }

        [Test]
        public void Should_BePossibleTo_SelectByContainingValue()
        {
            var comboBox = dropdownForm.ComboBox;
            comboBox.SelectByContainingValue("2");
            Assert.That(comboBox.SelectedValue, Is.EqualTo(comboBox.Values[2]));
        }

        [Test]
        public void Should_BePossibleTo_GetTextsViaJsActions()
        {
            var comboBox = dropdownForm.ComboBox;
            Assert.That(comboBox.JsActions.GetTexts(), Is.EquivalentTo(comboBox.Texts));
        }

        [Test]
        public void Should_BePossibleTo_GetSelectedTextViaJsActions()
        {
            var comboBox = dropdownForm.ComboBox;
            Assert.That(comboBox.JsActions.GetSelectedText(), Is.EqualTo(comboBox.SelectedText));
        }

        [Test]
        public void Should_BePossibleTo_SelectValueViaJsActions()
        {
            var comboBox = dropdownForm.ComboBox;
            comboBox.JsActions.SelectValueByText(Option2);
            Assert.That(Option2, Is.EqualTo(comboBox.SelectedText));
        }
    }
}
