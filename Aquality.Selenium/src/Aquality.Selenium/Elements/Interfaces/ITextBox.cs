namespace Aquality.Selenium.Elements.Interfaces
{
    public interface ITextBox : IElement
    {
        string Value { get; }

        void Type(string value, bool secret = false);

        void ClearAndType(string value, bool secret = false);

        void Submit();
    }
}
