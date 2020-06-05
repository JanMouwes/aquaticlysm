using System;
using UnityEditor.Graphs;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CharacterButton : MonoBehaviour
    {
        public Character Character { get; set; }

        public void Start()
        {
            Character = null;
        }

        public void OnClick()
        {
            // If button is clicked, grab selectable from Character and set selected on true.
            if (Character != null)
            {
                Character.GetComponent<Selectable>().Selected = true;
            }
        }
        
    }
}