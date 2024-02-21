using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using System;
using System.IO;

namespace Aquality.Selenium.Tests.Integration.TestApp.LoginFormForRelativeLocators.Forms
{
    internal class LoginForm : Form
    {
        private static string BaseUrl => Path.Combine($"{AppDomain.CurrentDomain.BaseDirectory}", "Resources", "LoginFormForRelativeLocators.html");

        public LoginForm() : base(By.XPath("//h1[contains(text(),'Student Login Form')]"), "Login form")
        {
        }

        public void Open()
        {
            AqualityServices.Browser.GoTo($"file://{BaseUrl}");
            AqualityServices.Browser.WaitForPageToLoad();
            AqualityServices.Browser.Maximize();
        }

        public static readonly string IdLocatorUserNameTextBox = "//input[@id='user-name']";
        public static readonly string IdLocatorSurnameTextBox = "//input[@id='user-name']";
        public static readonly string IdLocatorPasswordTextBox = "//input[@id='password']";

        public static readonly string IdLocatorUniversityTextBox = "//input[@id='university']";
        public static readonly string IdLocatorPhoneTextBox = "//input[@id='phone']";
        public static readonly string IdLocatorAdressTextBox = "//input[@id='adress']";

        public static readonly string IdLocatorFacultyTextBox = "//input[@id='faculty']";
        public static readonly string IdLocatorLoginButton = "//button[@id='submit-btn']";
        public static readonly string IdLocatorCancelButton = "//button[@id='cancel-btn']";

        public static readonly string ElementNameUserNameTextBox = "User Name Text Box";
        public static readonly string ElementNameSurnameTextBox = "Surname Name Text Box";
        public static readonly string ElementNamePasswordTextBox = "Password Text Box";

        public static readonly string ElementNameUniversityTextBox = "University Text Box";
        public static readonly string ElementNamePhoneTextBox = "Phone Text Box";
        public static readonly string ElementNameAdressTextBox = "Adress Text Box";

        public static readonly string ElementNameFacultyTextBox = "Faculty Text Box";
        public static readonly string ElementNameLoginButton = "Login Button";
        public static readonly string ElementNameCancelButton = "Cancel Button";

        public ITextBox UserNameTextBox => ElementFactory.GetTextBox(By.XPath(IdLocatorUserNameTextBox), ElementNameUserNameTextBox);
        public ITextBox SurnameTextBox => ElementFactory.GetTextBox(By.XPath(IdLocatorSurnameTextBox), ElementNameSurnameTextBox);
        public ITextBox PasswordTextBox => ElementFactory.GetTextBox(By.XPath(IdLocatorPasswordTextBox), ElementNamePasswordTextBox);
        public ITextBox UniversityTextBox => ElementFactory.GetTextBox(By.XPath(IdLocatorUniversityTextBox), ElementNameUniversityTextBox);
        public ITextBox PhoneTextBox => ElementFactory.GetTextBox(By.XPath(IdLocatorPhoneTextBox), ElementNamePhoneTextBox);
        public ITextBox AdressTextBox => ElementFactory.GetTextBox(By.XPath(IdLocatorAdressTextBox), ElementNameAdressTextBox);
        public ITextBox FacultyTextBox => ElementFactory.GetTextBox(By.XPath(IdLocatorFacultyTextBox), ElementNameFacultyTextBox);
        public IButton LoginButton => ElementFactory.GetButton(By.XPath(IdLocatorLoginButton), ElementNameLoginButton);
        public IButton CancelButton => ElementFactory.GetButton(By.XPath(IdLocatorCancelButton), ElementNameCancelButton);
    }
}
