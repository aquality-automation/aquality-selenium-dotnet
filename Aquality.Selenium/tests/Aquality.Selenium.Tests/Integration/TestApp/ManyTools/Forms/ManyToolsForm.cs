using Aquality.Selenium.Browsers;
using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Core.Utilities;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.ManyTools.Forms
{
    internal abstract class ManyToolsForm<T> : Form where T : ManyToolsForm<T>
    {
        private const string BaseUrl = "https://manytools.org/";        

        protected ManyToolsForm(By locator, string name) : base(locator, name)
        {
        }

        private ILabel ValueLabel => FormElement.FindChildElement<ILabel>(By.XPath(".//code"), Name, state: ElementState.ExistsInAnyState);

        private ILabel ConsentDialog => ElementFactory.GetLabel(By.Id("cmpwrapper"), "Cookie consent dialog", ElementState.ExistsInAnyState);

        private IButton DeclineCookiesButton => ConsentDialog.FindElementInShadowRoot<IButton>(By.Id("cmpbntnotxt"), "Decline cookies");

        protected abstract string UrlPart { get; }

        public string Url => BaseUrl + UrlPart;

        public string Value => ValueLabel.GetText();

        public T Open()
        {
            AqualityServices.Get<IActionRetrier>().DoWithRetry(() =>
            {
                AqualityServices.Browser.GoTo(Url);
                State.WaitForDisplayed();
            }, [typeof(WebDriverTimeoutException)]);

            if (ConsentDialog.State.IsExist && DeclineCookiesButton.State.IsDisplayed)
            {
                DeclineCookiesButton.Click();
                ConsentDialog.State.WaitForNotDisplayed();
            }

            return (T)this;
        }
    }
}
