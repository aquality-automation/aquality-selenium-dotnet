using System;
using System.Collections.Generic;
using System.Text;

namespace Aquality.Selenium.Locators
{
    internal class Function
    {
        public string Name { get; }
        public Object[] Arguments { get; }

        private Function()
        {

        }

        public Function(string name, Object[] arguments)
        {
            Name = name;
            Arguments = arguments;
        }

    }
}
