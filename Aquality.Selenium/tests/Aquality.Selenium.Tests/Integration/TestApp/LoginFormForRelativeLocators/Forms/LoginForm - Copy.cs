using AngleSharp.Dom;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;


namespace Aquality.Selenium.Tests.Integration.TestApp.LoginFormForRelativeLocators.Forms
{
    internal class LoginForm1 : Form
    {
        private const string BaseUrl =  "C:\\a1qa\\GitHub_repos\\aquality-selenium-dotnet\\Aquality.Selenium\\tests\\Aquality.Selenium.Tests\\bin\\Debug\\net6.0\\Resources\\LoginFormForRelativeLocators.html";
        public LoginForm1() : base(By.XPath("//h1[contains(text(),'Student Login Form')]"), "Login form")
        {
        }

        public string Url => BaseUrl;

        public void Open()
        {
            AqualityServices.Browser.GoTo($"file://{BaseUrl}");
            AqualityServices.Browser.WaitForPageToLoad();
            AqualityServices.Browser.Maximize();
        }
        // public static readonly string LocatorCommentTextBox = "//textarea[@id='your-message']"; <h1> Student Login Form </h1>

        private static string ElementNameText(string idName, string typeOfElement) => $"[{idName}] {typeOfElement}" ;

        //private readonly string LabelText = "label";
        private readonly string TextBoxText = "text box";
        private readonly string ButtonText = "button";

        public static readonly string IdLocatorUserNameTextBox = "name";                            //"//input[@id='name']";
        public static readonly string IdLocatorStudentSurnameTextBox = "surname";
        public static readonly string IdLocatorPasswordTextBox = "password";
        public static readonly string IdLocatorUniversityTextBox = "university";
        public static readonly string IdLocatorFacultyTextBox = "faculty";
        public static readonly string IdLocatorSpecializationTextBox = "specialization";
        public static readonly string IdLocatorUniversityAddressTextBox = "university-address";     // address of the university
        public static readonly string IdLocatorHomeAddressTextBox = "home-address";
        public static readonly string IdLocatorPhoneNumberTextBox = "phone-number";


        public static readonly string IdLocatorLoginButton = "submit-btn";
        public static readonly string IdLocatorCancelButton = "cancel-btn";

        //public ILabel FormHeaderLabel => ElementFactory.GetLabel(By.Id(IdLocatorUserNameTextBox), ElementNameText(IdLocatorUserNameTextBox, LabelText));

        public ITextBox NameTextBox => ElementFactory.GetTextBox(By.Id(IdLocatorUserNameTextBox), ElementNameText(IdLocatorUserNameTextBox, TextBoxText));
        public ITextBox SurnameTextBox => ElementFactory.GetTextBox(By.Id(IdLocatorStudentSurnameTextBox), ElementNameText(IdLocatorStudentSurnameTextBox, TextBoxText));
        public ITextBox PasswordTextBox => ElementFactory.GetTextBox(By.Id(IdLocatorPasswordTextBox), ElementNameText(IdLocatorPasswordTextBox, TextBoxText));
        public ITextBox UniversityTextBox => ElementFactory.GetTextBox(By.Id(IdLocatorUniversityTextBox), ElementNameText(IdLocatorUniversityTextBox, TextBoxText));
        public ITextBox FacultyTextBox => ElementFactory.GetTextBox(By.Id(IdLocatorFacultyTextBox), ElementNameText(IdLocatorFacultyTextBox, TextBoxText));
        public ITextBox SpecializationTextBox => ElementFactory.GetTextBox(By.Id(IdLocatorSpecializationTextBox), ElementNameText(IdLocatorSpecializationTextBox, TextBoxText));
        public ITextBox UniversityAddressTextBox => ElementFactory.GetTextBox(By.Id(IdLocatorUniversityAddressTextBox), ElementNameText(IdLocatorUniversityAddressTextBox, TextBoxText));
        public ITextBox HomeAddressTextBox => ElementFactory.GetTextBox(By.Id(IdLocatorHomeAddressTextBox), ElementNameText(IdLocatorHomeAddressTextBox, TextBoxText));
        public ITextBox PhoneNumberTextBox => ElementFactory.GetTextBox(By.Id(IdLocatorPhoneNumberTextBox), ElementNameText(IdLocatorPhoneNumberTextBox, TextBoxText));

        public IButton LoginButton => ElementFactory.GetButton(By.Id(IdLocatorLoginButton), ElementNameText(IdLocatorPhoneNumberTextBox, ButtonText));
        public IButton CancelButton => ElementFactory.GetButton(By.Id(IdLocatorCancelButton), ElementNameText(IdLocatorPhoneNumberTextBox, ButtonText));
       
        /*
        public ICheckBox PrivacyCheckBox => ElementFactory.GetCheckBox(By.XPath("//input[@name='privacy']/following-sibling::span[1]"), "Privacy");
        public IButton SendButton => ElementFactory.GetButton(By.XPath("//div[contains(@class,'contactsForm__submit')]//button"), "Send a message");
        public ILabel EmailAlertLabel => ElementFactory.GetLabel(By.XPath("//div[contains(@class,'error')]//input[@id='your-email']"), "Email validating message");
        public ILabel TitleLabel => FormElement.FindChildElement<ILabel>(By.XPath("//h2[contains(@class,'blockTitle')]"), "Title");
        public ILabel TermsLabel => FormElement.FindChildElement<ILabel>(By.XPath("//label[contains(@class, 'checkbox')]"), "Terms");
        */
    }

    /*
     internal abstract class TheA1qaForm : Form
    {
        private const string BaseUrl = "https://www.a1qa.com/";

        protected TheA1qaForm(By locator, string name) : base(locator, name)
        {
        }

        protected abstract string UrlPart { get; }

        public string Url => BaseUrl + UrlPart;

        public void Open()
        {
            AqualityServices.Browser.GoTo(Url);
            AqualityServices.Browser.WaitForPageToLoad();
            AqualityServices.Browser.Maximize();
        }
    }
     */

    /*
     internal class ContactUsForm : TheA1qaForm
    {
        public ContactUsForm() : base(By.XPath("//h3[contains(text(),'Data Tables')]"), "Data Tables form")
        {
        }
        protected override string UrlPart => "contacts/";

        public static readonly string ElementNameNameTextBox = "Name text box";
        public static readonly string ElementNameCompanyTextBox = "Company text box";
        public static readonly string ElementNameEmailTextBox = "Email text box";
        public static readonly string ElementNamePhoneTextBox = "Phone text box";
        public static readonly string ElementNameCommentTextBox = "Comment text box";

        public static readonly string LocatorNameTextBox = "//input[@id='your-name']";
        public static readonly string LocatorCompanyTextBox = "//input[@id='your-company']";
        public static readonly string LocatorEmailTextBox = "//input[@id='your-email']";
        public static readonly string LocatorPhoneTextBox = "//input[@id='your-phone']";
        public static readonly string LocatorCommentTextBox = "//textarea[@id='your-message']";

        public ITextBox NameTextBox => ElementFactory.GetTextBox(By.XPath(LocatorNameTextBox), ElementNameNameTextBox);
        public ITextBox CompanyTextBox => ElementFactory.GetTextBox(By.XPath(LocatorCompanyTextBox), ElementNameCompanyTextBox);
        public ITextBox EmailTextBox => ElementFactory.GetTextBox(By.XPath(LocatorEmailTextBox), ElementNameEmailTextBox);
        public ITextBox PhoneTextBox => ElementFactory.GetTextBox(By.XPath(LocatorPhoneTextBox), ElementNamePhoneTextBox);
        public ITextBox CommentTextBox => ElementFactory.GetTextBox(By.XPath(LocatorCommentTextBox), ElementNameCommentTextBox);

        public ICheckBox PrivacyCheckBox => ElementFactory.GetCheckBox(By.XPath("//input[@name='privacy']/following-sibling::span[1]"), "Privacy");
        public IButton SendButton => ElementFactory.GetButton(By.XPath("//div[contains(@class,'contactsForm__submit')]//button"), "Send a message");
        public ILabel EmailAlertLabel => ElementFactory.GetLabel(By.XPath("//div[contains(@class,'error')]//input[@id='your-email']"), "Email validating message");
        public ILabel TitleLabel => FormElement.FindChildElement<ILabel>(By.XPath("//h2[contains(@class,'blockTitle')]"), "Title");
        public ILabel TermsLabel => FormElement.FindChildElement<ILabel>(By.XPath("//label[contains(@class, 'checkbox')]"), "Terms");

        //private ICheckBox TermsCheckBox => FormElement.FindChildElement<ICheckBox>(By.XPath("//input[@type='checkbox']"), "Terms", null, ElementState.ExistsInAnyState);
    }
     */
}
