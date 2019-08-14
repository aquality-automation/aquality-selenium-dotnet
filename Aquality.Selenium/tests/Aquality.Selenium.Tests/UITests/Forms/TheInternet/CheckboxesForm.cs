﻿using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.UITests.Forms.AutomationPractice
{
    public class CheckboxesForm : Form
    {
        private const string FormName = "Checkboxes";
        private static readonly By FormLocator = By.Id("checkboxes");
        private ICheckBox CbxFirst => ElementFactory.GetCheckBox(By.XPath("//input[1]"), "First checkBox");
        private ICheckBox CbxSecond => ElementFactory.GetCheckBox(By.XPath("//input[2]"), "Second checkBox");

        public CheckboxesForm() : base(FormLocator, FormName)
        {
        }

        public bool GetFirstStateViaJs()
        {
            return CbxFirst.JsActions.GetState();
        }

        public bool GetSecondStateViaJs()
        {
            return CbxSecond.JsActions.GetState();
        }
    }
}
