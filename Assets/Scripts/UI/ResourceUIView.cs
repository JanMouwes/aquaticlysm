using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Resources;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ResourceUIView : MonoBehaviour
{
    private readonly Dictionary<string, GameObject> _resourceUiElements = new Dictionary<string, GameObject>();

    public GameObject resourceElementPrefab;

    // Update is called once per frame
    void Update()
    {
        IEnumerable<Resource> resources = ResourceManager.Instance.Resources.OrderBy(res => res.type.shortName);

        foreach (Resource resource in resources) { SetResourceValue(resource.type.shortName, resource.Amount); }
    }

    private void SetResourceValue(string resource, int value)
    {
        if (!this._resourceUiElements.TryGetValue(resource, out GameObject element))
        {
            // If resource not yet registered, add it
            GameObject instance = PrefabInstanceManager.Instance.Spawn(this.resourceElementPrefab, Vector3.zero);
            RectTransform rectTransform = instance.GetComponent<RectTransform>();

            int resourceCount = this._resourceUiElements.Count;

            rectTransform.SetParent(this.gameObject.transform);
            rectTransform.anchoredPosition = new Vector2((resourceCount + 1) * 100, 0);

            this._resourceUiElements.Add(resource, instance);
            element = instance;
        }

        GameObject child = element.transform.Find("Amount").gameObject;
        child.GetComponent<Text>().text = resource + ": " + value.ToString();
    }
}