using Actions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Builder))]
    public class ActionUiView : MonoBehaviour
    {
        public GameObject actionButtonPrefab;
        public Builder builder;

        private Dictionary<string, GameObject> _actionUiElements;
        private IEnumerable<GameActionButtonModel> _actions;
        private Selectable[] _oldSelection;

        private IEnumerable<Selectable> CurrentSelection => SelectionController.SelectedEntities;

        private void Start()
        {
            _actionUiElements = new Dictionary<string, GameObject>();
            _oldSelection = new Selectable[0];

            if (builder != null)
                UpdateButtons();
        }

        private void Update()
        {
            if (!_oldSelection.SequenceEqual(CurrentSelection))
            {
                _oldSelection = CurrentSelection.ToArray();
                UpdateButtons();
            }
        }

        /// <summary>
        /// Clear the current action buttons on the UI and set new buttons.
        /// </summary>
        private void UpdateButtons()
        {
            ClearButtons();

            _actions = (_oldSelection.Length == 0) ?
                _actions = builder.ButtonModels :
                _oldSelection
                    .SelectMany(entity => entity.GetComponents<MonoBehaviour>())
                    .OfType<IButtonActionComponent>()
                    .ToArray()
                    .SelectMany(component => component.ButtonModels);

            if (_actions != null)
            {
                foreach (GameActionButtonModel buttonModel in _actions.Where(action => !_actionUiElements.ContainsKey(action.Name)))
                {
                    int index = _actionUiElements.Count;
                    SetActionButtonModel(buttonModel, index);
                }
            }
        }

        /// <summary>
        /// Clears all the buttons.
        /// </summary>
        private void ClearButtons()
        {
            foreach (KeyValuePair<string, GameObject> item in _actionUiElements)
                PrefabInstanceManager.Instance.DestroyEntity(item.Value.GetInstanceID());

            _actionUiElements.Clear();
        }

        /// <summary>
        /// Gets the UI element.
        /// </summary>
        /// <param name="key">The model name</param>
        /// <returns>The UI button.</returns>
        private GameObject GetActionUiElement(string key)
        {
            if (!_actionUiElements.TryGetValue(key, out GameObject element))
            {
                element = PrefabInstanceManager.Instance.Spawn(actionButtonPrefab, Vector3.zero);

                _actionUiElements.Add(key, element);
            }

            return element;
        }

        /// <summary>
        /// Create and set the buttons in the UI.
        /// </summary>
        /// <param name="model">The game action button model.</param>
        /// <param name="index">The index.</param>
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
                button.onClick.AddListener(() => model.OnClick());
            }
        }
    }
}