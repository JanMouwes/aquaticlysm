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
            int SIZE = 50;
            int characterId = character.GetInstanceID();

            if (!this._characterUiElements.TryGetValue(characterId, out GameObject buttonGameObject))
            {
                buttonGameObject = PrefabInstanceManager.Instance.Spawn(this.characterButtonPrefab, Vector3.zero);
                this._characterUiElements[characterId] = buttonGameObject;
            }
            Image image = buttonGameObject.GetComponent<Image>();
            Button btn = buttonGameObject.GetComponent<Button>();
            CharacterButton charbtn = buttonGameObject.GetComponent<CharacterButton>();

            if (charbtn.Character == null)
            {
                charbtn.Character = character;
            }
            if (character.GetComponent<Selectable>().Selected)
            {
                btn.Select();
            }
            
            RectTransform imageTransform = buttonGameObject.GetComponent<RectTransform>();
            float imageYPos = index * -SIZE;

            imageTransform.SetParent(this.gameObject.transform);
            imageTransform.anchoredPosition = new Vector2(80, imageYPos);
            
            image.sprite = character.Portrait;
        }
    }
}