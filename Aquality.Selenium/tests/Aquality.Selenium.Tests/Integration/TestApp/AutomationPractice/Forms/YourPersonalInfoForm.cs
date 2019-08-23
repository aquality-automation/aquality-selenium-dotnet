using Aquality.Selenium.Elements;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Forms
{
    internal class YourPersonalInfoForm : Form
    {
        public YourPersonalInfoForm() : base(By.XPath("//input[@name='id_gender']"), "Your Personal Info")
        {
        }

        private ITextBox FirstNameTextBox => ElementFactory.GetTextBox(By.Id("customer_firstname"), "First name");
        private IComboBox StateComboBox => ElementFactory.GetComboBox(By.Id("id_state"), "State", ElementState.ExistsInAnyState);
        private IComboBox SelectDayComboBox => ElementFactory.GetComboBox(By.Name("days"), "Days", ElementState.ExistsInAnyState);
        private ICheckBox NewsCheckBox => ElementFactory.GetCheckBox(By.Id("newsletter"), "newsletter", ElementState.ExistsInAnyState);
        private IList<ILabel> DaysLabels => ElementFactory.FindElements<ILabel>(By.XPath("//select[@id='days']/option"), state: ElementState.ExistsInAnyState, expectedCount: ElementsCount.MoreThenZero);
        
        public void SelectGender(Gender gender)
        {
            ElementFactory.GetRadioButton(By.Id($"id_gender{(int)gender}"), $"Gender {gender}", state: ElementState.ExistsInAnyState);
        }

        public void SetFirstName(string firstName)
        {
            FirstNameTextBox.Type(firstName);
        }

        public int GetNumberOfDays()
        {
            return DaysLabels.Count;
        }

        public void SelectState(string state)
        {
            StateComboBox.Click();
            StateComboBox.SelectByText(state);
        }

        public void SelectDay(int day)
        {
            SelectDayComboBox.Click();
            SelectDayComboBox.SelectByValue(day.ToString());
        }

        public void SetNewsCheckBox()
        {
            NewsCheckBox.JsActions.Check();
        }

        public bool IsNewsCheckBoxChecked()
        {
            return NewsCheckBox.IsChecked;
        }
    }

    internal enum Gender
    {
        Female = 0,
        Male = 1
    }
}
