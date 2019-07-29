namespace Aquality.Selenium.Elements.Interfaces
{
    public interface IElementWithState
    {
        IElementStateProvider State { get; }

        bool HasState(PopularClassNames className); // TODO: need more understandable name
    }
}
