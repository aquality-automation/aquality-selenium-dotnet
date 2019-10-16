﻿using System.Collections.Generic;
using System.Linq;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Configurations;
using Aquality.Selenium.Core.Waitings;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Tests.Integration.TestApp.TheInternet.Forms
{
    internal class InfiniteScrollForm : TheInternetForm
    {
        public InfiniteScrollForm() : base(By.XPath("//div[@id='content' and .//h3[contains(.,'Infinite Scroll')]]"), "Infinite Scroll")
        {
        }

        protected override string UrlPart => "infinite_scroll";

        public IList<ILabel> ExampleLabels => ElementFactory.FindElements(By.XPath("//div[contains(@class,'jscroll-added')]"), ElementFactory.GetLabel);

        public ILabel LastExampleLabel => ExampleLabels.Last();

        public ILabel Footer => ElementFactory.GetLabel(By.Id("page-footer"), "Footer");

        public void WaitForPageToLoad()
        {
            var examplesCount = ExampleLabels.Count;
            BrowserManager.GetRequiredService<ConditionalWait>().WaitFor(() => examplesCount != ExampleLabels.Count, BrowserManager.GetRequiredService<ITimeoutConfiguration>().Script);
        }
    }
}
