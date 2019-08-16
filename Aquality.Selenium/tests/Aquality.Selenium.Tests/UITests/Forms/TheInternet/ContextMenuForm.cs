﻿using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.UITests.Forms.TheInternet
{
    public class ContextMenuForm : Form
    {
        private static readonly By FormLocator = By.XPath("//h3[contains(.,'Context Menu')]");
        public ILabel LblHotSpot => ElementFactory.GetLabel(By.Id("hot-spot"), "Hot spot");

        public ContextMenuForm() : base(FormLocator, "Context Menu")
        {
        }
    }
}
