using Aquality.Selenium.Browsers;

namespace Aquality.Selenium.Tests.Integration
{
    internal class BrowserWindowsTests : BrowserTabsTests
    {
        protected override IBrowserWindowNavigation Tabs => AqualityServices.Browser.Windows();
    }
}
