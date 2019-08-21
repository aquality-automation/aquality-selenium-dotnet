using System.Collections.Generic;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class AddRemoveElementsForm : TheInternetForm
    {        
        public AddRemoveElementsForm() : base(By.XPath("//h3[contains(.,'Add/Remove Elements')]"), "Add/Remove Elements")
        {
        }

        public IButton AddBtn => ElementFactory.GetButton(By.XPath("//button[contains(@onclick,'addElement')]"), "Add element");

        public IList<IButton> ListOfDeleteBtns => ElementFactory.FindElements(By.XPath("//button[contains(@class,'added-manually')]"), ElementFactory.GetButton);

        protected override string UrlPart => "add_remove_elements/";
    }
}
