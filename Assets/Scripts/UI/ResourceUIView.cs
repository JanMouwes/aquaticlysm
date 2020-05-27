using System;
using System.Collections.Generic;
using System.Linq;
using Resources;
using UnityEngine;
using UnityEngine.UI;
using UnityResources = UnityEngine.Resources;

public class ResourceUIView : MonoBehaviour
{
    private readonly Dictionary<string, GameObject> _resourceUiElements = new Dictionary<string, GameObject>();

    private readonly ResourceManager _resourceManager;

    public GameObject resourceElementPrefab;

    public ResourceUIView()
    {
        this._resourceManager = ResourceManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        IEnumerable<Resource> resources = this._resourceManager.Resources.OrderBy(res => res.type.ShortName);

        foreach (Resource resource in resources) { SetResourceValue(resource.type, resource.Amount); }
    }

    private void SetResourceValue(ResourceType resourceType, int value)
    {
        if (!this._resourceUiElements.TryGetValue(resourceType.ShortName, out GameObject element))
        {
            // If resource not yet registered, add it
            GameObject instance = PrefabInstanceManager.Instance.Spawn(this.resourceElementPrefab, Vector3.zero);

            int resourceCount = this._resourceUiElements.Count;

            RectTransform imageTransform = instance.GetComponent<RectTransform>();
            imageTransform.SetParent(this.gameObject.transform);
            imageTransform.anchoredPosition = new Vector2((resourceCount * 60) + 30, 0);

            Sprite icon = UnityResources.Load<Sprite>(resourceType.IconPath);

            instance.GetComponent<Image>().sprite = icon;
            this._resourceUiElements.Add(resourceType.ShortName, instance);
            element = instance;
        }

        GameObject child = element.transform.Find("Amount").gameObject;
        child.GetComponent<Text>().text = value.ToString();
    }
}