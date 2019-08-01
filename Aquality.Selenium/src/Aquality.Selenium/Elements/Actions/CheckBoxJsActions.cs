using System;
using Aquality.Selenium.Elements.Interfaces;

namespace Aquality.Selenium.Elements.Actions
{
    public class CheckBoxJsActions : JsActions
    {
        public CheckBoxJsActions(IElement element, string elementType) : base(element, elementType)
        {
        }

        public bool GetState()
        {
            throw new NotImplementedException();
        }
    }
}
