using Aquality.Selenium.Core.Utilities;
using System.Reflection;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Predefined JS scripts.
    /// </summary>
    public enum JavaScript
    {
        AutoAcceptAlerts,
        BorderElement,
        ClickElement,
        ElementIsOnScreen,
        GetCheckBoxState,
        GetComboBoxSelectedText,
        GetComboBoxTexts,
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
        SetAttribute,
        SetFocus,
        SetInnerHTML,
        SetValue,
        GetViewPortCoordinates,
        OpenNewTab,
        OpenInNewTab,
        ExpandShadowRoot
    }

    /// <summary>
    /// Extensions for <see cref="JavaScript"/> enum.
    /// </summary>
    public static class JavaScriptExtensions
    {
        private const string JavaScriptResourcePath = "Resources.JavaScripts";
        private const string JavaScriptFileExtension = "js";

        /// <summary>
        /// Gets string representation of JS script.
        /// </summary>
        /// <param name="javaScript">Desired JS script name.</param>
        /// <returns>String representation of script.</returns>
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
