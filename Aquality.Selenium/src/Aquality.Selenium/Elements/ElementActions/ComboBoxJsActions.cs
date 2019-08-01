using System;
using System.Collections.Generic;
using Aquality.Selenium.Elements.Interfaces;

namespace Aquality.Selenium.Elements.ElementActions
{
    public class ComboBoxJsActions : JsActions
    {
        public ComboBoxJsActions(IElement element, string elementType) : base(element, elementType)
        {
        }

        public IList<string> GetValues()
        {
            throw new NotImplementedException();
        }

        public string GetSelectedText()
        {
            throw new NotImplementedException();
        }

        public string SelectValueByText(string text)
        {
            throw new NotImplementedException();
        }
    }
}
