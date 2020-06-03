using System;
using System.Collections.Generic;
using System.Linq;
using Actions;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ActionUiView : MonoBehaviour
    {
        private IEnumerable<Selectable> CurrentSelection => SelectionController.selectedEntities;

        private Dictionary<string, GameObject> _actionUiElements;

        public GameObject actionButtonPrefab;

        private void Start()
        {
            this._actionUiElements = new Dictionary<string, GameObject>();
        }

        private void Update()
        {
            IEnumerable<GameActionButtonModel> actions = this.CurrentSelection
                                                             .SelectMany(entity => entity.GetComponents<MonoBehaviour>())
                                                             .OfType<IActionComponent>()
                                                             .ToArray()
                                                             .SelectMany(component => component.ButtonModels);

            foreach (GameActionButtonModel buttonModel in actions.Where(action => !this._actionUiElements.ContainsKey(action.Name)))
            {
                int index = this._actionUiElements.Count;
                SetActionButtonIcon(buttonModel, index);
            }
        }

        private GameObject GetActionUiElement(string key)
        {
            if (!this._actionUiElements.TryGetValue(key, out GameObject element))
            {
                element = PrefabInstanceManager.Instance.Spawn(this.actionButtonPrefab, Vector3.zero);

                this._actionUiElements.Add(key, element);
            }

            return element;
        }

        private void SetActionButtonIcon(GameActionButtonModel model, int index)
        {
            string key = model.Name;
            Sprite sprite = model.Icon;

            GameObject element = GetActionUiElement(key);

            const float distance = 70;

            RectTransform rectTransform = element.GetComponent<RectTransform>();

            element.transform.SetParent(this.gameObject.transform);
            element.transform.localScale = Vector3.one;
            element.transform.position = Vector3.zero;
            rectTransform.anchoredPosition = new Vector2(distance * index + (distance * .5f), 0);

            Image image = element.GetComponent<Image>();
            image.sprite = sprite;

            Debug.Log("test!");

            if (model.OnClick != null)
            {
                Debug.Log(model);
                Debug.Log(model.OnClick);
                element.GetComponent<Button>()
                       .onClick
                       .AddListener(model.OnClick);
            }
        }
    }
}