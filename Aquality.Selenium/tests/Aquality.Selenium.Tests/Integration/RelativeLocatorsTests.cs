using Aquality.Selenium.Browsers;
using Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms;
using NUnit.Framework;
using Aquality.Selenium.Elements.Interfaces;
using RelativeSeleniumBy = OpenQA.Selenium.RelativeBy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Aquality.Selenium.Locators;
using By = OpenQA.Selenium.By;

using Aquality.Selenium.Core.Elements;

namespace Aquality.Selenium.Tests.Integration
{
    internal class RelativeLocatorsTests : UITest
    {
        private readonly ChallengingDomForm challengingDomForm = new ChallengingDomForm();
        private const string labelLocatorCell = "//td";
        private static IElementFactory ElementFactory => AqualityServices.Get<IElementFactory>();
        private const string ABOVE = "above";
        private const string BELOW = "below";
        private const string LEFT = "left";
        private const string RIGHT = "right";
        private const string SELENIUM_RELATIVE = "selenium relative";
        private const string XPATH = "xpath";
        private const string WEB_ELEMENT = "web element";
        private const string AQUALITY_ELEMENT = "aquality element";

        private static string GetMessageError(string gotWith, string gotBy) => $"Text of element got with [{ gotWith }] by [{ gotBy }] is not expected"; 

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
                    ElementFactory.GetLabel(RelativeBy
                    .WithLocator(By.XPath(labelLocatorCell))
                    .Above(By.XPath(ChallengingDomForm.locatorCellRow5Column5)),
                    ChallengingDomForm.ELEMENT_NAME_ROW5_COLUMN5);

             var actualCellRaw3Column5GotWithWebElement =
                     ElementFactory.GetLabel(RelativeBy
                     .WithLocator(By.XPath(labelLocatorCell))
                     .Above(cellInRow5Column5.GetElement()),
                             ChallengingDomForm.ELEMENT_NAME_ROW3_COLUMN5);

             var actualCellRaw3Column5GotWithAqualityElement =
                     ElementFactory.GetLabel(RelativeBy.WithLocator(By.XPath(labelLocatorCell)).Above(cellInRow5Column5),
                             ChallengingDomForm.ELEMENT_NAME_ROW3_COLUMN5);

             var actualWebElementCellRaw3Column5GotBySeleniumRelative =
                     AqualityServices.Browser.Driver.FindElement(RelativeSeleniumBy
                             .WithLocator(By.XPath(labelLocatorCell))
                             .Above(By.XPath(ChallengingDomForm.locatorCellRow5Column5)));

            var expectedText = cellInRow3Column5.Text;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedText, actualWebElementCellRaw3Column5GotBySeleniumRelative.Text, GetMessageError(ABOVE, SELENIUM_RELATIVE));
                Assert.AreEqual(expectedText, actualCellRaw3Column5GotWithByXpath.Text, GetMessageError(ABOVE, XPATH));
                Assert.AreEqual(expectedText, actualCellRaw3Column5GotWithWebElement.Text, GetMessageError(ABOVE, WEB_ELEMENT));
                Assert.AreEqual(expectedText, actualCellRaw3Column5GotWithAqualityElement.Text, GetMessageError(ABOVE, AQUALITY_ELEMENT));
            });
        }

        [Test]
        public void Should_BePossibleTo_BelowWithDifferentParametersType()
        {
            var cellInRow5Column5 = challengingDomForm.CellInRow5Column5;
            var cellInRow7Column5 = challengingDomForm.CellInRow7Column5;

            var actualCellRaw7Column5GotWithByXpath =
                    ElementFactory.GetLabel(RelativeBy.WithLocator(By.XPath(labelLocatorCell))
                   .Below(By.XPath(ChallengingDomForm.locatorCellRow5Column5)), ChallengingDomForm.ELEMENT_NAME_ROW7_COLUMN5);

            var actualCellRaw7Column5GotWithWebElement =
                    ElementFactory.GetLabel(RelativeBy.WithLocator(By.XPath(labelLocatorCell))
                    .Below(cellInRow5Column5.GetElement()), ChallengingDomForm.ELEMENT_NAME_ROW7_COLUMN5);

            var actualCellRaw7Column5GotWithAqualityElement =
                    ElementFactory.GetLabel(RelativeBy.WithLocator(By.XPath(labelLocatorCell))
                    .Below(cellInRow5Column5),ChallengingDomForm.ELEMENT_NAME_ROW7_COLUMN5);

            var actualWebElementCellRaw7Column5GotBySeleniumRelative =
                    AqualityServices.Browser.Driver.FindElement(RelativeSeleniumBy
                    .WithLocator(By.XPath(labelLocatorCell))
                            .Below(By.XPath(ChallengingDomForm.locatorCellRow5Column5)));

            var expectedText = cellInRow7Column5.Text;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedText, actualWebElementCellRaw7Column5GotBySeleniumRelative.Text, GetMessageError(BELOW, SELENIUM_RELATIVE));
                Assert.AreEqual(expectedText, actualCellRaw7Column5GotWithByXpath.Text, GetMessageError(BELOW, XPATH));
                Assert.AreEqual(expectedText, actualCellRaw7Column5GotWithWebElement.Text, GetMessageError(BELOW, WEB_ELEMENT));
                Assert.AreEqual(expectedText, actualCellRaw7Column5GotWithAqualityElement.Text, GetMessageError(BELOW, AQUALITY_ELEMENT));
            });
        }

        [Test]
        public void Should_BePossibleTo_LeftWithDifferentParametersType()
        {
            var cellInRow5Column5 = challengingDomForm.CellInRow5Column5;
            var cellInRow5Column3 = challengingDomForm.CellInRow5Column3;

            var actualCellRaw5Column3GotWithByXpath =
                    ElementFactory.GetLabel(RelativeBy.WithLocator(By.XPath(labelLocatorCell))
                   .Left(By.XPath(ChallengingDomForm.locatorCellRow5Column5)), ChallengingDomForm.ELEMENT_NAME_ROW5_COLUMN3);

            var actualCellRaw5Column3GotWithWebElement =
                    ElementFactory.GetLabel(RelativeBy.WithLocator(By.XPath(labelLocatorCell))
                    .Left(cellInRow5Column5.GetElement()), ChallengingDomForm.ELEMENT_NAME_ROW5_COLUMN3);

            var actualCellRaw5Column3GotWithAqualityElement =
                    ElementFactory.GetLabel(RelativeBy.WithLocator(By.XPath(labelLocatorCell))
                    .Left(cellInRow5Column5), ChallengingDomForm.ELEMENT_NAME_ROW5_COLUMN3);

            var actualWebElementCellRaw5Column3GotBySeleniumRelative =
                    AqualityServices.Browser.Driver.FindElement(RelativeSeleniumBy
                    .WithLocator(By.XPath(labelLocatorCell))
                            .LeftOf(By.XPath(ChallengingDomForm.locatorCellRow5Column5)));

            var expectedText = cellInRow5Column3.Text;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedText, actualWebElementCellRaw5Column3GotBySeleniumRelative.Text, GetMessageError(LEFT, SELENIUM_RELATIVE));
                Assert.AreEqual(expectedText, actualCellRaw5Column3GotWithByXpath.Text, GetMessageError(LEFT, XPATH));
                Assert.AreEqual(expectedText, actualCellRaw5Column3GotWithWebElement.Text, GetMessageError(LEFT, WEB_ELEMENT));
                Assert.AreEqual(expectedText, actualCellRaw5Column3GotWithAqualityElement.Text, GetMessageError(LEFT, AQUALITY_ELEMENT));
            });
        }

        [Test]
        public void Should_BePossibleTo_RightWithDifferentParametersType()
        {
            var cellInRow5Column5 = challengingDomForm.CellInRow5Column5;
            var cellInRow5Column7 = challengingDomForm.CellInRow5Column7;

            var actualCellRaw5Column7GotWithByXpath =
                    ElementFactory.GetLabel(RelativeBy.WithLocator(By.XPath(labelLocatorCell))
                   .Right(By.XPath(ChallengingDomForm.locatorCellRow5Column5)), ChallengingDomForm.ELEMENT_NAME_ROW5_COLUMN7);

            var actualCellRaw5Column7GotWithWebElement =
                    ElementFactory.GetLabel(RelativeBy.WithLocator(By.XPath(labelLocatorCell))
                    .Right(cellInRow5Column5.GetElement()), ChallengingDomForm.ELEMENT_NAME_ROW5_COLUMN7);

            var actualCellRaw5Column7GotWithAqualityElement =
                    ElementFactory.GetLabel(RelativeBy.WithLocator(By.XPath(labelLocatorCell))
                    .Right(cellInRow5Column5), ChallengingDomForm.ELEMENT_NAME_ROW5_COLUMN7);

            var actualWebElementCellRaw5Column7GotBySeleniumRelative =
                    AqualityServices.Browser.Driver.FindElement(RelativeSeleniumBy
                    .WithLocator(By.XPath(labelLocatorCell))
                            .RightOf(By.XPath(ChallengingDomForm.locatorCellRow5Column5)));

            var expectedText = cellInRow5Column7.Text;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedText, actualWebElementCellRaw5Column7GotBySeleniumRelative.Text, GetMessageError(RIGHT, SELENIUM_RELATIVE));
                Assert.AreEqual(expectedText, actualCellRaw5Column7GotWithByXpath.Text, GetMessageError(RIGHT, XPATH));
                Assert.AreEqual(expectedText, actualCellRaw5Column7GotWithWebElement.Text, GetMessageError(RIGHT, WEB_ELEMENT));
                Assert.AreEqual(expectedText, actualCellRaw5Column7GotWithAqualityElement.Text, GetMessageError(RIGHT, AQUALITY_ELEMENT));
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
                    ElementFactory.GetLabel(RelativeBy.WithLocator(By.XPath(labelLocatorCell))
                   .Above(By.XPath(ChallengingDomForm.locatorCellRow7Column5))
                   .Below(By.XPath(ChallengingDomForm.locatorCellRow3Column5))
                   .Left(By.XPath(ChallengingDomForm.locatorCellRow5Column7))
                   .Right(By.XPath(ChallengingDomForm.locatorCellRow5Column3))
                   .Above(By.XPath(ChallengingDomForm.locatorCellRow7Column5))
                   , ChallengingDomForm.ELEMENT_NAME_ROW3_COLUMN5);

            var actualCellRaw3Column5GotWithWebElement =
                    ElementFactory.GetLabel(RelativeBy.WithLocator(By.XPath(labelLocatorCell))
                    .Above(cellInRow7Column5.GetElement())
                    .Below(cellInRow3Column5.GetElement())
                    .Left(cellInRow5Column7.GetElement())
                    .Right(cellInRow5Column3.GetElement())
                    .Above(cellInRow7Column5.GetElement())
                    , ChallengingDomForm.ELEMENT_NAME_ROW3_COLUMN5);

            var actualCellRaw3Column5GotWithAqualityElement =
                    ElementFactory.GetLabel(RelativeBy.WithLocator(By.XPath(labelLocatorCell))
                    .Above(cellInRow7Column5)
                    .Below(cellInRow3Column5)
                    .Left(cellInRow5Column7)
                    .Right(cellInRow5Column3)
                    .Above(cellInRow7Column5)
                    , ChallengingDomForm.ELEMENT_NAME_ROW3_COLUMN5);

            /*var actualWebElementCellRaw3Column5GotBySeleniumRelative =
                  AqualityServices.Browser.Driver.FindElement(RelativeSeleniumBy
                  .WithLocator(By.XPath(labelLocatorCell))
                  .Above(By.XPath(ChallengingDomForm.locatorCellRow7Column5))
                  .Below(By.XPath(ChallengingDomForm.locatorCellRow3Column5))
                  .LeftOf(By.XPath(ChallengingDomForm.locatorCellRow5Column7))
                  .RightOf(By.XPath(ChallengingDomForm.locatorCellRow5Column3))
                  .Above(By.XPath(ChallengingDomForm.locatorCellRow7Column5)));*/




            var actualWebElementCellRaw3Column5GotBySeleniumRelative =
                  AqualityServices.Browser.Driver.FindElement(RelativeSeleniumBy
                  .WithLocator(RelativeSeleniumBy.WithLocator(By.XPath(labelLocatorCell)).Above(By.XPath(ChallengingDomForm.locatorCellRow7Column5)))

                  .Below(By.XPath(ChallengingDomForm.locatorCellRow3Column5)));
                  




            var expectedText = cellInRow5Column5.Text;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedText, actualWebElementCellRaw3Column5GotBySeleniumRelative.Text, GetMessageError(RIGHT, SELENIUM_RELATIVE));
              //  Assert.AreEqual(expectedText, actualCellRaw3Column5GotWithByXpath.Text, GetMessageError(RIGHT, XPATH));
              //  Assert.AreEqual(expectedText, actualCellRaw3Column5GotWithWebElement.Text, GetMessageError(RIGHT, WEB_ELEMENT));
              //  Assert.AreEqual(expectedText, actualCellRaw3Column5GotWithAqualityElement.Text, GetMessageError(RIGHT, AQUALITY_ELEMENT));
            });
        }





    }
}
