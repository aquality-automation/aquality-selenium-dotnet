using OpenQA.Selenium;
using System;

namespace Aquality.Selenium.Elements
{
    internal class DesiredState
    {
        public DesiredState(Func<IWebElement, bool> elementStateCondition, string stateName)
        {
            ElementStateCondition = elementStateCondition;
            StateName = stateName;
        }

        public Func<IWebElement, bool> ElementStateCondition { get; }

        public bool IsCatchingTimeoutException { get; set; }

        public bool IsThrowingNoSuchElementException { get; set; }

        public string StateName { get; }
    }
}
