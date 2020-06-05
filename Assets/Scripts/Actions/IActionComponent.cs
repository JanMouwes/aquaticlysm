using System.Collections.Generic;
using Actions.GameActions;
using UnityEngine;

namespace Actions
{
    public interface IActionComponent
    {
        IEnumerable<GameActionButtonModel> ButtonModels { get; }

        bool HandleAction(RaycastHit hit, bool priority);
    }
}