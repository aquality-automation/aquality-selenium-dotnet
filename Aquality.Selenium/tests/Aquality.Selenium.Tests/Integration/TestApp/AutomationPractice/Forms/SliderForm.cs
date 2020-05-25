using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using System.Drawing;

namespace Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Forms
{
    internal class SliderForm : Form
    {
        public SliderForm() : base(By.Id("slider_row"), "Slider")
        {
        }

        public IButton NextButton => ElementFactory.GetButton(By.XPath("//a[contains(.,'Next')]"), "Next");

        public Point FormPointInViewPort => ElementFactory.GetLabel(Locator, Name).JsActions.GetViewPortCoordinates();

        public IButton GetAddToCartBtn(ElementState elementState)
        {
            return ElementFactory.GetButton(By.XPath("//ul[@id='blockbestsellers']//li[last()]//a[contains(@class, 'add_to_cart')]"), "Add to cart", elementState);
        }

        public void ClickNextButton()
        {
            NextButton.ClickAndWait();
        }
    }
}
