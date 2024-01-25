using System;
using System.Collections.Generic;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Provides functionality to work with browser tab navigation.  
    /// </summary>
    public interface IBrowserTabNavigation
    {
        /// <summary>
        /// Gets current tab handle.
        /// </summary>
        /// <returns>Current tab handle.</returns>
        string CurrentTabHandle { get; }

        /// <summary>
        /// Gets opened tab handles.
        /// </summary>
        /// <returns>List of tab handles.</returns>
        IList<string> TabHandles { get; }

        /// <summary>
        /// Switches to tab.
        /// </summary>
        /// <param name="tabHandle">Tab handle.</param>
        /// <param name="closeCurrent">Close current tab if true and leave it otherwise.</param>
        void SwitchToTab(string tabHandle, bool closeCurrent = false);

        /// <summary>
        /// Switches to tab.
        /// </summary>
        /// <param name="index">Tab index.</param>
        /// <param name="closeCurrent">Close current tab if true and leave it otherwise.</param>
        void SwitchToTab(int index, bool closeCurrent = false);

        /// <summary>
        /// Switches to the last tab.
        /// </summary>
        /// <param name="closeCurrent">Close current tab if true and leave it otherwise.</param>
        void SwitchToLastTab(bool closeCurrent = false);

        /// <summary>
        /// Closes current tab.
        /// </summary>
        void CloseTab();

        /// <summary>
        /// Opens new tab.
        /// </summary>
        /// <param name="switchToNew">Switches to new tab if true and stays at current otherwise.</param>
        void OpenNewTab(bool switchToNew = true);

        /// <summary>
        /// Opens new tab using JS function.
        /// </summary>
        /// <param name="switchToNew">Switches to new tab if true and stays at current otherwise.</param>
        void OpenNewTabViaJs(bool switchToNew = true);

        /// <summary>
        /// Navigates to desired url in new tab.
        /// </summary>
        /// <param name="url">String representation of URL.</param>
        void OpenInNewTab(string url);

        /// <summary>
        /// Navigates to desired url in new tab.
        /// </summary>
        /// <param name="url">target URL.</param>
        void OpenInNewTab(Uri url);

        /// <summary>
        /// Navigates to desired url in new tab using JS function.
        /// </summary>
        /// <param name="url">String representation of URL.</param>
        void OpenInNewTabViaJs(string url);
    }
}
