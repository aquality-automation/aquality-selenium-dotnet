using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;

namespace Aquality.Selenium.Elements
{
    /// <summary>
    /// Delegate that defines constructor of element.
    /// </summary>
    /// <typeparam name="T">Type of element that has to implement IElement interface</typeparam>
    /// <param name="locator">Element locator</param>
    /// <param name="name">Element name</param>
    /// <param name="state">Element state</param>
    /// <returns>Element instance of type T</returns>
    public delegate T ElementSupplier<T>(By locator, string name, ElementState state) where T : IElement;
}
