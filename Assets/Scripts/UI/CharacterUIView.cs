using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class CharacterUIView : MonoBehaviour
    {
        private readonly Dictionary<int, GameObject> _characterUiElements = new Dictionary<int, GameObject>();

        private CharacterSystem _characterSystem;
        
        public GameObject characterButtonPrefab;
        
        private void Awake()
        {
            _characterSystem = new CharacterSystem();
        }

        private void Update()
        {
            
            if (Input.GetKeyUp("B"))
            {
                
            }

            foreach (Character character in _characterSystem.CharacterManager.list)
            {
                Draw(character);
            }
        }

        public void Draw(Character character)
        {
            if (!_characterUiElements.TryGetValue(character.gameObject.GetInstanceID(), out GameObject element))
            {
                GameObject instance = PrefabInstanceManager.Instance.Spawn(this.characterButtonPrefab, Vector3.zero);

                
            }
            
            
        }
        
    }
}