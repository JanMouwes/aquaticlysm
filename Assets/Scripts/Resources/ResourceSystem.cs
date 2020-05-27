using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Resources
{
    public class ResourceSystem : MonoBehaviour
    {
        private Dictionary<string, ResourceType> _resourceTypes;
        public IEnumerable<ResourceType> ResourceTypes => this._resourceTypes.Values;

        public string resourceFilePath;
        public string resourceStartingValuesFilePath;

        private void Start()
        {
            this._resourceTypes = new Dictionary<string, ResourceType>();
            ResourceManager.Instance.SetResourceSystem(this);

            foreach (ResourceType resourceType in ParseResourcesFromXmlFile(this.resourceFilePath))
            {
                // Register resource type   
                this._resourceTypes.Add(resourceType.shortName, resourceType);
            }

            foreach ((string resourceTypeName, int amount) in ParseResourceStartingValues(this.resourceStartingValuesFilePath))
            {
                // Registers resource to resource manager with starting amount - quick fix code
                ResourceManager.Instance.AddResourceType(resourceTypeName, amount);
            }
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

            const string defaultPath = "Assets/Sprites/default-resource.png";

            foreach (XmlNode node in nodeList)
            {
                yield return new ResourceType(
                    GetValueOrDefault(node, "name", "unknown resource"),
                    GetValueOrDefault(node, "icon-path", defaultPath)
                );
            }
        }

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

        public bool TryGetResourceType(string key, out ResourceType resourceType)
        {
            return this._resourceTypes.TryGetValue(key, out resourceType);
        }
    }
}