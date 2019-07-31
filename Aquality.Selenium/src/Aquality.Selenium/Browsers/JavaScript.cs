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
            var resourcePath = GetResourcePath(javaScript, assembly.GetName().Name);
            using (var stream = assembly.GetManifestResourceStream(resourcePath))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException(
                        $"Assembly {assembly.FullName} doesn't contain JavaScript {javaScript} at path {resourcePath} as EmbeddedResource.");
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static string GetResourcePath(this JavaScript javaScript, string assemblyName)
        {
            return $"{assemblyName}.{JavaScriptResourcePath}.{javaScript}.{JavaScriptFileExtension}";
        }
    }
}
