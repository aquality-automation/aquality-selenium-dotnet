using OpenQA.Selenium;
using System.Collections.Generic;

namespace Aquality.Selenium.Elements.Interfaces
{
    /// <summary>
    /// Interface responsible for elements creation
    /// </summary>
    public interface IElementFactory
    {
        IButton GetButton(By locator, string name, ElementState state = ElementState.Displayed);

        ICheckBox GetCheckBox(By locator, string name, ElementState state = ElementState.Displayed);

        IComboBox GetComboBox(By locator, string name, ElementState state = ElementState.Displayed);

        ILabel GetLabel(By locator, string name, ElementState state = ElementState.Displayed);

        ILink GetLink(By locator, string name, ElementState state = ElementState.Displayed);

        IRadioButton GetRadioButton(By locator, string name, ElementState state = ElementState.Displayed);

        ITextBox GetTextBox(By locator, string name, ElementState state = ElementState.Displayed);

        T GetCustomElement<T>(ElementSupplier<T> elementSupplier, By locator, string name, ElementState state) where T : IElement;

        T FindChildElement<T>(IElement parentElement, By childLocator, ElementSupplier<T> supplier = null, ElementState state = ElementState.Displayed) where T : IElement;

        IList<T> FindElements<T>(By locator, ElementSupplier<T> supplier = null, ElementsCount expectedCount = ElementsCount.MoreThenZero, ElementState state = ElementState.Displayed) where T : IElement;
    }
}
