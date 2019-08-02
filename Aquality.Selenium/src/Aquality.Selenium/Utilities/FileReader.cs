using System;
using System.IO;
using System.Reflection;

namespace Aquality.Selenium.Utilities
{
    internal static class FileReader
    {
        private const string ResourcesFolder = "Resources";

        public static string GetTextFromEmbeddedResource(string embeddedResourcePath, Assembly resourceAssembly = null)
        {
            var assembly = resourceAssembly ?? Assembly.GetCallingAssembly();
            var resourcePath = $"{assembly.GetName().Name}.{embeddedResourcePath}";
            using (var stream = assembly.GetManifestResourceStream(resourcePath))
            {
                if (stream == null)
                {
                    throw new InvalidOperationException(
                        $"Assembly {assembly.FullName} doesn't contain EmbeddedResource at path {resourcePath}. Resource file cannot be loaded");
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Gets text from the file in the Resources folded (should be copied to output directory)
        /// </summary>
        /// <param name="fileName">name of the resource file</param>
        /// <returns>text of the file</returns>
        public static string GetTextFromResource(string fileName)
        {
            return GetTextFromFile(new FileInfo(Path.Combine(ResourcesFolder, fileName)));
        }

        /// <summary>
        /// Gets text from the file
        /// </summary>
        /// <param name="fileInfo">required file info</param>
        /// <returns>text of the file</returns>
        public static string GetTextFromFile(FileInfo fileInfo)
        {
            using (var reader = fileInfo.OpenText())
            {
                return reader.ReadToEnd();
            }
        }
    }
}
