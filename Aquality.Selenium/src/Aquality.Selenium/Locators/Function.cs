namespace Aquality.Selenium.Locators
{
    internal class Function
    {
        public string Name { get; }
        public object[] Arguments { get; }

        private Function() { }

        public Function(string name, object[] arguments)
        {
            Name = name;
            Arguments = arguments;
        }
    }
}
