using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Resources
{
    public class ResourceSystem : MonoBehaviour
    {
        private Dictionary<string, ResourceType> _resourceTypes;
        public IEnumerable<ResourceType> ResourceTypes => _resourceTypes.Values;
        
        public string resourceFilePath;

        private void Start()
        {
            _resourceTypes = new Dictionary<string, ResourceType>();   
            ResourceManager.Instance.SetResourceSystem(this);
            
            InitResources();
        }

        private void InitResources()
        {
            ParseResourcesFromXml();

            foreach (ResourceType resourceType in ResourceTypes)
            {
                ResourceManager.Instance.AddResourceType(resourceType.shortName);
            }
        }

        private void ParseResourcesFromXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(resourceFilePath);

            XmlNodeList nodeList = doc.GetElementsByTagName("resource");

            string GetValueOrDefault(XmlNode node, string elementName, string defaultValue = default)
            {
                XmlNodeList temp = node.SelectNodes(elementName);
                
                if (temp == null || temp.Count == 0)
                {
                    return defaultValue;
                }
                
                return temp[0].InnerText;
            }
            
            foreach (XmlNode node in nodeList)
            {
                string defaultPath = "Assets/Sprites/default-resource.png";
                ResourceType resourceType = new ResourceType(
                        GetValueOrDefault(node, "name", "unknown resource"),
                        GetValueOrDefault(node, "icon-path", defaultPath)
                );
                
                _resourceTypes.Add(resourceType.shortName, resourceType);
            }
        }

        public bool GetResourceType(string key, out ResourceType resourceType)
        {
            return _resourceTypes.TryGetValue(key, out resourceType);
        }
        


    }
}