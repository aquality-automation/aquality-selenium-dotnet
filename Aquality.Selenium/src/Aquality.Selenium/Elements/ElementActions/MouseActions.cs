using Aquality.Selenium.Elements.Interfaces;
using System;

namespace Aquality.Selenium.Elements.ElementActions
{
    public class MouseActions
    {
        private readonly IElement element;

        public MouseActions(IElement element)
        {
            this.element = element;
        }

        public void Click()
        {
            throw new NotImplementedException();
        }

        public void DoubleClick()
        {
            throw new NotImplementedException();
        }

        public void MoveMouseToElement()
        {
            throw new NotImplementedException();
        }

        public void MouseUp()
        {
            throw new NotImplementedException();
        }
    }
}
