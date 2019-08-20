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

        public Point FormPointInViewPort
        {
            get
            {
                return ElementFactory.GetLabel(Locator, Name).JsActions.GetViewPortCoordinates();
            }
        }
    }
}
