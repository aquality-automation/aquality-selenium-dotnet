using System;
using System.ComponentModel;
using System.Reflection;

namespace Aquality.Selenium.Tests.Integration.TestApp.Utilities
{
    internal static class EnumExtensions
    {
        public static string GetDescription<T>(this T value) where T : struct, IConvertible
        {
            var description = typeof(T).GetField(value.ToString()).GetCustomAttribute<DescriptionAttribute>();
            return description != null ? description.Description : value.ToString();
        }
    }
}
