using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;
using System;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Extensions for scripts pinned with <see cref="Browser.JavaScriptEngine"/>.
    /// </summary>
    public static class PinnedScriptExtensions
    {
        private static Browser Browser => AqualityServices.Browser;

        /// <summary>
        /// Executes pinned JS script.
        /// </summary>
        /// <param name="pinnedScript">Instance of script pinned with <see cref="Browser.JavaScriptEngine"/>.</param>
        /// <param name="arguments">Script arguments.</param>
        public static void ExecuteScript(this PinnedScript pinnedScript, params object[] arguments)
        {
            Browser.Driver.ExecuteScript(pinnedScript, arguments);
        }
        /// <summary>
        /// Executes pinned JS script against the element.
        /// </summary>
        /// <param name="pinnedScript">Instance of script pinned with <see cref="Browser.JavaScriptEngine"/>.</param>
        /// <param name="element">Instance of element created with <see cref="Form.ElementFactory"/>.</param>
        /// <param name="arguments">Script arguments.</param>
        public static void ExecuteScript(this PinnedScript pinnedScript, IElement element, params object[] arguments)
        {
            element.JsActions.ExecuteScript(pinnedScript, arguments);
        }

        /// <summary>
        /// Executes pinned JS script against the element and gets result value.
        /// </summary>
        /// <param name="pinnedScript">Instance of script pinned with <see cref="Browser.JavaScriptEngine"/>.</param>
        /// <param name="element">Instance of element created with <see cref="Form.ElementFactory"/>.</param>
        /// <param name="arguments">Script arguments.</param>
        /// <typeparam name="T">Type of return value.</typeparam>
        /// <returns>Script execution result.</returns>
        public static T ExecuteScript<T>(this PinnedScript pinnedScript, IElement element, params object[] arguments)
        {
            return element.JsActions.ExecuteScript<T>(pinnedScript, arguments);
        }

        /// <summary>
        /// Executes pinned JS script and gets result value.
        /// </summary>
        /// <param name="pinnedScript">Instance of script pinned with <see cref="Browser.JavaScriptEngine"/>.</param>
        /// <param name="arguments">Script arguments.</param>
        /// <typeparam name="T">Type of return value.</typeparam>
        /// <returns>Script execution result.</returns>
        public static T ExecuteScript<T>(this PinnedScript pinnedScript, params object[] arguments)
        {
            var value = Browser.Driver.ExecuteScript(pinnedScript, arguments);
            var result = default(T);
            Type type = typeof(T);
            if (value == null)
            {
                if (type.IsValueType && (Nullable.GetUnderlyingType(type) == null))
                {
                    throw new WebDriverException("Script returned null, but desired type is a value type");
                }
            }
            else if (!type.IsInstanceOfType(value))
            {
                throw new WebDriverException("Script returned a value, but the result could not be cast to the desired type");
            }
            else
            {
                result = (T)value;
            }

            return result;
        }
    }
}
