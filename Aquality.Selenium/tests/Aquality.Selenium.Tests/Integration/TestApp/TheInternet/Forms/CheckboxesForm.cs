﻿using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class CheckBoxesForm : TheInternetForm
    {
        public CheckBoxesForm() : base(By.Id("checkboxes"), "CheckBoxes")
        {
        }

        public ICheckBox FirstChbx => ElementFactory.GetCheckBox(By.XPath("//input[1]"), "First checkBox");

        public ICheckBox SecondChbx => ElementFactory.GetCheckBox(By.XPath("//input[2]"), "Second checkBox");

        protected override string UrlPart => "checkboxes";
    }
}
