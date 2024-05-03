using System;
using System.Collections.Generic;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Provides functionality to work with browser tab navigation.  
    /// </summary>
    public interface IBrowserTabNavigation : IBrowserWindowNavigation
    {
        /// <summary>
        /// Gets current tab handle.
        /// Obsolete, use <see cref="IBrowserWindowNavigation.CurrentHandle"/> instead.
        /// </summary>
        /// <returns>Current tab handle.</returns>
        [Obsolete("Use CurrentHandle instead")]
        string CurrentTabHandle { get; }

        /// <summary>
        /// Gets opened tab handles.
        /// Obsolete, use <see cref="IBrowserWindowNavigation.Handles"/> instead.
        /// </summary>
        /// <returns>List of tab handles.</returns>
        [Obsolete("Use Handles instead")]
        IList<string> TabHandles { get; }

        /// <summary>
        /// Switches to tab.
        /// Obsolete, use <see cref="IBrowserWindowNavigation.SwitchTo(string, bool)"/> instead.
        /// </summary>
        /// <param name="tabHandle">Tab handle.</param>
        /// <param name="closeCurrent">Close current tab if true and leave it otherwise.</param>
        [Obsolete("Use SwitchTo instead")]
        void SwitchToTab(string tabHandle, bool closeCurrent = false);

        /// <summary>
        /// Switches to tab.
        /// Obsolete, use <see cref="IBrowserWindowNavigation.SwitchTo(int, bool)"/> instead.
        /// </summary>
        /// <param name="index">Tab index.</param>
        /// <param name="closeCurrent">Close current tab if true and leave it otherwise.</param>
        [Obsolete("Use SwitchTo instead")]
        void SwitchToTab(int index, bool closeCurrent = false);

        /// <summary>
        /// Switches to the last tab.
        /// Obsolete, use <see cref="IBrowserWindowNavigation.SwitchToLast(bool)"/> instead.
        /// </summary>
        /// <param name="closeCurrent">Close current tab if true and leave it otherwise.</param>
        [Obsolete("Use SwitchToLast instead")]
        void SwitchToLastTab(bool closeCurrent = false);

        /// <summary>
        /// Closes current tab.
        /// Obsolete, use <see cref="IBrowserWindowNavigation.Close()"/> instead.
        /// </summary>
        [Obsolete("Use Close instead")]
        void CloseTab();

        /// <summary>
        /// Opens new tab.
        /// Obsolete, use <see cref="IBrowserWindowNavigation.OpenNew(bool)"/> instead.
        /// </summary>
        /// <param name="switchToNew">Switches to new tab if true and stays at current otherwise.</param>
        [Obsolete("Use OpenNew instead")]
        void OpenNewTab(bool switchToNew = true);

        /// <summary>
        /// Opens new tab using JS function.
        /// Obsolete, use <see cref="IBrowserWindowNavigation.OpenNewViaJs(bool)"/> instead.
        /// </summary>
        /// <param name="switchToNew">Switches to new tab if true and stays at current otherwise.</param>
        [Obsolete("Use OpenNewViaJs instead")]
        void OpenNewTabViaJs(bool switchToNew = true);

        /// <summary>
        /// Navigates to desired url in new tab.
        /// Obsolete, use <see cref="IBrowserWindowNavigation.OpenInNew(string)"/> instead.
        /// </summary>
        /// <param name="url">String representation of URL.</param>
        [Obsolete("Use OpenInNew instead")]
        void OpenInNewTab(string url);

        /// <summary>
        /// Navigates to desired url in new tab.
        /// Obsolete, use <see cref="IBrowserWindowNavigation.OpenInNew(Uri)"/> instead.
        /// </summary>
        /// <param name="url">target URL.</param>
        [Obsolete("Use OpenInNew instead")]
        void OpenInNewTab(Uri url);

        /// <summary>
        /// Navigates to desired url in new tab using JS function.
        /// Obsolete, use <see cref="IBrowserWindowNavigation.OpenInNewViaJs(string)"/> instead.
        /// </summary>
        /// <param name="url">String representation of URL.</param>
        [Obsolete("Use OpenInNewViaJs instead")]
        void OpenInNewTabViaJs(string url);
    }
}
