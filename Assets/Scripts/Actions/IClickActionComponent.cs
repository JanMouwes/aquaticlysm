using System.Collections.Generic;
using UnityEngine;

namespace Actions
{
    public interface IClickActionComponent
    {
        bool HandleAction(RaycastHit hit, bool priority);
    }
}