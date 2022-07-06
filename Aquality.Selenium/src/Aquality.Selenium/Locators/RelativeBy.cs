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
        }

        public RelativeBy Above(WebElement webElement)
        {
            functions.Add(new Function(ABOVE, new[] { webElement }));
            return this;
        }

        public RelativeBy Above(IElement element)
        {
            functions.Add(new Function(ABOVE, new[] { element.Locator }));
            return this;
        }

        public RelativeBy Below(By by)
        {
            functions.Add(new Function(BELOW, new[] { by }));
            return this;
        }

        public RelativeBy Below(WebElement webElement)
        {
            functions.Add(new Function(BELOW, new[] { webElement }));
            return this;
        }

        public RelativeBy Below(IElement element)
        {
            functions.Add(new Function(BELOW, new[] { element.Locator }));
            return this;
        }

        public RelativeBy Left(By by)
        {
            functions.Add(new Function(LEFT, new[] { by }));
            return this;
        }

        public RelativeBy Left(WebElement webElement)
        {
            functions.Add(new Function(LEFT, new[] { webElement }));
            return this;
        }

        public RelativeBy Left(IElement element)
        {
            functions.Add(new Function(LEFT, new[] { element.Locator }));
            return this;
        }

        public RelativeBy Right(By by)
        {
            functions.Add(new Function(RIGHT, new[] { by }));
            return this;
        }

        public RelativeBy Right(WebElement webElement)
        {
            functions.Add(new Function(RIGHT, new[] { webElement }));
            return this;
        }

        public RelativeBy Right(IElement element)
        {
            functions.Add(new Function(RIGHT, new[] { element.Locator }));
            return this;
        }

        public RelativeBy Near(By by)
        {
            functions.Add(new Function(NEAR, new[] { by }));
            return this;
        }

        public RelativeBy Near(WebElement webElement)
        {
            functions.Add(new Function(NEAR, new[] { webElement }));
            return this;
        }

        public RelativeBy Near(IElement element)
        {
            functions.Add(new Function(NEAR, new[] { element.Locator }));
            return this;
        }


        public override IWebElement FindElement(ISearchContext context)
        {
            return FindElements(context).First();
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {

            RelativeSeleniumBy formedBy = RelativeSeleniumBy.WithLocator(by);
            functions.ForEach(function =>
            {
                var firstArgumentType = function.Arguments.First().GetType();
                var countArguments = function.Arguments.Length;

                switch (function.Name)
                {

                    case ABOVE:
                        if (firstArgumentType == typeof(WebElement))
                        {
                            formedBy = formedBy.Above((WebElement)function.Arguments.First());
                        }

                        if (firstArgumentType == typeof(By))
                        {
                            formedBy = formedBy.Above((By)function.Arguments.First());
                        }

                        if (firstArgumentType == typeof(IElement))
                        {
                            formedBy = formedBy.Above(((IElement)function.Arguments.First()).Locator);
                        }

                        break;

                    case BELOW:

                        if (firstArgumentType == typeof(WebElement))
                        {
                            formedBy = formedBy.Below((WebElement)function.Arguments.First());
                        }

                        if (firstArgumentType == typeof(By))
                        {
                            formedBy = formedBy.Below((By)function.Arguments.First());
                        }

                        if (firstArgumentType == typeof(IElement))
                        {
                            formedBy = formedBy.Below(((IElement)function.Arguments.First()).Locator);
                        }
                        break;

                    case LEFT:

                        if (firstArgumentType == typeof(WebElement))
                        {
                            formedBy = formedBy.LeftOf((WebElement)function.Arguments.First());
                        }

                        if (firstArgumentType == typeof(By))
                        {
                            formedBy = formedBy.LeftOf((By)function.Arguments.First());
                        }

                        if (firstArgumentType == typeof(IElement))
                        {
                            formedBy = formedBy.LeftOf(((IElement)function.Arguments.First()).Locator);
                        }

                        break;

                    case RIGHT:

                        if (firstArgumentType == typeof(WebElement))
                        {
                            formedBy = formedBy.RightOf((WebElement)function.Arguments.First());
                        }

                        if (firstArgumentType == typeof(By))
                        {
                            formedBy = formedBy.RightOf((By)function.Arguments.First());
                        }

                        if (firstArgumentType == typeof(IElement))
                        {
                            formedBy = formedBy.RightOf(((IElement)function.Arguments.First()).Locator);
                        }

                        break;

                    case NEAR:
                        if (countArguments == 1)
                        {
                            if (firstArgumentType == typeof(WebElement))
                            {
                                formedBy = formedBy.Near((WebElement)function.Arguments.First());
                            }

                            if (firstArgumentType == typeof(By))
                            {
                                formedBy = formedBy.Near((By)function.Arguments.First());
                            }

                            if (firstArgumentType == typeof(IElement))
                            {
                                formedBy = formedBy.Near(((IElement)function.Arguments.First()).Locator);
                            }
                        }
                        else
                        {
                            if (firstArgumentType == typeof(WebElement))
                            {
                                formedBy = formedBy.Near((WebElement)function.Arguments.First(), (int)function.Arguments[1]);
                            }

                            if (firstArgumentType == typeof(By))
                            {
                                formedBy = formedBy.Near((By)function.Arguments.First(), (int)function.Arguments[1]);
                            }

                            if (firstArgumentType == typeof(IElement))
                            {
                                formedBy = formedBy.Near(((IElement)function.Arguments.First()).Locator, (int)function.Arguments[1]);
                            }
                        }
                        break;

                    default:
                        throw new ArgumentException($"There is no realisation for {function.Name} function");






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
