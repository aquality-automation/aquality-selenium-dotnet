using System;
using System.IO;
using System.Reflection;

namespace Aquality.Selenium.Utilities
{
    /// <summary>
    /// Utility methods to read files.
    /// </summary>
    internal static class FileReader
    {
        private const string ResourcesFolder = "Resources";

        /// <summary>
        /// Gets text from embedded resource file.
        /// </summary>
        /// <param name="embeddedResourcePath">Embedded resource path.</param>
        /// <param name="resourceAssembly">Assembly which resource belongs to.</param>
        /// <returns>Text of the file.</returns>
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
        /// Gets text from the file in the Resources folder (should be copied to output directory).
        /// </summary>
        /// <param name="fileName">Name of the resource file.</param>
        /// <returns>Text of the file.</returns>
        public static string GetTextFromResource(string fileName)
        {
            return GetTextFromFile(new FileInfo(Path.Combine(ResourcesFolder, fileName)));
        }

        /// <summary>
        /// Checks whether file exists in Resources folder or not.
        /// </summary>
        /// <param name="fileName">Name of resource file.</param>
        /// <returns>True if exists and false otherwise</returns>
        public static bool IsResourceFileExist(string fileName)
        {
            var fileInfo = new FileInfo(Path.Combine(ResourcesFolder, fileName));
            return fileInfo.Exists;
        }

        /// <summary>
        /// Gets text from the file.
        /// </summary>
        /// <param name="fileInfo">Required file info.</param>
        /// <returns>Text of the file.</returns>
        public static string GetTextFromFile(FileInfo fileInfo)
        {
            using (var reader = fileInfo.OpenText())
            {
                return reader.ReadToEnd();
            }
        }
    }
}
