using OpenQA.Selenium;

namespace Aquality.Selenium.Locators
{
    internal interface IRelativeWebElement
    {
        RelativeBy Above(WebElement by);
        RelativeBy Below(WebElement by);
        RelativeBy Left(WebElement by);
        RelativeBy Right(WebElement by);
    }
}
