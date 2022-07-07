using Aquality.Selenium.Core.Elements.Interfaces;

namespace Aquality.Selenium.Locators
{
    internal interface IRelativeAqualityElement
    {
        RelativeBy Above(IElement by);
        RelativeBy Below(IElement by);
        RelativeBy Left(IElement by);
        RelativeBy Right(IElement by);
    }
}
