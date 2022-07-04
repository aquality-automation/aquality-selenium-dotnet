using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class ChallengingDomForm : TheInternetForm
    {
        public ChallengingDomForm() : base(By.XPath("//h3[contains(text(),'Challenging DOM')]"), "Challenging dom form")
        {
        }

        protected override string UrlPart => "challenging_dom";

        public static readonly string ELEMENT_NAME_ROW3_COLUMN5 = "Cell in row 3 column 5";
        public static string ELEMENT_NAME_ROW5_COLUMN5 = "Cell in row 5 column 5";
        public static string ELEMENT_NAME_ROW7_COLUMN5 = "Cell in row 7 column 5";
        public static string ELEMENT_NAME_ROW5_COLUMN7 = "Cell in row 5 column 7";
        public static string ELEMENT_NAME_ROW5_COLUMN3 = "Cell in row 5 column 3";
        public static string ELEMENT_NAME_ROW1_COLUMN1 = "Cell in row 1 column 1";
        public static string ELEMENT_NAME_ROW2_COLUMN1 = "Cell in row 2 column 1";

        public static readonly string locatorCellRow5Column5 = "//tr[5]/td[5]";
        public const string locatorCellRow1Column5 = "//tr[1]/td[5]";
        public const string locatorCellRow3Column5 = "//tr[3]/td[5]";
        public const string locatorCellRow7Column5 = "//tr[7]/td[5]";
        public const string locatorCellRow5Column3 = "//tr[5]/td[3]";
        public const string locatorCellRow5Column7 = "//tr[5]/td[7]";
        public const string locatorCellRow1Column1 = "//tr[1]/td[1]";
        public const string locatorCellRow2Column1 = "//tr[2]/td[1]";


        public ILabel CellInRow3Column5 => ElementFactory.GetLabel(By.XPath(locatorCellRow3Column5), ELEMENT_NAME_ROW3_COLUMN5);
        public ILabel CellInRow5Column5 => ElementFactory.GetLabel(By.XPath(locatorCellRow5Column5), ELEMENT_NAME_ROW5_COLUMN5);
        public ILabel CellInRow7Column5 => ElementFactory.GetLabel(By.XPath(locatorCellRow7Column5), ELEMENT_NAME_ROW7_COLUMN5);
        public ILabel CellInRow5Column7 => ElementFactory.GetLabel(By.XPath(locatorCellRow5Column7), ELEMENT_NAME_ROW5_COLUMN7);
        public ILabel CellInRow5Column3 => ElementFactory.GetLabel(By.XPath(locatorCellRow5Column3), ELEMENT_NAME_ROW5_COLUMN3);
        public ILabel CellInRow1Column1 => ElementFactory.GetLabel(By.XPath(locatorCellRow1Column1), ELEMENT_NAME_ROW1_COLUMN1);
        public ILabel CellInRow2Column1 => ElementFactory.GetLabel(By.XPath(locatorCellRow2Column1), ELEMENT_NAME_ROW2_COLUMN1);
    }
}
