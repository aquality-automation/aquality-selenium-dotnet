using OpenQA.Selenium;
using Aquality.Selenium.Core.Elements.Interfaces;

namespace Aquality.Selenium.Locators
{
    internal interface IRelativeAquality
    {
        RelativeAqualityBy Above(By by);
        RelativeAqualityBy Below(By by);
        RelativeAqualityBy Left(By by);
        RelativeAqualityBy Right(By by);
        RelativeAqualityBy Near(By by);
        RelativeAqualityBy Near(By by, int atMostDistanceInPixels);

        RelativeAqualityBy Above(IElement element);
        RelativeAqualityBy Below(IElement element);
        RelativeAqualityBy Left(IElement element);
        RelativeAqualityBy Right(IElement elementy);
        RelativeAqualityBy Near(IElement element);
        RelativeAqualityBy Near(IElement element, int atMostDistanceInPixels);

        RelativeAqualityBy Above(WebElement webElement);
        RelativeAqualityBy Below(WebElement webElement);
        RelativeAqualityBy Left(WebElement webElement);
        RelativeAqualityBy Right(WebElement webElement);
        RelativeAqualityBy Near(WebElement webElement);
        RelativeAqualityBy Near(WebElement webElement, int atMostDistanceInPixels);
    }
}
