using UnityEngine;

namespace Resources
{
    public class ResourceSystem : MonoBehaviour
    {
        private ResourceTypeManager _resourceTypeManager;

        public string resourceTypesFilePath;
        public string resourceStartingValuesFilePath;
        private ResourceManager _resourceManager;

        private void Start()
        {
            _resourceManager = FindObjectOfType<ResourceManager>();
            this._resourceTypeManager = new ResourceTypeManager();
            this._resourceTypeManager.Init(this.resourceTypesFilePath);

            _resourceManager.SetResourceTypeManager(this._resourceTypeManager);

            foreach ((string resourceTypeName, int amount) in ResourceTypeManager.ParseResourceStartingValues(this.resourceStartingValuesFilePath))
            {
                // Registers resource to resource manager with starting amount - quick fix code
                _resourceManager.AddResourceType(resourceTypeName, amount);
            }
        }

        public void ReloadResourceSystem()
        {
            Start();
        }
    }
}