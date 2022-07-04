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

        public RelativeBy Below(By by)
        {
            this.by = RelativeSeleniumBy.WithLocator(this.by).Below(by);
            return new RelativeBy(this.by);
        }

        public RelativeBy Below(WebElement webElement)
        {
            by = RelativeSeleniumBy.WithLocator(by).Below(webElement);
            return new RelativeBy(by);
        }

        public RelativeBy Below(IElement element)
        {
            by = RelativeSeleniumBy.WithLocator(by).Below(element.Locator);
            return new RelativeBy(by);
        }

        public RelativeBy Left(By by)
        {
            this.by = RelativeSeleniumBy.WithLocator(this.by).LeftOf(by);
            return new RelativeBy(this.by);
        }

        public RelativeBy Left(WebElement webElement)
        {
            by = RelativeSeleniumBy.WithLocator(by).LeftOf(webElement);
            return new RelativeBy(by);
        }

        public RelativeBy Left(IElement element)
        {
            by = RelativeSeleniumBy.WithLocator(by).LeftOf(element.Locator);
            return new RelativeBy(by);
        }

        public RelativeBy Right(By by)
        {
            this.by = RelativeSeleniumBy.WithLocator(this.by).RightOf(by);
            return new RelativeBy(this.by);
        }

        public RelativeBy Right(WebElement webElement)
        {
            by = RelativeSeleniumBy.WithLocator(by).RightOf(webElement);
            return new RelativeBy(by);
        }

        public RelativeBy Right(IElement element)
        {
            by = RelativeSeleniumBy.WithLocator(by).RightOf(element.Locator);
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
