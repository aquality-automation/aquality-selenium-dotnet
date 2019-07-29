using System;

namespace Aquality.Selenium.Elements.Interfaces
{
    public interface IElementStateProvider
    {
        bool IsDisplayed { get; }

        bool IsExist { get; }

        TimeSpan DefaultTimeout { get; }

        bool WaitForDisplayed(TimeSpan? timeout = null);

        bool WaitForNotDisplayed(TimeSpan? timeout = null);

        bool WaitForExist(TimeSpan? timeout = null);

        bool WaitForNotExist(TimeSpan? timeout = null);
    }
}
