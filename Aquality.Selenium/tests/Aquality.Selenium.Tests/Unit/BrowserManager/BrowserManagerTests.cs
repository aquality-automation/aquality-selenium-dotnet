using Aquality.Selenium.Browsers;
using NUnit.Framework;
using System;

namespace Aquality.Selenium.Tests.Unit.JavaScripts
{
    [Parallelizable(ParallelScope.All)]
    public class BrowserManagerTests
    {
        [Test]
        public void Should_BeAbleGetBrowser()
        {
            Assert.Throws<NotImplementedException>(() => BrowserManager.Browser.WaitForPageToLoad());
        }
    }
}
