using System;
using System.Collections.Generic;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Provides functionality to work with browser tab/window navigation.  
    /// </summary>
    public interface IBrowserWindowNavigation
    {
        /// <summary>
        /// Gets current tab/window handle.
        /// </summary>
        /// <returns>Current tab/window handle.</returns>
        string CurrentHandle { get; }

        /// <summary>
        /// Gets opened tab/window handles.
        /// </summary>
        /// <returns>List of tab/window handles.</returns>
        IList<string> Handles { get; }

        /// <summary>
        /// Switches to tab/window.
        /// </summary>
        /// <param name="handle">Tab/window handle.</param>
        /// <param name="closeCurrent">Close current tab/window if true and leave it otherwise.</param>
        void SwitchTo(string handle, bool closeCurrent = false);

        /// <summary>
        /// Switches to tab/window.
        /// </summary>
        /// <param name="index">Tab/window index.</param>
        /// <param name="closeCurrent">Close current tab/window if true and leave it otherwise.</param>
        void SwitchTo(int index, bool closeCurrent = false);

        /// <summary>
        /// Switches to the last tab/window.
        /// </summary>
        /// <param name="closeCurrent">Close current tab/window if true and leave it otherwise.</param>
        void SwitchToLast(bool closeCurrent = false);

        /// <summary>
        /// Closes current tab/window.
        /// </summary>
        void Close();

        /// <summary>
        /// Opens new tab/window.
        /// </summary>
        /// <param name="switchToNew">Switches to new tab/window if true and stays at current otherwise.</param>
        void OpenNew(bool switchToNew = true);

        /// <summary>
        /// Opens new tab/window using JS function.
        /// </summary>
        /// <param name="switchToNew">Switches to new tab/window if true and stays at current otherwise.</param>
        void OpenNewViaJs(bool switchToNew = true);

        /// <summary>
        /// Navigates to desired url in new tab/window.
        /// </summary>
        /// <param name="url">String representation of URL.</param>
        void OpenInNew(string url);

        /// <summary>
        /// Navigates to desired url in new tab/window.
        /// </summary>
        /// <param name="url">target URL.</param>
        void OpenInNew(Uri url);

        /// <summary>
        /// Navigates to desired url in new tab/window using JS function.
        /// </summary>
        /// <param name="url">String representation of URL.</param>
        void OpenInNewViaJs(string url);
    }
}
