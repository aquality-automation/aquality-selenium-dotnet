using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;
using System.Linq;

namespace Aquality.Selenium.Tests.Integration.TestApp.W3Schools.Forms
{
    internal class SelectMultipleForm : Form
    {
        private readonly IMultiChoiceBox carsMultiChoiceBox = ElementFactory.GetMultiChoiceBox(By.Id("cars"), "Cars");
        private readonly IButton submitButton = ElementFactory.GetButton(By.CssSelector("input[type='submit']"), "Submit");
        private readonly ITextBox resultTextBox = ElementFactory.GetTextBox(By.CssSelector(".w3-large"), "Result");
        private readonly IButton acceptCookiesButton = ElementFactory.GetButton(By.Id("accept-choices"), "Accept cookies");

        public SelectMultipleForm() : base(By.Id("iframe"), "Select Multiple")
        {
        }

        public void AcceptCookies()
        {
            if (acceptCookiesButton.State.WaitForDisplayed())
            {
                acceptCookiesButton.Click();
                acceptCookiesButton.State.WaitForNotDisplayed();
            }
        }

        public static void SwitchToResultFrame()
        {
            AqualityServices.Browser.Driver.SwitchTo().Frame("iframeResult");
        }

        public void Submit()
        {
            submitButton.ClickAndWait();
        }

        public IList<string> ValuesFromResult
            => Regex.Matches(resultTextBox.Text.Trim(), ".*?=(.*?)(&|$)").Select(match => match.Groups[1].Value).ToList();

        public IList<string> AllTexts => carsMultiChoiceBox.Texts;

        public IList<string> SelectedTexts => carsMultiChoiceBox.SelectedTexts;

        public IList<string> SelectedValues => carsMultiChoiceBox.SelectedValues;

        public void SelectAll() => carsMultiChoiceBox.SelectAll();

        public static void Open()
        {
            AqualityServices.Browser.GoTo($"{Constants.UrlW3Schools}?filename=tryhtml_select_multiple");
            AqualityServices.Browser.WaitForPageToLoad();
        }

        public IList<string> DeselectByValue(IList<string> valuesToDeselect)
        {
            return DeselectBy(carsMultiChoiceBox.DeselectByValue, valuesToDeselect);
        }

        public IList<string> DeselectByContainingValue(IList<string> valuesToDeselect)
        {
            return DeselectBy(carsMultiChoiceBox.DeselectByContainingValue, valuesToDeselect);
        }

        public IList<string> DeselectByText(IList<string> textToDeselect)
        {
            return DeselectBy(carsMultiChoiceBox.DeselectByText, textToDeselect);
        }

        public IList<string> DeselectByContainingText(IList<string> textToDeselect)
        {
            return DeselectBy(carsMultiChoiceBox.DeselectByContainingText, textToDeselect);
        }

        public IList<string> DeselectByIndex(IList<int> indicesToDeselect)
        {
            indicesToDeselect.ToList().ForEach(carsMultiChoiceBox.DeselectByIndex);
            return carsMultiChoiceBox.Values.Where((value, index) => !indicesToDeselect.Contains(index)).ToList();
        }

        public void DeselectAll()
        {
            carsMultiChoiceBox.DeselectAll();
        }

        private List<string> DeselectBy(Action<string> deselectFunc, IList<string> valuesToDeselect)
        {
            valuesToDeselect.ToList().ForEach(value => deselectFunc.Invoke(value));
            return carsMultiChoiceBox.Values.Where(value => !valuesToDeselect.Any(deselected => value.Contains(deselected.ToLower()))).ToList();
        }
    }
}
