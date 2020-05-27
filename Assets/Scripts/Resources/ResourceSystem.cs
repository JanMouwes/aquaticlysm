using UnityEngine;

namespace Resources
{
    public class ResourceSystem : MonoBehaviour
    {
        private ResourceTypeManager _resourceTypeManager;

        public string ResourceTypesFilePath { get; set; }
        public string ResourceStartingValuesFilePath { get; set; }

        private void Start()
        {
            this._resourceTypeManager = new ResourceTypeManager();
            this._resourceTypeManager.Init(this.ResourceTypesFilePath);

            ResourceManager.Instance.SetResourceTypeManager(this._resourceTypeManager);

            foreach ((string resourceTypeName, int amount) in ResourceTypeManager.ParseResourceStartingValues(ResourceStartingValuesFilePath))
            {
                // Registers resource to resource manager with starting amount - quick fix code
                ResourceManager.Instance.AddResourceType(resourceTypeName, amount);
            }
        }
    }
}