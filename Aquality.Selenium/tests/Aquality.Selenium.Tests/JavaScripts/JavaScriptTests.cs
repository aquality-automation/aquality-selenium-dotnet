using Aquality.Selenium.Browsers;
using NUnit.Framework;
using System;
using System.Linq;

namespace Aquality.Selenium.Tests.JavaScripts
{
    [Parallelizable(ParallelScope.All)]
    public class JavaScriptTests
    {
        private const string TestScriptName = "TestJavaScript.js";
        private const string TestScriptEmbeddedPath = "Resources." + TestScriptName;
        private const string TestScriptFilePath = "Resources/" + TestScriptName;
#pragma warning disable IDE0052 // Remove unread private members
        private static readonly JavaScript[] JavaScripts = Enum.GetValues(typeof(JavaScript)) as JavaScript[];
#pragma warning restore IDE0052 // Remove unread private members

        [TestCaseSource("JavaScripts")]
        public void Should_GetJavaScript(JavaScript script)
        {
            Assert.IsNotEmpty(script.GetScript(), $"Failed to get javascript {script}");
        }

        [Test]
        public void Should_GetCustomJavaScript_FromEmbeddedResourcePath()
        {
            Assert.IsNotEmpty(TestScriptEmbeddedPath.GetScript(), $"Failed to get javascript {TestScriptEmbeddedPath}");
        }

        [Test]
        public void Should_BeUniqueJavaScripts()
        {
            Assert.IsEmpty(
                JavaScripts.GroupBy(script => script.GetScript()).Where(group => group.Count() > 1).Select(group => string.Join(" and ", group)), 
                "some duplicates where found among JavaScripts");
        }
    }
}
