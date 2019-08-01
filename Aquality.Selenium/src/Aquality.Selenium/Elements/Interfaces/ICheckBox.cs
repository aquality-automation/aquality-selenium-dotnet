using Aquality.Selenium.Elements.Actions;

namespace Aquality.Selenium.Elements.Interfaces
{
    public interface ICheckBox : IElement
    {
        new CheckBoxJsActions JsActions { get; }

        bool IsChecked { get; }

        void Check();

        void Uncheck();

        void Toggle();
    }
}
