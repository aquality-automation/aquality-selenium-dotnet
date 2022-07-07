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

        public static readonly string ElementNameRow3Column5 = "Cell in row 3 column 5";
        public static readonly string ElementNameRow5Column5 = "Cell in row 5 column 5";
        public static readonly string ElementNameRow7Column5 = "Cell in row 7 column 5";
        public static readonly string ElementNameRow5Column7 = "Cell in row 5 column 7";
        public static readonly string ElementNameRow5Column3 = "Cell in row 5 column 3";
        public static readonly string ElementNameRow1Column1 = "Cell in row 1 column 1";
        public static readonly string ElementNameRow2Column1 = "Cell in row 2 column 1";

        public static readonly string LocatorCellRow5Column5 = "//tr[5]/td[5]";
        public static readonly string LocatorCellRow1Column5 = "//tr[1]/td[5]";
        public static readonly string LocatorCellRow3Column5 = "//tr[3]/td[5]";
        public static readonly string LocatorCellRow7Column5 = "//tr[7]/td[5]";
        public static readonly string LocatorCellRow5Column3 = "//tr[5]/td[3]";
        public static readonly string LocatorCellRow5Column7 = "//tr[5]/td[7]";
        public static readonly string LocatorCellRow1Column1 = "//tr[1]/td[1]";
        public static readonly string LocatorCellRow2Column1 = "//tr[2]/td[1]";


        public ILabel CellInRow3Column5 => ElementFactory.GetLabel(By.XPath(LocatorCellRow3Column5), ElementNameRow3Column5);
        public ILabel CellInRow5Column5 => ElementFactory.GetLabel(By.XPath(LocatorCellRow5Column5), ElementNameRow5Column5);
        public ILabel CellInRow7Column5 => ElementFactory.GetLabel(By.XPath(LocatorCellRow7Column5), ElementNameRow7Column5);
        public ILabel CellInRow5Column7 => ElementFactory.GetLabel(By.XPath(LocatorCellRow5Column7), ElementNameRow5Column7);
        public ILabel CellInRow5Column3 => ElementFactory.GetLabel(By.XPath(LocatorCellRow5Column3), ElementNameRow5Column3);
        public ILabel CellInRow1Column1 => ElementFactory.GetLabel(By.XPath(LocatorCellRow1Column1), ElementNameRow1Column1);
        public ILabel CellInRow2Column1 => ElementFactory.GetLabel(By.XPath(LocatorCellRow2Column1), ElementNameRow2Column1);
    }
}
