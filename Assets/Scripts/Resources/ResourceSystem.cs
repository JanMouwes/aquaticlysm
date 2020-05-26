using UnityEngine;

namespace Resources
{
    public class ResourceSystem : MonoBehaviour
    {
        private ResourceManager _instance;
        private void Start()
        {
            _instance = ResourceManager.Instance;
            
            InitResources();
        }

        private void InitResources()
        {
            _instance.AddResourceType("wood", 100);
            _instance.AddResourceType("steel", 100);
            _instance.AddResourceType("plastic", 100);
            _instance.AddResourceType("water", 100);
        }
        
        
    }
}