using System.Collections.Generic;

namespace Resources
{
    public interface IResourceTypeManager
    {
        /// <summary>
        /// All registered resource types
        /// </summary>
        IEnumerable<ResourceType> ResourceTypes { get; }
        
        /// <summary>
        /// Initialises the resource manager
        /// </summary>
        /// <param name="resourceFilePath">File to find the resource definitions</param>
        void Init(string resourceFilePath);
        
        /// <summary>
        /// Tries to find resource type in registered resources types
        /// </summary>
        /// <param name="key">Name of resource type to try to find</param>
        /// <param name="resourceType">Resource type if found, null if not found</param>
        /// <returns>Whether the resource was found</returns>
        bool TryGetResourceType(string key, out ResourceType resourceType);
    }
}