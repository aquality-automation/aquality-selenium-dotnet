using Aquality.Selenium.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Aquality.Selenium.Utilities
{
    public sealed class JsonFile
    {
        private readonly string fileName;
        private readonly string fileContent;

        private JObject JsonObject => JsonConvert.DeserializeObject<JObject>(fileContent);

        public JsonFile(FileInfo fileInfo)
        {
            fileContent = FileReader.GetTextFromFile(fileInfo);
            fileName = fileInfo.Name;
        }

        public JsonFile(string resourceFileName)
        {
            fileContent = FileReader.GetTextFromResource(resourceFileName);
            fileName = resourceFileName;
        }

        public T GetObject<T>(string jsonPath)
        {
            return (T) GetValue(jsonPath);
        }

        public object GetValue(string jsonPath)
        {
            return GetEnvironmentValueOrDefault(jsonPath);
        }

        public bool IsValuePresent(string jsonPath)
        {
            return GetEnvironmentValue(jsonPath) != null || GetJsonNode(jsonPath) != null;
        }

        private object GetEnvironmentValueOrDefault(string jsonPath)
        {
            var envValue = GetEnvironmentValue(jsonPath);
            if(envValue == null)
            {
                var node = GetJsonNode(jsonPath);
                if(node == null)
                {
                    throw new InvalidDataException($"Failed to get value by JPath {jsonPath} from json file {fileName}");
                }
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
                Logger.Instance.Debug($"***** Using variable passed from environment {jsonPath.Substring(1)}={envValue}");
                return envValue;
            }
        }

        private string GetEnvironmentValue(string jsonPath)
        {
            var key = jsonPath.Substring(1);
            return EnvironmentConfiguration.GetVariable(key);
        }

        private JToken GetJsonNode(string jsonPath)
        {
            return JsonObject.SelectToken(jsonPath);
        }
    }
}
