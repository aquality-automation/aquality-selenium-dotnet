using System;
using System.Diagnostics.Contracts;

namespace Aquality.Selenium.Utilities
{
    internal static class EnumExtensions
    {
        internal static T ToEnum<T>(this object value) where T : struct, IConvertible
        {
            return value is int
                ? (T) (object) (int) (long) value 
                : value.ToString().ToEnum<T>();
        }

        internal static T ToEnum<T>(this string value) where T : struct, IConvertible
        {
            var type = typeof(T);
            Contract.Assert(type.IsEnum, "T must be an enum type");
            return (T) Enum.Parse(type, value, ignoreCase: true);
        }
    }
}
