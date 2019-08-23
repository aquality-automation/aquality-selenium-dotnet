using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class DynamicControlsForm : TheInternetForm
    {
        public ITextBox TxtTextInput => ElementFactory.GetTextBox(By.XPath("//input[@type='text']"), "Text input");
        public IButton BtnChangeInputState => ElementFactory.GetButton(By.XPath("//form[@id='input-example']//button"), "Change input state");
        public ICheckBox CbxCheckboxExample => ElementFactory.GetCheckBox(By.XPath("//input[@type='checkbox']"), "Checkbox example");
        public IButton BtnRemoveAddCheckbox => ElementFactory.GetButton(By.XPath("//form[@id='checkbox-example']//button"), "Remove\\Add checkbox");
        public ILabel LblLoading => ElementFactory.GetLabel(By.Id("loading"), "Loading");

        public DynamicControlsForm() : base(By.Id("content"), "Dynamic Controls")
        {
        }

        protected override string UrlPart => "dynamic_controls";
    }
}
