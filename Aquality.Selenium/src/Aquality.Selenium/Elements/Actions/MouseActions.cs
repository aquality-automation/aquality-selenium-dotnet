using System;
using Aquality.Selenium.Elements.Interfaces;

namespace Aquality.Selenium.Elements.Actions
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
