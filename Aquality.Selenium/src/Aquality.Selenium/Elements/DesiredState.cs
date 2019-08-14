using OpenQA.Selenium;
using System;

namespace Aquality.Selenium.Elements
{
    internal class DesiredState
    {
        public DesiredState(Func<IWebElement, bool> elementStateCondition, string message)
        {
            ElementStateCondition = elementStateCondition;
            Message = message;
        }

        public Func<IWebElement, bool> ElementStateCondition { get; }

        public bool IsCatchingTimeoutException { get; set; }

        public bool IsThrowingNoSuchElementException { get; set; }

        public string Message { get; set; }

        public void Apply()
        {
            if (IsCatchingTimeoutException)
            {
                if (IsThrowingNoSuchElementException)
                {
                    throw new NoSuchElementException(Message);
                }
            }
            else
            {
                throw new WebDriverTimeoutException(Message);
            }
        }
    }
}
