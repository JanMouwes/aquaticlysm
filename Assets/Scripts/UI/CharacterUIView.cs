using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class CharacterUIView : MonoBehaviour
    {
        private readonly Dictionary<int, GameObject> _characterUiElements = new Dictionary<int, GameObject>();

        public CharacterSystem characterSystem;

        public GameObject characterButtonPrefab;

        public IEnumerable<Character> Characters => this.characterSystem.CharacterManager.characters;

        private void Update()
        {
            Character[] characters = this.Characters.ToArray();
            IEnumerable<int> ids = characters.Select(character => character.GetInstanceID()).ToArray();

            foreach (int uiElementKey in this._characterUiElements.Keys.ToArray().Where(key => !ids.Contains(key)))
            {
                if (this._characterUiElements.TryGetValue(uiElementKey, out GameObject element)) { Destroy(element); }

                // Remove key not in characters
                this._characterUiElements.Remove(uiElementKey);
            }

            for (int index = 0; index < characters.Length; index++)
            {
                Character character = characters[index];
                Draw(character, index);
            }
        }

        public void Draw(Character character, int index)
        {
            int characterId = character.GetInstanceID();

            if (!this._characterUiElements.TryGetValue(characterId, out GameObject element))
            {
                element = PrefabInstanceManager.Instance.Spawn(this.characterButtonPrefab, Vector3.zero);
                this._characterUiElements[characterId] = element;
            }

            RectTransform imageTransform = element.GetComponent<RectTransform>();
            float imageYPos = index * -100;

            imageTransform.SetParent(this.gameObject.transform);
            imageTransform.anchoredPosition = new Vector2(80, imageYPos);

            Image image = element.GetComponent<Image>();
            image.sprite = character.Portrait;
        }
    }
}