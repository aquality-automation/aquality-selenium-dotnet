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

        [SetUp]
        public void Before()
        {
            challengingDomForm.Open();
            //AqualityServices.Browser.GoTo(WelcomeForm.Url);
        }

        [Test]
        public void Should_BePossibleTo_AboveWithDifferentParametersType()
        {
            var url = new WelcomeForm().Url;
            var browser = AqualityServices.Browser;

            var cellInRow5Column5 = challengingDomForm.CellInRow5Column5;
            var cellInRow3Column5 = challengingDomForm.CellInRow3Column5;


            var actualWebElementCellRaw3Column5GotBySeleniumRelative1 =
                    AqualityServices.Browser.Driver.FindElement(RelativeBy
                            .WithLocator(By.XPath(labelLocatorCell))
                            .Above(By.XPath(ChallengingDomForm.locatorCellRow5Column5)));
            var text35 = actualWebElementCellRaw3Column5GotBySeleniumRelative1.Text;




            var actualCellRaw3Column5GotWithByXpath =
                    ElementFactory.GetLabel(RelativeBy
                    .WithLocator(By.XPath(labelLocatorCell))
                    .Above(By.XPath(ChallengingDomForm.locatorCellRow5Column5)),
                    ChallengingDomForm.ELEMENT_NAME_ROW5_COLUMN5);

             ILabel actualCellRaw3Column5GotWithWebElement =
                     ElementFactory.GetLabel(RelativeBy
                     .WithLocator(By.XPath(labelLocatorCell))
                     .Above(cellInRow5Column5.GetElement()),
                             ChallengingDomForm.ELEMENT_NAME_ROW3_COLUMN5);

             ILabel actualCellRaw3Column5GotWithAqualityElement =
                     ElementFactory.GetLabel(RelativeBy.WithLocator(By.XPath(labelLocatorCell)).Above(cellInRow5Column5),
                             ChallengingDomForm.ELEMENT_NAME_ROW3_COLUMN5);

             var actualWebElementCellRaw3Column5GotBySeleniumRelative =
                     AqualityServices.Browser.Driver.FindElement(RelativeSeleniumBy
                             .WithLocator(By.XPath(labelLocatorCell))
                             .Above(By.XPath(ChallengingDomForm.locatorCellRow5Column5)));

            var expectedText = cellInRow3Column5.Text;

            string text1 = actualWebElementCellRaw3Column5GotBySeleniumRelative.Text;
            //  string text2 = actualCellRaw3Column5GotWithByXpath.Text;
            // string text3 = actualCellRaw3Column5GotWithWebElement.Text;
            //  string text4 = actualCellRaw3Column5GotWithAqualityElement.Text;
            string text5 = actualWebElementCellRaw3Column5GotBySeleniumRelative.Text;

            Assert.Multiple(() =>
            {
                Assert.AreEqual(expectedText, actualWebElementCellRaw3Column5GotBySeleniumRelative.Text, "Text got with above by relative selenium is not expected");
                Assert.AreEqual(expectedText, actualCellRaw3Column5GotWithByXpath.Text, "Text got with above by xpath is not expected");
                Assert.AreEqual(expectedText, actualCellRaw3Column5GotWithWebElement.Text, "Text got with above by webelement is not expected");
                Assert.AreEqual(expectedText, actualCellRaw3Column5GotWithAqualityElement.Text, "Text got with above by aqualityElement is not expected");
            });



        }
    }
}
