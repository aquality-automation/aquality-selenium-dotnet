using OpenQA.Selenium;

namespace Aquality.Selenium.Elements.Interfaces
{
    public interface IParent
    {
        T FindChildElement<T>(By childLocator, ElementState state = ElementState.Displayed) where T : IElement;
        
        T FindChildElement<T>(By childLocator, ElementSupplier supplier, ElementState state = ElementState.Displayed) where T : IElement;
    }
}
