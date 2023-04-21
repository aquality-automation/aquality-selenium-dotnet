using Aquality.Selenium.Tests.Integration.TestApp.W3Schools.Forms;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Aquality.Selenium.Tests.Integration.Elements
{
    internal class MultiChoiceBoxTests : UITest
    {
        private readonly SelectMultipleForm selectMultipleForm = new SelectMultipleForm();

        [SetUp]
        public void BeforeTest()
        {
            SelectMultipleForm.Open();
            selectMultipleForm.AcceptCookies();
            SelectMultipleForm.SwitchToResultFrame();
        }

        [Test]
        public void Should_BePossibleTo_SelectAll()
        {
            var allTexts = selectMultipleForm.AllTexts;

            selectMultipleForm.SelectAll();
            var selected = selectMultipleForm.SelectedTexts;
            Assert.AreEqual(selected, allTexts, "Not all texts were selected");

            selected = selectMultipleForm.SelectedValues;
            selectMultipleForm.Submit();

            Assert.AreEqual(selected, selectMultipleForm.ValuesFromResult);
        }

        [Test]
        public void Should_BePossibleTo_DeselectByValue()
        {
            var valuesToRemove = new List<string> { "volvo", "saab" };

            selectMultipleForm.SelectAll();
            var remaining = selectMultipleForm.DeselectByValue(valuesToRemove);
            selectMultipleForm.Submit();

            Assert.AreEqual(remaining, selectMultipleForm.ValuesFromResult);
        }

        [Test]
        public void Should_BePossibleTo_DeselectByContainingValue()
        {
            var valuesToRemove = new List<string> { "saa", "ope" };

            selectMultipleForm.SelectAll();
            var remaining = selectMultipleForm.DeselectByContainingValue(valuesToRemove);
            selectMultipleForm.Submit();

            Assert.AreEqual(remaining, selectMultipleForm.ValuesFromResult);
        }

        [Test]
        public void Should_BePossibleTo_DeselectByText()
        {
            var valuesToRemove = new List<string> { "Opel" };

            selectMultipleForm.SelectAll();
            var remaining = selectMultipleForm.DeselectByText(valuesToRemove);
            selectMultipleForm.Submit();

            Assert.AreEqual(remaining, selectMultipleForm.ValuesFromResult);
        }

        [Test]
        public void Should_BePossibleTo_DeselectByContainingText()
        {
            var valuesToRemove = new List<string> { "Au", "Vol" };

            selectMultipleForm.SelectAll();
            var remaining = selectMultipleForm.DeselectByContainingText(valuesToRemove);
            selectMultipleForm.Submit();

            Assert.AreEqual(remaining, selectMultipleForm.ValuesFromResult);
        }

        [Test]
        public void Should_BePossibleTo_DeselectByIndex()
        {
            var valuesToRemove = new List<int> { 2, 3 };

            selectMultipleForm.SelectAll();
            var remaining = selectMultipleForm.DeselectByIndex(valuesToRemove);
            selectMultipleForm.Submit();

            Assert.AreEqual(remaining, selectMultipleForm.ValuesFromResult);
        }

        [Test]
        public void Should_BePossibleTo_DeselectAll()
        {
            selectMultipleForm.SelectAll();
            selectMultipleForm.DeselectAll();
            selectMultipleForm.Submit();

            Assert.False(selectMultipleForm.ValuesFromResult.Any());
        }
    }
}
