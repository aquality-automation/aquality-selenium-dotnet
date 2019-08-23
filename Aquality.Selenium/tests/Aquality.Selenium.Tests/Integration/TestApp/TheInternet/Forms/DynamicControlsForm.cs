using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class DynamicControlsForm : TheInternetForm
    {
        public ITextBox TextInputTextBox => ElementFactory.GetTextBox(By.XPath("//input[@type='text']"), "Text input");
        public IButton ChangeInputStateButton => ElementFactory.GetButton(By.XPath("//form[@id='input-example']//button"), "Change input state");
        public ICheckBox ExampleCheckbox => ElementFactory.GetCheckBox(By.XPath("//input[@type='checkbox']"), "Example checkbox");
        public IButton RemoveAddExampleButton => ElementFactory.GetButton(By.XPath("//form[@id='checkbox-example']//button"), "Remove\\Add example checkbox");
        public ILabel LoadingLabel => ElementFactory.GetLabel(By.Id("loading"), "Loading");

        public DynamicControlsForm() : base(By.Id("content"), "Dynamic Controls")
        {
        }

        protected override string UrlPart => "dynamic_controls";
    }
}
