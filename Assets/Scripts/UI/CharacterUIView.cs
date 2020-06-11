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

            // Remove characters that were destroyed
            foreach (int uiElementKey in this._characterUiElements.Keys.ToArray().Where(key => !ids.Contains(key)))
            {
                if (this._characterUiElements.TryGetValue(uiElementKey, out GameObject element)) { Destroy(element); }

                this._characterUiElements.Remove(uiElementKey);
            }

            // Draw character-buttons
            for (int index = 0; index < characters.Length; index++)
            {
                Character character = characters[index];
                Draw(character, index);
            }
        }

        /// <summary>
        /// Draws ui-element for a given character
        /// </summary>
        /// <param name="character">Character for which to draw a ui element</param>
        /// <param name="index">Index of this character within the list of characters. Determines element position</param>
        public void Draw(Character character, int index)
        {
            int characterId = character.GetInstanceID();

            // Add UI element when not present
            if (!this._characterUiElements.TryGetValue(characterId, out GameObject buttonGameObject))
            {
                buttonGameObject = PrefabInstanceManager.Instance.Spawn(this.characterButtonPrefab, Vector3.zero);
                this._characterUiElements[characterId] = buttonGameObject;
            }

            Image image = buttonGameObject.transform.GetChild(0).GetComponent<Image>();
            Button btn = buttonGameObject.GetComponent<Button>();
            CharacterButton charbtn = buttonGameObject.GetComponent<CharacterButton>();
            charbtn.transform.SetParent(gameObject.transform);
            charbtn.transform.localScale = Vector3.one;
            charbtn.transform.position = Vector3.zero;

            if (charbtn.Character == null) 
                charbtn.Character = character;

            if (character.GetComponent<Selectable>().Selected)
                btn.Select();

            RectTransform imageTransform = buttonGameObject.GetComponent<RectTransform>();
            float distance = imageTransform.sizeDelta.y*2;
            float imageYPos = index * -distance;

            imageTransform.SetParent(this.gameObject.transform);
            imageTransform.anchoredPosition = new Vector2(100, imageYPos);

            image.sprite = character.Portrait;
        }
    }
}