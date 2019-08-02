using Aquality.Selenium.Utilities;
using System.Reflection;

namespace Aquality.Selenium.Browsers
{
    public enum JavaScript
    {
        AutoAcceptAlerts,
        BorderElement,
        ClickElement,
        ElementIsOnScreen,
        GetCheckBoxState,
        GetComboBoxText,
        GetComboBoxValues,
        GetElementByXPath,
        GetElementText,
        GetElementXPath,
        GetTextFirstChild,
        IsPageLoaded,
        MouseHover,
        ScrollBy,
        ScrollToBottom,
        ScrollToElement,
        ScrollToElementCenter,
        ScrollToTop,
        ScrollWindowBy,
        SelectComboBoxValueByText,
        SetFocus,
        SetInnerHTML,
        SetValue
    }

    public static class JavaScriptExtensions
    {
        private const string JavaScriptResourcePath = "Resources.JavaScripts";
        private const string JavaScriptFileExtension = "js";

        public static string GetScript(this JavaScript javaScript)
        {
            return javaScript.GetResourcePath().GetScript(Assembly.GetExecutingAssembly());
        }

        internal static string GetScript(this string embeddedResourcePath, Assembly assembly)
        {
            return FileReader.GetTextFromEmbeddedResource(embeddedResourcePath, assembly);
        }

        private static string GetResourcePath(this JavaScript javaScript)
        {
            return $"{JavaScriptResourcePath}.{javaScript}.{JavaScriptFileExtension}";
        }
    }
}
