using Resources;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityResources = UnityEngine.Resources;

public class ResourceUIView : MonoBehaviour
{
    private readonly Dictionary<string, GameObject> _resourceUiElements = new Dictionary<string, GameObject>();

    private ResourceManager _resourceManager;

    public GameObject resourceElementPrefab;

    private void Start()
    {
        _resourceManager = ResourceManager.Instance;
    }

    void Update()
    {
        IEnumerable<Resource> resources = _resourceManager.Resources.OrderBy(res => res.type.ShortName);

        foreach (Resource resource in resources)
        {
            SetResourceValue(resource.type, resource.Amount);  
        }
    }

    /// <summary>
    /// Updates the resource value on the UI, if the type doesn't excists on the UI create it.
    /// </summary>
    /// <param name="resourceType">The resource type.</param>
    /// <param name="value">The resource value.</param>
    private void SetResourceValue(ResourceType resourceType, int value)
    {
        if (!_resourceUiElements.TryGetValue(resourceType.ShortName, out GameObject element))
        {
            int resourceCount = _resourceUiElements.Count;

            // If resource not yet registered, add it
            GameObject instance = PrefabInstanceManager.Instance.Spawn(resourceElementPrefab, Vector3.zero);
            instance.transform.SetParent(gameObject.transform);
            instance.transform.localScale = Vector3.one;

            // Get image
            RectTransform imageTransform = instance.GetComponent<RectTransform>();
            imageTransform.SetParent(gameObject.transform);

            // Image position
            float distance = imageTransform.sizeDelta.x;
            imageTransform.anchoredPosition = new Vector2(distance * 4 * resourceCount + distance, 0);

            // Set icon inside of the image
            Sprite icon = UnityResources.Load<Sprite>(resourceType.IconPath);
            instance.GetComponent<Image>().sprite = icon;

            _resourceUiElements.Add(resourceType.ShortName, instance);
            element = instance;
        }

        // Set text
        GameObject child = element.transform.Find("Amount").gameObject;
        child.GetComponent<Text>().text = value.ToString();
    }
}