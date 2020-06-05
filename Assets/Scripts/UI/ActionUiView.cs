using System.Collections.Generic;
using System.Linq;
using Actions;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ActionUiView : MonoBehaviour
    {
        private IEnumerable<Selectable> CurrentSelection => SelectionController.SelectedEntities;

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
                SetActionButtonModel(buttonModel, index);
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

        private void SetActionButtonModel(GameActionButtonModel model, int index)
        {
            string key = model.Name;
            Sprite sprite = model.Icon;

            GameObject element = GetActionUiElement(key);
            element.name = "Button for " + model.Name;

            RectTransform rectTransform = element.GetComponent<RectTransform>();
            float distance = rectTransform.sizeDelta.x;

            element.transform.SetParent(this.gameObject.transform);
            element.transform.localScale = Vector3.one;
            element.transform.position = Vector3.zero;
            rectTransform.anchoredPosition = new Vector2(distance * 1.6f * index + distance, 0);

            Image image = element.GetComponent<Image>();
            image.sprite = sprite;

            if (model.OnClick != null)
            {
                Button button = element.GetComponent<Button>();

                Debug.Log(button.name);
                
                button.onClick
                      .AddListener(() =>
                       {
                           model.OnClick();
                           Debug.Log("Called onClick");
                       });
            }
        }
    }
}