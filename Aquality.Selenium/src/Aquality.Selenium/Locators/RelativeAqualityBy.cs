using OpenQA.Selenium;
using RelativeSeleniumBy = OpenQA.Selenium.RelativeBy;
using Aquality.Selenium.Core.Elements.Interfaces;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Aquality.Selenium.Locators.Enums;

namespace Aquality.Selenium.Locators
{
    public class RelativeAqualityBy : RelativeSeleniumBy, IRelativeAquality
    {
        private readonly By by;
        private readonly List<KeyValuePair<RelativeAqualityLocators, object[]>> relativePairs = new List<KeyValuePair<RelativeAqualityLocators, object[]>>();

        private const int DEFAULT_VALUE_IN_PIXELS_FOR_THE_NEAR_LOCATOR = 50;

        private RelativeAqualityBy(By by)
        {
            this.by = by;
        }

        public static RelativeAqualityBy WithLocator(By by)
        {
            return new RelativeAqualityBy(by);
        }

        public RelativeAqualityBy Above(By by)
        {
            relativePairs.Add(new KeyValuePair<RelativeAqualityLocators, object[]>(RelativeAqualityLocators.Above, new[] { by }));
            return this;
        }

        public RelativeAqualityBy Above(WebElement webElement)
        {
            relativePairs.Add(new KeyValuePair<RelativeAqualityLocators, object[]>(RelativeAqualityLocators.Above, new[] { webElement }));
            return this;
        }

        public RelativeAqualityBy Above(IElement element)
        {
            relativePairs.Add(new KeyValuePair<RelativeAqualityLocators, object[]>(RelativeAqualityLocators.Above, new[] { element.Locator }));
            return this;
        }

        public RelativeAqualityBy Below(By by)
        {
            relativePairs.Add(new KeyValuePair<RelativeAqualityLocators, object[]>(RelativeAqualityLocators.Below, new[] { by }));
            return this;
        }

        public RelativeAqualityBy Below(WebElement webElement)
        {
            relativePairs.Add(new KeyValuePair<RelativeAqualityLocators, object[]>(RelativeAqualityLocators.Below, new[] { webElement }));
            return this;
        }

        public RelativeAqualityBy Below(IElement element)
        {
            relativePairs.Add(new KeyValuePair<RelativeAqualityLocators, object[]>(RelativeAqualityLocators.Below, new[] { element.Locator }));
            return this;
        }

        public RelativeAqualityBy Left(By by)
        {
            relativePairs.Add(new KeyValuePair<RelativeAqualityLocators, object[]>(RelativeAqualityLocators.Left, new[] { by }));
            return this;
        }

        public RelativeAqualityBy Left(WebElement webElement)
        {
            relativePairs.Add(new KeyValuePair<RelativeAqualityLocators, object[]>(RelativeAqualityLocators.Left, new[] { webElement }));
            return this;
        }

        public RelativeAqualityBy Left(IElement element)
        {
            relativePairs.Add(new KeyValuePair<RelativeAqualityLocators, object[]>(RelativeAqualityLocators.Left, new[] { element.Locator }));
            return this;
        }

        public RelativeAqualityBy Right(By by)
        {
            relativePairs.Add(new KeyValuePair<RelativeAqualityLocators, object[]>(RelativeAqualityLocators.Right, new[] { by }));
            return this;
        }

        public RelativeAqualityBy Right(WebElement webElement)
        {
            relativePairs.Add(new KeyValuePair<RelativeAqualityLocators, object[]>(RelativeAqualityLocators.Right, new[] { webElement }));
            return this;
        }

        public RelativeAqualityBy Right(IElement element)
        {
            relativePairs.Add(new KeyValuePair<RelativeAqualityLocators, object[]>(RelativeAqualityLocators.Right, new[] { element.Locator }));
            return this;
        }

        public RelativeAqualityBy Near(By by)
        {
           return Near(by, DEFAULT_VALUE_IN_PIXELS_FOR_THE_NEAR_LOCATOR);
        }

        public RelativeAqualityBy Near(WebElement webElement)
        {
            return Near(webElement, DEFAULT_VALUE_IN_PIXELS_FOR_THE_NEAR_LOCATOR);
        }

        public RelativeAqualityBy Near(IElement element)
        {
            return Near(element, DEFAULT_VALUE_IN_PIXELS_FOR_THE_NEAR_LOCATOR);
        }

        public RelativeAqualityBy Near(By by, int atMostDistanceInPixels)
        {
            relativePairs.Add(new KeyValuePair<RelativeAqualityLocators, object[]>(RelativeAqualityLocators.Near, new object[] { by, atMostDistanceInPixels }));
            return this;
        }

        public RelativeAqualityBy Near(WebElement webElement, int atMostDistanceInPixels)
        {
            relativePairs.Add(new KeyValuePair<RelativeAqualityLocators, object[]>(RelativeAqualityLocators.Near, new object[] { webElement, atMostDistanceInPixels }));
            return this;
        }

        public RelativeAqualityBy Near(IElement element, int atMostDistanceInPixels)
        {
            relativePairs.Add(new KeyValuePair<RelativeAqualityLocators, object[]>(RelativeAqualityLocators.Near, new object[] { element.Locator, atMostDistanceInPixels }));
            return this;
        }
        //TODO!
        public override IWebElement FindElement(ISearchContext context)
        {
            return FindElements(context).First();
        }
        //TODO!
        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            RelativeSeleniumBy formedBy = RelativeSeleniumBy.WithLocator(by);

            relativePairs.ForEach(relativePair =>
            {
                var firstArgument = relativePair.Value[0];
                switch (relativePair.Key)
                {
                    case RelativeAqualityLocators.Above:
                        formedBy = GetRelativeWithAbove(formedBy, firstArgument);
                        break;

                    case RelativeAqualityLocators.Below:
                        formedBy = GetRelativeWithBelow(formedBy, firstArgument);
                        break;

                    case RelativeAqualityLocators.Left:
                        formedBy = GetRelativeWithLeft(formedBy, firstArgument);
                        break;

                    case RelativeAqualityLocators.Right:
                        formedBy = GetRelativeWithRight(formedBy, firstArgument);
                        break;

                    case RelativeAqualityLocators.Near:
                        formedBy = GetRelativeWitNear(formedBy, relativePair.Value[0], (int)relativePair.Value[1]);
                        break;

                    default:
                        throw new ArgumentException($"There is no realisation for [{relativePair.Key}] function");
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

        private RelativeSeleniumBy GetRelativeWitNear(RelativeSeleniumBy formedBy, object locator, int valueInPixelsForTheNearLocator)
        {
            var typeArgument = locator.GetType();
            if (typeArgument == typeof(WebElement))
            {  
                return formedBy.Near((WebElement)locator, valueInPixelsForTheNearLocator); 
            }
            if (typeArgument == typeof(By))
            {
                 return formedBy.Near((By)locator, valueInPixelsForTheNearLocator);
            }
            if (typeArgument == typeof(IElement))
            {
                return formedBy.Near(((IElement)locator).Locator, valueInPixelsForTheNearLocator);
            }

            throw new ArgumentException(ErrorMessageForType(typeArgument));
        }

        private string ErrorMessageForType(Type typeArgument) => $"There is no realisation for [{typeArgument}] type";
    }
}
