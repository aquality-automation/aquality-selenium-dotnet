using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Drawing;

namespace Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Forms
{
    internal class SliderForm : Form
    {
        private readonly ILabel styleContainer = ElementFactory.GetLabel(By.Id("homeslider"), "Slider style container", ElementState.ExistsInAnyState);

        public SliderForm() : base(By.Id("slider_row"), "Slider")
        {
        }

        protected override IDictionary<string, IElement> ElementsForVisualization => ElementsInitializedAsDisplayed;

        public IButton NextButton => ElementFactory.GetButton(By.XPath("//a[contains(.,'Next')]"), "Next");

        public Point FormPointInViewPort => ElementFactory.GetLabel(Locator, Name).JsActions.GetViewPortCoordinates();

        public string Style => styleContainer.GetAttribute("style");

        public void WaitForSliding()
        {
            var style = string.Empty;
            ConditionalWait.WaitForTrue(() =>
            {
                if (style == Style)
                {
                    return true;
                }
                style = Style;
                return false;
            }, message: "Sliding should stop after a while");
        }

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
