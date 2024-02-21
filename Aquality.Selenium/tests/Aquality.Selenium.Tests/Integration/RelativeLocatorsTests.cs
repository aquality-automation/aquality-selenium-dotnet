using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Locators;
using By = OpenQA.Selenium.By;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration
{
    internal class RelativeLocatorsTests : UITest //TODO! This code should be removed during refactoring
    {
        private readonly ChallengingDomForm challengingDomForm = new ChallengingDomForm();
        private const string labelLocatorCell = "//td";
        private static IElementFactory ElementFactory => AqualityServices.Get<IElementFactory>();
        private const string Above = "above";
        private const string Below = "below";
        private const string Left = "left";
        private const string Right = "right";
        private const string SeleniumRelative = "selenium relative";
        private const string Xpath = "xpath";
        private const string WebElement = "web element";
        private const string AqualityElement = "aquality element";

        private static string MessageError(string gotWith, string gotBy) => $"Text of element got with [{gotWith}] by [{gotBy}] is not expected";

        [SetUp]
        public void Before()
        {
            challengingDomForm.Open();
        }

        [Test]
        public void Should_BePossibleTo_AboveWithDifferentParametersType()
        {
            var cellInRow5Column5 = challengingDomForm.CellInRow5Column5;
            var cellInRow3Column5 = challengingDomForm.CellInRow3Column5;

            var actualCellRaw3Column5GotWithByXpath =
                    ElementFactory.GetLabel(RelativeAqualityBy
                    .WithLocator(By.XPath(labelLocatorCell))
                    .Above(By.XPath(ChallengingDomForm.LocatorCellRow5Column5)),
                    ChallengingDomForm.ElementNameRow5Column5);

            var actualCellRaw3Column5GotWithWebElement =
                    ElementFactory.GetLabel(RelativeAqualityBy
                    .WithLocator(By.XPath(labelLocatorCell))
                    .Above(cellInRow5Column5.GetElement()),
                            ChallengingDomForm.ElementNameRow3Column5);

            var actualCellRaw3Column5GotWithAqualityElement =
                    ElementFactory.GetLabel(RelativeAqualityBy.WithLocator(By.XPath(labelLocatorCell)).Above(cellInRow5Column5),
                            ChallengingDomForm.ElementNameRow3Column5);

            var actualWebElementCellRaw3Column5GotBySeleniumRelative =
                    AqualityServices.Browser.Driver.FindElement(RelativeBy
                            .WithLocator(By.XPath(labelLocatorCell))
                            .Above(By.XPath(ChallengingDomForm.LocatorCellRow5Column5)));

            var expectedText = cellInRow3Column5.Text;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedText, actualWebElementCellRaw3Column5GotBySeleniumRelative.Text, MessageError(Above, SeleniumRelative));
                Assert.AreEqual(expectedText, actualCellRaw3Column5GotWithByXpath.Text, MessageError(Above, Xpath));
                Assert.AreEqual(expectedText, actualCellRaw3Column5GotWithWebElement.Text, MessageError(Above, WebElement));
                Assert.AreEqual(expectedText, actualCellRaw3Column5GotWithAqualityElement.Text, MessageError(Above, AqualityElement));
            });
        }

        [Test]
        public void Should_BePossibleTo_BelowWithDifferentParametersType()
        {
            var cellInRow5Column5 = challengingDomForm.CellInRow5Column5;
            var cellInRow7Column5 = challengingDomForm.CellInRow7Column5;

            var actualCellRaw7Column5GotWithByXpath =
                    ElementFactory.GetLabel(RelativeAqualityBy.WithLocator(By.XPath(labelLocatorCell))
                   .Below(By.XPath(ChallengingDomForm.LocatorCellRow5Column5)), ChallengingDomForm.ElementNameRow7Column5);

            var actualCellRaw7Column5GotWithWebElement =
                    ElementFactory.GetLabel(RelativeAqualityBy.WithLocator(By.XPath(labelLocatorCell))
                    .Below(cellInRow5Column5.GetElement()), ChallengingDomForm.ElementNameRow7Column5);

            var actualCellRaw7Column5GotWithAqualityElement =
                    ElementFactory.GetLabel(RelativeAqualityBy.WithLocator(By.XPath(labelLocatorCell))
                    .Below(cellInRow5Column5), ChallengingDomForm.ElementNameRow7Column5);

            var actualWebElementCellRaw7Column5GotBySeleniumRelative =
                    AqualityServices.Browser.Driver.FindElement(RelativeBy
                    .WithLocator(By.XPath(labelLocatorCell))
                            .Below(By.XPath(ChallengingDomForm.LocatorCellRow5Column5)));

            var expectedText = cellInRow7Column5.Text;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedText, actualWebElementCellRaw7Column5GotBySeleniumRelative.Text, MessageError(Below, SeleniumRelative));
                Assert.AreEqual(expectedText, actualCellRaw7Column5GotWithByXpath.Text, MessageError(Below, Xpath));
                Assert.AreEqual(expectedText, actualCellRaw7Column5GotWithWebElement.Text, MessageError(Below, WebElement));
                Assert.AreEqual(expectedText, actualCellRaw7Column5GotWithAqualityElement.Text, MessageError(Below, AqualityElement));
            });
        }

        [Test]
        public void Should_BePossibleTo_LeftWithDifferentParametersType()
        {
            var cellInRow5Column5 = challengingDomForm.CellInRow5Column5;
            var cellInRow5Column3 = challengingDomForm.CellInRow5Column3;

            var actualCellRaw5Column3GotWithByXpath =
                    ElementFactory.GetLabel(RelativeAqualityBy.WithLocator(By.XPath(labelLocatorCell))
                   .Left(By.XPath(ChallengingDomForm.LocatorCellRow5Column5)), ChallengingDomForm.ElementNameRow5Column3);

            var actualCellRaw5Column3GotWithWebElement =
                    ElementFactory.GetLabel(RelativeAqualityBy.WithLocator(By.XPath(labelLocatorCell))
                    .Left(cellInRow5Column5.GetElement()), ChallengingDomForm.ElementNameRow5Column3);

            var actualCellRaw5Column3GotWithAqualityElement =
                    ElementFactory.GetLabel(RelativeAqualityBy.WithLocator(By.XPath(labelLocatorCell))
                    .Left(cellInRow5Column5), ChallengingDomForm.ElementNameRow5Column3);

            var actualWebElementCellRaw5Column3GotBySeleniumRelative =
                    AqualityServices.Browser.Driver.FindElement(RelativeBy
                    .WithLocator(By.XPath(labelLocatorCell))
                            .LeftOf(By.XPath(ChallengingDomForm.LocatorCellRow5Column5)));

            var expectedText = cellInRow5Column3.Text;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedText, actualWebElementCellRaw5Column3GotBySeleniumRelative.Text, MessageError(Left, SeleniumRelative));
                Assert.AreEqual(expectedText, actualCellRaw5Column3GotWithByXpath.Text, MessageError(Left, Xpath));
                Assert.AreEqual(expectedText, actualCellRaw5Column3GotWithWebElement.Text, MessageError(Left, WebElement));
                Assert.AreEqual(expectedText, actualCellRaw5Column3GotWithAqualityElement.Text, MessageError(Left, AqualityElement));
            });
        }

        [Test]
        public void Should_BePossibleTo_RightWithDifferentParametersType()
        {
            var cellInRow5Column5 = challengingDomForm.CellInRow5Column5;
            var cellInRow5Column7 = challengingDomForm.CellInRow5Column7;

            var actualCellRaw5Column7GotWithByXpath =
                    ElementFactory.GetLabel(RelativeAqualityBy.WithLocator(By.XPath(labelLocatorCell))
                   .Right(By.XPath(ChallengingDomForm.LocatorCellRow5Column5)), ChallengingDomForm.ElementNameRow5Column7);

            var actualCellRaw5Column7GotWithWebElement =
                    ElementFactory.GetLabel(RelativeAqualityBy.WithLocator(By.XPath(labelLocatorCell))
                    .Right(cellInRow5Column5.GetElement()), ChallengingDomForm.ElementNameRow5Column7);

            var actualCellRaw5Column7GotWithAqualityElement =
                    ElementFactory.GetLabel(RelativeAqualityBy.WithLocator(By.XPath(labelLocatorCell))
                    .Right(cellInRow5Column5), ChallengingDomForm.ElementNameRow5Column7);

            var actualWebElementCellRaw5Column7GotBySeleniumRelative =
                    AqualityServices.Browser.Driver.FindElement(RelativeBy
                    .WithLocator(By.XPath(labelLocatorCell))
                            .RightOf(By.XPath(ChallengingDomForm.LocatorCellRow5Column5)));

            var expectedText = cellInRow5Column7.Text;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedText, actualWebElementCellRaw5Column7GotBySeleniumRelative.Text, MessageError(Right, SeleniumRelative));
                Assert.AreEqual(expectedText, actualCellRaw5Column7GotWithByXpath.Text, MessageError(Right, Xpath));
                Assert.AreEqual(expectedText, actualCellRaw5Column7GotWithWebElement.Text, MessageError(Right, WebElement));
                Assert.AreEqual(expectedText, actualCellRaw5Column7GotWithAqualityElement.Text, MessageError(Right, AqualityElement));
            });
        }

        [Test]
        public void Should_BePossibleTo_AboveBelowLeftWrightAboveWithDifferentParametersType()
        {
            var cellInRow5Column3 = challengingDomForm.CellInRow5Column3;
            var cellInRow5Column5 = challengingDomForm.CellInRow5Column5;
            var cellInRow5Column7 = challengingDomForm.CellInRow5Column7;
            var cellInRow3Column5 = challengingDomForm.CellInRow3Column5;
            var cellInRow7Column5 = challengingDomForm.CellInRow7Column5;

            var actualCellRaw3Column5GotWithByXpath =
                    ElementFactory.GetLabel(RelativeAqualityBy.WithLocator(By.XPath(labelLocatorCell))
                   .Above(By.XPath(ChallengingDomForm.LocatorCellRow7Column5))
                   .Below(By.XPath(ChallengingDomForm.LocatorCellRow3Column5))
                   .Left(By.XPath(ChallengingDomForm.LocatorCellRow5Column7))
                   .Right(By.XPath(ChallengingDomForm.LocatorCellRow5Column3))
                   .Above(By.XPath(ChallengingDomForm.LocatorCellRow7Column5))
                   , ChallengingDomForm.ElementNameRow3Column5);

            var actualCellRaw3Column5GotWithWebElement =
                    ElementFactory.GetLabel(RelativeAqualityBy.WithLocator(By.XPath(labelLocatorCell))
                    .Above(cellInRow7Column5.GetElement())
                    .Below(cellInRow3Column5.GetElement())
                    .Left(cellInRow5Column7.GetElement())
                    .Right(cellInRow5Column3.GetElement())
                    .Above(cellInRow7Column5.GetElement())
                    , ChallengingDomForm.ElementNameRow3Column5);

            var actualCellRaw3Column5GotWithAqualityElement =
                    ElementFactory.GetLabel(RelativeAqualityBy.WithLocator(By.XPath(labelLocatorCell))
                    .Above(cellInRow7Column5)
                    .Below(cellInRow3Column5)
                    .Left(cellInRow5Column7)
                    .Right(cellInRow5Column3)
                    .Above(cellInRow7Column5)
                    , ChallengingDomForm.ElementNameRow3Column5);

            var actualWebElementCellRaw3Column5GotBySeleniumRelative =
                  AqualityServices.Browser.Driver.FindElement(RelativeBy
                  .WithLocator(By.XPath(labelLocatorCell))
                  .Above(By.XPath(ChallengingDomForm.LocatorCellRow7Column5))
                  .Below(By.XPath(ChallengingDomForm.LocatorCellRow3Column5))
                  .LeftOf(By.XPath(ChallengingDomForm.LocatorCellRow5Column7))
                  .RightOf(By.XPath(ChallengingDomForm.LocatorCellRow5Column3))
                  .Above(By.XPath(ChallengingDomForm.LocatorCellRow7Column5)));

            var expectedText = cellInRow5Column5.Text;
            var gotWith = $"{Above} {Below} {Left} {Right} {Above}";

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedText, actualWebElementCellRaw3Column5GotBySeleniumRelative.Text, MessageError(gotWith, SeleniumRelative));
                Assert.AreEqual(expectedText, actualCellRaw3Column5GotWithByXpath.Text, MessageError(gotWith, Xpath));
                Assert.AreEqual(expectedText, actualCellRaw3Column5GotWithWebElement.Text, MessageError(gotWith, WebElement));
                Assert.AreEqual(expectedText, actualCellRaw3Column5GotWithAqualityElement.Text, MessageError(gotWith, AqualityElement));
            });
        }
    }
}
