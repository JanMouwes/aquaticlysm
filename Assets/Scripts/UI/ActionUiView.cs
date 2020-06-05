using Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ActionUiView : MonoBehaviour
    {
        private IEnumerable<Selectable> CurrentSelection => SelectionController.SelectedEntities;

        private Dictionary<string, GameObject> _actionUiElements;

        public GameObject actionButtonPrefab;
        public Builder builder;

        IEnumerable<GameActionButtonModel> _actions;
        private Selectable[] _oldSelection;
        private LinkedList<int> buttons;

        private void Start()
        {
            _actionUiElements = new Dictionary<string, GameObject>();
            _oldSelection = new Selectable[0];

            if (builder != null)
                _actions = builder.ButtonModels;
        }

        private void Update()
        {
            if (!_oldSelection.SequenceEqual(CurrentSelection))
            {
                _oldSelection = CurrentSelection.ToArray();

                ClearButtons();

                if (_oldSelection.Length == 0)
                {
                    if(builder != null)
                        _actions = builder.ButtonModels;
                }
                else
                {
                    _actions = _oldSelection
                                    .SelectMany(entity => entity.GetComponents<MonoBehaviour>())
                                    .OfType<IActionComponent>()
                                    .ToArray()
                                    .SelectMany(component => component.ButtonModels);
                }

                foreach (GameActionButtonModel buttonModel in _actions.Where(action => !_actionUiElements.ContainsKey(action.Name)))
                {
                    int index = _actionUiElements.Count;
                    SetActionButtonModel(buttonModel, index);
                }
            }
        }

        private void ClearButtons() 
        {
            foreach (var item in _actionUiElements)
                PrefabInstanceManager.Instance.DestroyEntity(item.Value.GetInstanceID());

            _actionUiElements.Clear();
        }

        private GameObject GetActionUiElement(string key)
        {
            if (!_actionUiElements.TryGetValue(key, out GameObject element))
            {
                element = PrefabInstanceManager.Instance.Spawn(actionButtonPrefab, Vector3.zero);

                _actionUiElements.Add(key, element);
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

            element.transform.SetParent(gameObject.transform);
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