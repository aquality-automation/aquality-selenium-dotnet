using OpenQA.Selenium;
using RelativeSeleniumBy = OpenQA.Selenium.RelativeBy;
using Aquality.Selenium.Core.Elements.Interfaces;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aquality.Selenium.Locators
{
    public class RelativeBy : By, IRelativeBy, IRelativeWebElement, IRelativeAqualityElement
    {
        private readonly By by;
        private readonly List<Function> functions = new List<Function>();

        private const string ABOVE = "Above";
        private const string BELOW = "Below";
        private const string LEFT = "LeftOf";
        private const string RIGHT = "RightOf";

        private RelativeBy() { }

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

        public override IWebElement FindElement(ISearchContext context)
        {
            return FindElements(context).First();
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            RelativeSeleniumBy formedBy = RelativeSeleniumBy.WithLocator(by);

            functions.ForEach(function =>
            {
                var firstArgument = function.Arguments.First();
                var firstArgumentType = function.Arguments.First().GetType();

                switch (function.Name)
                {
                    case ABOVE:

                        formedBy = GetRelativeWithAbove(formedBy, firstArgument);
                        break;

                    case BELOW:

                        formedBy = GetRelativeWithBelow(formedBy, firstArgument);
                        break;

                    case LEFT:

                        formedBy = GetRelativeWithLeft(formedBy, firstArgument);
                        break;

                    case RIGHT:

                        formedBy = GetRelativeWithRight(formedBy, firstArgument);
                        break;

                    default:
                        throw new ArgumentException($"There is no realisation for {function.Name} function");

                }
            });
            return context.FindElements(formedBy);
        }
        private RelativeSeleniumBy GetRelativeWithAbove(RelativeSeleniumBy formedBy, object savedArgument)
        {
            var typeArgument = savedArgument.GetType();

            if (typeArgument == typeof(WebElement))
            {
                return formedBy.Above((WebElement)savedArgument);
            }

            if (typeArgument == typeof(By))
            {
                return formedBy.Above((By)savedArgument);
            }

            if (typeArgument == typeof(IElement))
            {
                return formedBy.Above(((IElement)savedArgument).Locator);
            }

            throw new ArgumentException(ErrorMessageForType(typeArgument));
        }

        private RelativeSeleniumBy GetRelativeWithBelow(RelativeSeleniumBy formedBy, object savedArgument)
        {
            var typeArgument = savedArgument.GetType();

            if (typeArgument == typeof(WebElement))
            {
                return formedBy.Below((WebElement)savedArgument);
            }

            if (typeArgument == typeof(By))
            {
                return formedBy.Below((By)savedArgument);
            }

            if (typeArgument == typeof(IElement))
            {
                return formedBy.Below(((IElement)savedArgument).Locator);
            }

            throw new ArgumentException(ErrorMessageForType(typeArgument));
        }

        private RelativeSeleniumBy GetRelativeWithRight(RelativeSeleniumBy formedBy, object savedArgument)
        {
            var typeArgument = savedArgument.GetType();

            if (typeArgument == typeof(WebElement))
            {
                return formedBy.RightOf((WebElement)savedArgument);
            }

            if (typeArgument == typeof(By))
            {
                return formedBy.RightOf((By)savedArgument);
            }

            if (typeArgument == typeof(IElement))
            {
                return formedBy.RightOf(((IElement)savedArgument).Locator);
            }

            throw new ArgumentException(ErrorMessageForType(typeArgument));
        }

        private RelativeSeleniumBy GetRelativeWithLeft(RelativeSeleniumBy formedBy, object savedArgument)
        {
            var typeArgument = savedArgument.GetType();

            if (typeArgument == typeof(WebElement))
            {
                return formedBy.LeftOf((WebElement)savedArgument);
            }

            if (typeArgument == typeof(By))
            {
                return formedBy.LeftOf((By)savedArgument);
            }

            if (typeArgument == typeof(IElement))
            {
                return formedBy.LeftOf(((IElement)savedArgument).Locator);
            }

            throw new ArgumentException(ErrorMessageForType(typeArgument));
        }

        private string ErrorMessageForType(Type typeArgument) => $"There is no realisation for {typeArgument} type";
    }
}
