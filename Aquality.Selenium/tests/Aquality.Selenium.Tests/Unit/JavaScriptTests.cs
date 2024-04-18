using System;
using System.Linq;
using Aquality.Selenium.Browsers;
using NUnit.Framework;

namespace Aquality.Selenium.Tests.Unit
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    internal class JavaScriptTests
    {
        private static readonly JavaScript[] JavaScripts = Enum.GetValues(typeof(JavaScript)) as JavaScript[];

        [TestCaseSource("JavaScripts")]
        public void Should_GetJavaScript(JavaScript script)
        {
            Assert.That(script.GetScript(), Is.Not.Empty, $"Failed to get javascript {script}");
        }
        
        [Test]
        public void Should_BeUniqueJavaScripts()
        {
            Assert.That(
                JavaScripts.GroupBy(script => script.GetScript()).Where(group => group.Count() > 1).Select(group => string.Join(" and ", group)), 
                Is.Empty,
                "some duplicates where found among JavaScripts");
        }
    }
}
