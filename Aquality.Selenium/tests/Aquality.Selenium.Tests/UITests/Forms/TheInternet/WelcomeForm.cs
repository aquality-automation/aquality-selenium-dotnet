﻿using System.Drawing;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using Aquality.Selenium.Tests.Utilities;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.UITests.Forms.AutomationPractice
{
    public class WelcomeForm : Form
    {
        private const string FormName = "Welcom form";
        private const string TmpExampleLoc = "//a[contains(@href,'{0}')]";
        public const string SubTitle = "Available Examples";
        private static readonly By FormLocator = By.XPath("//h1[contains(.,'Welcome to the-internet')]");
        private ILabel LblSubTitle => ElementFactory.GetLabel(By.XPath("//h2"), "Sub title");

        public WelcomeForm() : base(FormLocator, FormName)
        {
        }

        public void SelectExample(AvailableExample example)
        {
            GetExampleLink(example).Click();
        }

        public ILink GetExampleLink(AvailableExample example)
        {
            var menuItemXpath = string.Format(TmpExampleLoc, example.GetDescription());
            return ElementFactory.GetLink(By.XPath(menuItemXpath), example.ToString());
        }

        public string GetSubTitleViaJs()
        {
            return LblSubTitle.JsActions.GetElementText();
        }

        public string GetSubTitleLocatorViaJs()
        {
            return LblSubTitle.JsActions.GetXPath();
        }

        public Point GetSubTitleCoordinatesViaJs()
        {
            return LblSubTitle.JsActions.GetViewPortCoordinates();
        }
    }
}