using System.Collections.Generic;
using System.Xml;

namespace Resources
{
    /// <inheritdoc cref="IResourceTypeManager"/>
    public class ResourceTypeManager : IResourceTypeManager
    {
        private Dictionary<string, ResourceType> _resourceTypes;
        public IEnumerable<ResourceType> ResourceTypes => this._resourceTypes.Values;

        public void Init(string resourceFilePath)
        {
            this._resourceTypes = new Dictionary<string, ResourceType>();


            foreach (ResourceType resourceType in ParseResourcesFromXmlFile(resourceFilePath))
            {
                // Register resource type   
                this._resourceTypes.Add(resourceType.ShortName, resourceType);
            }
        }

        public bool TryGetResourceType(string key, out ResourceType resourceType)
        {
            return this._resourceTypes.TryGetValue(key, out resourceType);
        }

        /// <summary>
        /// Parses xml-file as resourceTypes
        /// </summary>
        /// <param name="filePath">Path where to look for file</param>
        /// <returns>All resource types found</returns>
        public static IEnumerable<ResourceType> ParseResourcesFromXmlFile(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNodeList nodeList = doc.GetElementsByTagName("resource");

            string GetValueOrDefault(XmlNode node, string elementName, string defaultValue = default)
            {
                XmlNodeList temp = node.SelectNodes(elementName);

                if (temp == null || temp.Count == 0) { return defaultValue; }

                return temp[0].InnerText;
            }          
            
            int GetIntValueOrDefault(XmlNode node, string elementName, int defaultValue = default)
            {
                XmlNodeList temp = node.SelectNodes(elementName);

                if (temp == null || temp.Count == 0) { return defaultValue; }

                int x = 0;
                System.Int32.TryParse(temp[0].InnerText, out x);

                return x;
            }

            const string defaultPath = "Assets/Sprites/default-resource.png";

            foreach (XmlNode node in nodeList)
            {
                yield return new ResourceType(
                    GetValueOrDefault(node, "name", "unknown resource"),
                    GetValueOrDefault(node, "icon-path", defaultPath),
                    GetValueOrDefault(node, "description", ""),
                    GetIntValueOrDefault(node, "scarcity", 0)
                );
            }
        }

        /// <summary>
        /// Retrieves resource starting values from xml-file
        /// </summary>
        /// <param name="filePath">Xml-file from which to get values</param>
        /// <returns>All found starting values</returns>
        public static IEnumerable<(string resourceTypeName, int amount)> ParseResourceStartingValues(string filePath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNodeList nodeList = doc.GetElementsByTagName("resource");

            foreach (XmlNode node in nodeList)
            {
                if (node.Attributes == null) { continue; }

                bool success = int.TryParse(node.InnerText, out int result);

                string resourceName = node.Attributes["name"].Value;

                yield return success ? (resourceName, result) : (resourceName, 0);
            }
        }
    }
}