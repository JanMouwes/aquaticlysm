using System.Collections.Generic;

namespace Resources
{
    public interface IResourceTypeManager
    {
        IEnumerable<ResourceType> ResourceTypes { get; }
        void Init(string resourceFilePath);
        bool TryGetResourceType(string key, out ResourceType resourceType);
    }
}