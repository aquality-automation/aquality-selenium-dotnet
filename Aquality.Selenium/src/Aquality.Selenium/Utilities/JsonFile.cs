using Aquality.Selenium.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Aquality.Selenium.Utilities
{
    public sealed class JsonFile
    {
        private readonly string fileContent;

        private JObject JsonObject => JsonConvert.DeserializeObject<JObject>(fileContent);

        public JsonFile(FileInfo fileInfo)
        {
            fileContent = FileReader.GetTextFromFile(fileInfo);
        }

        public JsonFile(string embeddedResourceName)
        {
            fileContent = FileReader.GetTextFromEmbeddedResource(embeddedResourceName);
        }

        public T GetObject<T>(string name = null)
        {
            return name == null
                ? JsonObject.ToObject<T>()
                : JsonObject.SelectToken(name).ToObject<T>();
        }

        public object GetValue(string jsonPath)
        {
            return GetEnvironmentValueOrDefault(jsonPath);
        }

        private object GetEnvironmentValueOrDefault(string jsonPath)
        {
            var key = jsonPath.Replace('/', '.').Substring(1);
            var envValue = Environment.GetEnvironmentVariable(key);
            if(envValue == null)
            {
                var node = GetJsonNode(jsonPath);
                if (node.Type == JTokenType.Boolean)
                {
                    return node.ToObject<bool>();
                }
                else if (node.Type == JTokenType.Integer)
                {
                    return node.ToObject<int>();
                }
                else
                {
                    return node.ToString();
                }
            }
            else
            {
                Logger.Instance.Debug($"***** Using variable passed from environment {key}={envValue}");
                return envValue;
            }
        }

        private JToken GetJsonNode(string jsonPath)
        {
            return JsonObject.SelectToken(jsonPath);
        }
    }
}
