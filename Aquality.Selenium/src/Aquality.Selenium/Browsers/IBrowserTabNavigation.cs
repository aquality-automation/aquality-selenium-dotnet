using System.Collections.Generic;

namespace Aquality.Selenium.Browsers
{
    /// <summary>
    /// Provides functionality to work with browser tab navigation.  
    /// </summary>
    public interface IBrowserTabNavigation
    {
        /// <summary>
        /// Switches to tab.
        /// </summary>
        /// <param name="name">Tab name.</param>
        /// <param name="closeCurrent">Close current tab if true and leave it otherwise.</param>
        void SwitchToTab(string name, bool closeCurrent = false);

        /// <summary>
        /// Switches to tab.
        /// </summary>
        /// <param name="index">Tab index.</param>
        /// <param name="closeCurrent">Close current tab if true and leave it otherwise.</param>
        void SwitchToTab(int index, bool closeCurrent = false);

        /// <summary>
        /// Switches to new tab.
        /// </summary>
        /// <param name="closeCurrent">Close current tab if true and leave it otherwise.</param>
        void SwitchToNewTab(bool closeCurrent = false);

        /// <summary>
        /// Gets opened tab names.
        /// </summary>
        /// <returns>List of tab names.</returns>
        IList<string> GetTabNames();

        /// <summary>
        /// Gets current tab name.
        /// </summary>
        /// <returns>Tab name.</returns>
        string GetTabName();

        /// <summary>
        /// Closes curent tab.
        /// </summary>
        void CloseTab();

        /// <summary>
        /// Opens new tab.
        /// </summary>
        /// <param name="switchToNew">Switches to new tab if true and stays at current otherwise.</param>
        void OpenNewTab(bool switchToNew = true);
    }
}
