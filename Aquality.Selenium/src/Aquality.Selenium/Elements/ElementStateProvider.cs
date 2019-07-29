using Aquality.Selenium.Configurations;
using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System;

namespace Aquality.Selenium.Elements
{
    internal class ElementStateProvider : IElementStateProvider
    {
        private readonly By elementLocator;

        internal ElementStateProvider(By elementLocator)
        {
            this.elementLocator = elementLocator;
        }

        public bool IsDisplayed => throw new NotImplementedException();

        public bool IsExist => throw new NotImplementedException();

        public TimeSpan DefaultTimeout => Configuration.Instance.TimeoutConfiguration.Condition;

        public bool WaitForDisplayed(TimeSpan? timeout = null)
        {
            throw new NotImplementedException();
        }

        public bool WaitForExist(TimeSpan? timeout = null)
        {
            throw new NotImplementedException();
        }

        public bool WaitForNotDisplayed(TimeSpan? timeout = null)
        {
            throw new NotImplementedException();
        }

        public bool WaitForNotExist(TimeSpan? timeout = null)
        {
            throw new NotImplementedException();
        }
    }
}
