using System;

namespace Aquality.Selenium.Forms
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class PageInfoAttribute : Attribute
    {
        /// <summary>
        /// Page name
        /// </summary>
        public string PageName { get; set; } = default;

        /// <summary>
        /// Page xpath anchor
        /// </summary>
        public string Xpath { get; set; } = default;

        /// <summary>
        /// Page id anchor getter
        /// </summary>
        public string Id { get; set; } = default;

        /// <summary>
        /// Page css anchor
        /// </summary>
        public string Css { get; set; } = default;
    }
}
