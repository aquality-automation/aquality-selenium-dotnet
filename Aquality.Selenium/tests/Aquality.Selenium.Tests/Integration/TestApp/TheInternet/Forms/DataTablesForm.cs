using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class DataTablesForm : TheInternetForm
    {
        const string TABLE1_BODY = "//table[@id='table1']//tbody";

        public DataTablesForm() : base(By.XPath("//h3[contains(text(),'Data Tables')]"), "Data Tables form")
        {
        }
        protected override string UrlPart => "tables";

        public static readonly string ElementNameRow4Column5 = "Cell in row 4 column 5";
        public static readonly string ElementNameRow4Column4 = "Cell in row 4 column 4";
        public static readonly string ElementNameRow4Column3 = "Cell in row 4 column 3";
        public static readonly string ElementNameRow4Column2 = "Cell in row 4 column 2";
        public static readonly string ElementNameRow3Column5 = "Cell in row 3 column 5";
        public static readonly string ElementNameRow2Column5 = "Cell in row 2 column 5";
        public static readonly string ElementNameRow2Column4 = "Cell in row 2 column 4";
        public static readonly string ElementNameRow2Column3 = "Cell in row 2 column 3";

        public static readonly string LocatorCellRow4Column5 = $"{TABLE1_BODY}//tr[4]/td[5]";
        public static readonly string LocatorCellRow4Column4 = $"{TABLE1_BODY}//tr[4]/td[4]";
        public static readonly string LocatorCellRow4Column2 = $"{TABLE1_BODY}//tr[4]/td[2]";
        public static readonly string LocatorCellRow3Column5 = $"{TABLE1_BODY}//tr[3]/td[5]";
        public static readonly string LocatorCellRow4Column3 = $"{TABLE1_BODY}//tr[4]/td[3]";
        public static readonly string LocatorCellRow2Column5 = $"{TABLE1_BODY}//tr[2]/td[5]";
        public static readonly string LocatorCellRow2Column4 = $"{TABLE1_BODY}//tr[2]/td[4]";
        public static readonly string LocatorCellRow2Column3 = $"{TABLE1_BODY}//tr[2]/td[3]";

        public static readonly string LocatorCellRow2Column2 = $"{TABLE1_BODY}//tr[2]/td[2]";
        public static readonly string LocatorCellRow2Column1 = $"{TABLE1_BODY}//tr[2]/td[1]";

        public ILabel CellInRow4Column5 => ElementFactory.GetLabel(By.XPath(LocatorCellRow4Column5), ElementNameRow4Column5);
        public ILabel CellInRow4Column4 => ElementFactory.GetLabel(By.XPath(LocatorCellRow4Column4), ElementNameRow4Column4);
        public ILabel CellInRow4Column3 => ElementFactory.GetLabel(By.XPath(LocatorCellRow4Column3), ElementNameRow4Column3);
        public ILabel CellInRow4Column2 => ElementFactory.GetLabel(By.XPath(LocatorCellRow4Column2), ElementNameRow4Column2);
        public ILabel CellInRow3Column5 => ElementFactory.GetLabel(By.XPath(LocatorCellRow3Column5), ElementNameRow3Column5);
        public ILabel CellInRow2Column5 => ElementFactory.GetLabel(By.XPath(LocatorCellRow2Column5), ElementNameRow2Column5);
        public ILabel CellInRow2Column4 => ElementFactory.GetLabel(By.XPath(LocatorCellRow2Column4), ElementNameRow2Column4);
        public ILabel CellInRow2Column3 => ElementFactory.GetLabel(By.XPath(LocatorCellRow2Column3), ElementNameRow2Column3);
    }
}
