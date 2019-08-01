﻿using System.Collections.Generic;
using Aquality.Selenium.Elements.Actions;

namespace Aquality.Selenium.Elements.Interfaces
{
    public interface IComboBox : IElement
    {
        new ComboBoxJsActions JsActions { get; }

        string SelectedText { get; }

        string SelectedTextByJs { get; }

        IList<string> Values { get; }

        void SelectByIndex(int index);

        void SelectByText(string text);

        void SelectByValue(string value);

        void SelectByContainingText(string partialText);

        void SelectByContainingValue(string partialValue);
    }
}
