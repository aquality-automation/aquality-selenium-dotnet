using Aquality.Selenium.Core.Elements;
using Aquality.Selenium.Core.Elements.Interfaces;
using Aquality.Selenium.Core.Waitings;
using OpenQA.Selenium;
using CoreElementStateProvider = Aquality.Selenium.Core.Elements.ElementStateProvider;

namespace Aquality.Selenium.Elements
{
    public class ElementStateProvider : CoreElementStateProvider
    {
        public ElementStateProvider(By elementLocator, IConditionalWait conditionalWait, IElementFinder elementFinder, LogElementState logElementState) 
            : base(elementLocator, conditionalWait, elementFinder, logElementState)
        {
        }

        protected override bool IsElementEnabled(IWebElement element)
        {
            return element.Enabled && !element.GetAttribute(Attributes.Class).Contains(PopularClassNames.Disabled);
        }
    }
}
