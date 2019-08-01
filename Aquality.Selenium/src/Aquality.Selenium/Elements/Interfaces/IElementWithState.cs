﻿namespace Aquality.Selenium.Elements.Interfaces
{
    public interface IElementWithState
    {
        IElementStateProvider State { get; }

        bool HasState(string className);
    }
}
