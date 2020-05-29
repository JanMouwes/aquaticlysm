using System;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    
    public class CharacterButton : MonoBehaviour
    {
        public Character Character { get; set; }
        private float _doubleClickTimer;

        public void OnClick()
        {
            if(Character != null)
                Character.GetComponent<Selectable>().Selected = true;
        }
        
    }
}