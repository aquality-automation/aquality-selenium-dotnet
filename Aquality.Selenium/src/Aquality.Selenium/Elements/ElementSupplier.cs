using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Elements
{
    public delegate T ElementSupplier<T>(By locator, string name, ElementState state) where T : IElement;
}
