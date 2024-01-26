using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.a1qa.Forms
{
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
}
