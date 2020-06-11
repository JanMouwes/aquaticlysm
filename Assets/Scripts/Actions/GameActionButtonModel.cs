using UnityEngine;
using UnityEngine.Events;

namespace Actions
{
    public struct GameActionButtonModel
    {
        public string Name { get; set; }
        
        public Sprite Icon { get; set; }

        public UnityAction OnClick { get; set; }
    }
}