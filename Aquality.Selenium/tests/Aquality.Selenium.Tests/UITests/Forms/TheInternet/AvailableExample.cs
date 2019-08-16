using System.ComponentModel;

namespace Aquality.Selenium.Tests.UITests.Forms.TheInternet
{
    public enum AvailableExample
    {
        [Description("checkboxes")]
        Checkboxes,
        [Description("dropdown")]
        Dropdown,
        [Description("hovers")]
        Hovers,
        [Description("key_presses")]
        KeyPresses,
        [Description("infinite_scroll")]
        InfiniteScroll,
        [Description("add_remove_elements")]
        AddRemoveElements,
        [Description("context_menu")]
        ContextMenu
    }
}
