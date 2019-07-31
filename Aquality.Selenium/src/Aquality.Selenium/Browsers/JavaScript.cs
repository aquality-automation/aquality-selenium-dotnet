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
        SetValue
    }

    public static class JavaScriptExtensions
    {
        private const string JavaScriptResourcePath = "Resources.JavaScripts";
        private const string JavaScriptFileExtension = "js";
        
        public static string GetScript(this JavaScript javaScript)
        {
            var assembly = Assembly.GetExecutingAssembly();
            return javaScript.GetResourcePath().GetScript(assembly);
        }

        public static string GetScript(this string embeddedResourcePath, Assembly executingAssembly)
        {
            var resourcePath = $"{executingAssembly.GetName().Name}.{embeddedResourcePath}";
            using (var stream = executingAssembly.GetManifestResourceStream(resourcePath))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException(
                        $"Assembly {executingAssembly.FullName} doesn't contain JavaScript at path {resourcePath} as EmbeddedResource.");
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
