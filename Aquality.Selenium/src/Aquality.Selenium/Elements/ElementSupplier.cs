using Aquality.Selenium.Elements.Interfaces;
using OpenQA.Selenium;
using System;

namespace Aquality.Selenium.Elements
{
    public delegate Func<By, string, ElementState, IElement> ElementSupplier();
}
