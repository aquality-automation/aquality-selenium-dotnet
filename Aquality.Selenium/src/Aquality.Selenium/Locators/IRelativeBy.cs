using OpenQA.Selenium;

namespace Aquality.Selenium.Locators
{
    internal interface IRelativeBy
    {
        RelativeBy Above(By by);
        RelativeBy Below(By by);
        RelativeBy Left(By by);
        RelativeBy Right(By by);
    }
}
