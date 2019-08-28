using System.Collections.Generic;

namespace Aquality.Selenium.Utilities
{
    public static class JsonFileExtensions
    {
        public static T GetValueOrNew<T>(this JsonFile jsonFile, string jsonPath) where T : new()
        {
            return GetValueOrDefault(jsonFile, jsonPath, new T());
        }

        public static IList<T> GetValueListOrEmpty<T>(this JsonFile jsonFile, string jsonPath)
        {
            return jsonFile.IsValuePresent(jsonPath) ? jsonFile.GetValueList<T>(jsonPath) : new List<T>();
        }

        public static T GetValueOrDefault<T>(this JsonFile jsonFile, string jsonPath, T defaultValue = default(T))
        {
            return jsonFile.IsValuePresent(jsonPath) ? jsonFile.GetValue<T>(jsonPath) : defaultValue;
        }
    }
}
