using OpenQA.Selenium;

namespace Aquality.Selenium.Elements.Interfaces
{
    public interface IParent
    {
        T FindChildElement<T>(By childLocator, ElementState state = ElementState.Displayed) where T : IElement;
        
        T FindChildElement<T>(By childLocator, ElementSupplier<T> supplier, ElementState state = ElementState.Displayed) where T : IElement;
    }
}
