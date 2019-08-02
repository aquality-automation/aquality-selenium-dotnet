using System;
using System.IO;
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
        SetValue,
        GetViewPortCoordinates
    }

    public static class JavaScriptExtensions
    {
        private const string JavaScriptResourcePath = "Resources.JavaScripts";
        private const string JavaScriptFileExtension = "js";

        public static string GetScript(this JavaScript javaScript)
        {
            return javaScript.GetResourcePath().GetScript();
        }

        public static string GetScript(this string embeddedResourcePath)
        {
            var assembly = Assembly.GetCallingAssembly();
            var resourcePath = $"{assembly.GetName().Name}.{embeddedResourcePath}";
            using (var stream = assembly.GetManifestResourceStream(resourcePath))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException(
                        $"Assembly {assembly.FullName} doesn't contain JavaScript at path {resourcePath} as EmbeddedResource.");
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static string GetResourcePath(this JavaScript javaScript)
        {
            return $"{JavaScriptResourcePath}.{javaScript}.{JavaScriptFileExtension}";
        }
    }
}
