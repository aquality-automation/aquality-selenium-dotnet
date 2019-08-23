using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Localization;
using OpenQA.Selenium;

namespace Aquality.Selenium.Elements
{
    /// <summary>
    /// Defines TextBox UI element.
    /// </summary>
    public class TextBox : Element, ITextBox
    {
        private const string SecretMask = "*********";

        protected internal TextBox(By locator, string name, ElementState state) : base(locator, name, state)
        {
        }

        protected override string ElementType => LocalizationManager.Instance.GetLocalizedMessage("loc.text.field");

        public string Value => GetAttribute(Attributes.Value);

        public void Type(string value, bool secret = false)
        {
            LogElementAction("loc.text.typing", secret ? SecretMask : value);
            JsActions.HighlightElement();
            DoWithRetry(() => GetElement().SendKeys(value));
        }

        public void ClearAndType(string value, bool secret = false)
        {
            LogElementAction("loc.text.clearing");
            LogElementAction("loc.text.typing", secret ? SecretMask : value);
            JsActions.HighlightElement();
            DoWithRetry(() =>
            {
                GetElement().Clear();
                GetElement().SendKeys(value);                
            });
        }

        public void Submit()
        {
            DoWithRetry(() => GetElement().Submit());
        }

        public new void Focus()
        {
            DoWithRetry(() => GetElement().SendKeys(string.Empty));
        }
    }
}
