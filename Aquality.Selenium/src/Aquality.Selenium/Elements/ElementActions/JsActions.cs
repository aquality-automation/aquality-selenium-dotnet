using Aquality.Selenium.Elements.Interfaces;
using System;

namespace Aquality.Selenium.Elements.ElementActions
{
    public class JsActions
    {
        private readonly IElement element;
        private readonly string elementType;

        public JsActions(IElement element, string elementType)
        {
            this.element = element;
            this.elementType = elementType;
        }

        public void Click()
        {
            throw new NotImplementedException();
        }

        public void ClickAndWait()
        {
            throw new NotImplementedException();
        }

        public void HighlightElement()
        {
            throw new NotImplementedException();
        }

        public void ScrollIntoView()
        {
            throw new NotImplementedException();
        }

        public void ScrollBy(int x, int y)
        {
            throw new NotImplementedException();
        }

        public void ScrollToTheCenter()
        {
            throw new NotImplementedException();
        }

        public void SetValue(string value)
        {
            throw new NotImplementedException();
        }

        public void SetFocus()
        {
            throw new NotImplementedException();
        }

        public bool IsElementOnScreen()
        {
            throw new NotImplementedException();
        }

        public string GetElementText()
        {
            throw new NotImplementedException();
        }

        public void HoverMouse()
        {
            throw new NotImplementedException();
        }

        public string GetXPath()
        {
            throw new NotImplementedException();
        }
    }
}
