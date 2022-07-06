using OpenQA.Selenium;
using RelativeSeleniumBy = OpenQA.Selenium.RelativeBy;
using CoreElement = Aquality.Selenium.Core.Elements.Element;
using Aquality.Selenium.Core.Elements.Interfaces;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aquality.Selenium.Locators
{
    public class RelativeBy : By
    {
        private By by;
        private List<Function> functions = new List<Function>();

        private const string ABOVE = "Above";
        private const string BELOW = "Below";
        private const string LEFT = "LeftOf";
        private const string RIGHT = "RightOf";
        private const string NEAR = "Near";

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
            functions.Add(new Function(ABOVE, new[] { by }));
            return this;
            //this.by = RelativeSeleniumBy.WithLocator(this.by).Above(by);
            //return new RelativeBy(this.by);
        }

        public RelativeBy Above(WebElement webElement)
        {
            functions.Add(new Function(ABOVE, new[] { webElement }));
            return this;
            //    by = RelativeSeleniumBy.WithLocator(by).Above(webElement);
            //    return new RelativeBy(by);
        }

        public RelativeBy Above(IElement element)
        {
            functions.Add(new Function(ABOVE, new[] { element.Locator }));
            return this;
            //  by = RelativeSeleniumBy.WithLocator(by).Above(element.Locator);
            //   return new RelativeBy(by);
        }

        public RelativeBy Below(By by)
        {
            functions.Add(new Function(BELOW, new[] { by }));
            return this;
            // this.by = RelativeSeleniumBy.WithLocator(this.by).Below(by);
            // return new RelativeBy(this.by);
        }

        public RelativeBy Below(WebElement webElement)
        {
            functions.Add(new Function(BELOW, new[] { webElement }));
            return this;
            // by = RelativeSeleniumBy.WithLocator(by).Below(webElement);
            // return new RelativeBy(by);
        }

        public RelativeBy Below(IElement element)
        {
            functions.Add(new Function(BELOW, new[] { element.Locator }));
            return this;
            //  by = RelativeSeleniumBy.WithLocator(by).Below(element.Locator);
            // return new RelativeBy(by);
        }

        public RelativeBy Left(By by)
        {
            functions.Add(new Function(LEFT, new[] { by }));
            return this;
            // this.by = RelativeSeleniumBy.WithLocator(this.by).LeftOf(by);
            //  return new RelativeBy(this.by);
        }

        public RelativeBy Left(WebElement webElement)
        {
            functions.Add(new Function(LEFT, new[] { webElement }));
            return this;
            // by = RelativeSeleniumBy.WithLocator(by).LeftOf(webElement);
            // return new RelativeBy(by);
        }

        public RelativeBy Left(IElement element)
        {
            functions.Add(new Function(LEFT, new[] { element.Locator }));
            return this;
            //  by = RelativeSeleniumBy.WithLocator(by).LeftOf(element.Locator);
            // return new RelativeBy(by);
        }

        public RelativeBy Right(By by)
        {
            functions.Add(new Function(RIGHT, new[] { by }));
            return this;
            // this.by = RelativeSeleniumBy.WithLocator(this.by).RightOf(by);
            // return new RelativeBy(this.by);
        }

        public RelativeBy Right(WebElement webElement)
        {
            functions.Add(new Function(RIGHT, new[] { webElement }));
            return this;
            // by = RelativeSeleniumBy.WithLocator(by).RightOf(webElement);
            //  return new RelativeBy(by);
        }

        public RelativeBy Right(IElement element)
        {
            functions.Add(new Function(RIGHT, new[] { element.Locator }));
            return this;
            // by = RelativeSeleniumBy.WithLocator(by).RightOf(element.Locator);
            // return new RelativeBy(by);
        }


        public override IWebElement FindElement(ISearchContext context)
        {
            return FindElements(context).First();
           // Console.WriteLine(by.ToString());
           // return ((RelativeSeleniumBy)by).FindElement(context);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            
            RelativeSeleniumBy formedBy = RelativeSeleniumBy.WithLocator(by);
            functions.ForEach(x =>
            {
                var firstArgumentType = x.Arguments.First().GetType();
                switch (x.Name)
                {

                    case ABOVE:
                        if (firstArgumentType == typeof(WebElement))
                        {
                            formedBy = formedBy.Above((WebElement)x.Arguments.First());
                        }

                        if (firstArgumentType == typeof(By))
                        {
                            formedBy = formedBy.Above((By)x.Arguments.First());
                        }

                        if (firstArgumentType == typeof(IElement))
                        {
                            formedBy = formedBy.Above(((IElement)x.Arguments.First()).Locator);
                        }

                        break;

                    case BELOW:

                        if (firstArgumentType == typeof(WebElement))
                        {
                            formedBy = formedBy.Below((WebElement)x.Arguments.First());
                        }

                        if (firstArgumentType == typeof(By))
                        {
                            formedBy = formedBy.Below((By)x.Arguments.First());
                        }

                        if (firstArgumentType == typeof(IElement))
                        {
                            formedBy = formedBy.Below(((IElement)x.Arguments.First()).Locator);
                        }
                        break;

                    case LEFT:

                        if (firstArgumentType == typeof(WebElement))
                        {
                            formedBy = formedBy.LeftOf((WebElement)x.Arguments.First());
                        }

                        if (firstArgumentType == typeof(By))
                        {
                            formedBy = formedBy.LeftOf((By)x.Arguments.First());
                        }

                        if (firstArgumentType == typeof(IElement))
                        {
                            formedBy = formedBy.LeftOf(((IElement)x.Arguments.First()).Locator);
                        }

                        break;

                    case RIGHT:

                        if (firstArgumentType == typeof(WebElement))
                        {
                            formedBy = formedBy.RightOf((WebElement)x.Arguments.First());
                        }

                        if (firstArgumentType == typeof(By))
                        {
                            formedBy = formedBy.RightOf((By)x.Arguments.First());
                        }

                        if (firstArgumentType == typeof(IElement))
                        {
                            formedBy = formedBy.RightOf(((IElement)x.Arguments.First()).Locator);
                        }

                        break;

                }
                // Console.WriteLine(typeof(RelativeSeleniumBy).GetMethods().Where(y=>y.Name==x.Name).First().GetParameters().First().ParameterType.Name);
                // formedBy = (RelativeSeleniumBy)typeof(RelativeSeleniumBy).GetMethod(x.Name).Invoke(formedBy, x.Arguments);
                //  (RelativeSeleniumBy)typeof(RelativeSeleniumBy).GetMethods().Where(x=>x.Attributes.)

            //    Console.WriteLine("Parameters");
           //     typeof(RelativeSeleniumBy).GetMethods().Where(y => (y.Name == x.Name)).ToList()[2].GetParameters().Select(z => z.ParameterType).ToList().ForEach(c => Console.WriteLine(c));
            //    Console.WriteLine("Arguments");
           //     x.Arguments.Select(t => t.GetType()).ToList().ForEach(z => Console.WriteLine(z));


            //    Console.WriteLine(typeof(RelativeSeleniumBy).GetMethods().Where(y => (y.Name == x.Name) && (y.GetParameters().Select(z => z.ParameterType).SequenceEqual(x.Arguments.Select(t => t.GetType())))).First().Name);

            });
            return context.FindElements(formedBy);
            // return context.FindElements(by);
            //  return ((RelativeSeleniumBy)by).FindElements(context);
        }


    }
}
