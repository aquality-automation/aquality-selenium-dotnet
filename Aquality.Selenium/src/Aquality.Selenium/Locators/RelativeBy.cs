using OpenQA.Selenium;
using RelativeSeleniumBy = OpenQA.Selenium.RelativeBy;
using CoreElement = Aquality.Selenium.Core.Elements.Element;
using Aquality.Selenium.Core.Elements.Interfaces;
using System.Collections.ObjectModel;
using System;

namespace Aquality.Selenium.Locators
{
    public class RelativeBy : By
    {
        private By by;

        private RelativeBy(By by)
        {
            this.by = by;
        }

        public static RelativeBy WithLocator(By by)
        {
            return new RelativeBy(by);
        }

        public RelativeBy Above(By by)
        {
            this.by = RelativeSeleniumBy.WithLocator(this.by).Above(by);
            return new RelativeBy(this.by);
        }

        public RelativeBy Above(WebElement webElement)
        {
            by = RelativeSeleniumBy.WithLocator(by).Above(webElement);
            return new RelativeBy(by);
        }

        public RelativeBy Above(IElement element)
        {
            by = RelativeSeleniumBy.WithLocator(by).Above(element.Locator);
            return new RelativeBy(by);
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            Console.WriteLine(by.ToString());
            return ((RelativeSeleniumBy)by).FindElement(context);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            return ((RelativeSeleniumBy)by).FindElements(context);
        }


    }
}
