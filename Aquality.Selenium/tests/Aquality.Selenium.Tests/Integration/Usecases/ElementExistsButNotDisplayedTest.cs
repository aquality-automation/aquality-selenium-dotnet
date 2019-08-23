using Aquality.Selenium.Browsers;
using Aquality.Selenium.Elements;
using Aquality.Selenium.Tests.Integration.TestApp;
using Aquality.Selenium.Tests.Integration.TestApp.AutomationPractice.Forms;
using Aquality.Selenium.Waitings;
using NUnit.Framework;

namespace Aquality.Selenium.Tests.Integration.Usecases
{
    internal class ElementExistsButNotDisplayedTest : UITest
    {
        [SetUp]
        public void BeforeTest()
        {
            BrowserManager.Browser.GoTo(Constants.UrlAutomationPractice);
        }

        [Test]
        public void Should_BePossibleTo_WaitForElement_WhichExistsButNotDisplayed()
        {
            var button = new SliderForm().GetAddToCartBtn(ElementState.ExistsInAnyState);
            Assert.IsTrue(ConditionalWait.WaitFor(() => button.State.IsExist && !button.State.IsDisplayed));
        }
    }
}
