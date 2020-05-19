using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface ISelectable
{
    bool Selected { get; set; }

    /// <summary>
    /// The action handler processes all the selectable specific actions.
    /// </summary>
    /// <param name="tag">The tag of the clicked object</param>
    /// <param name="position">The click position</param>
    /// <param name="priority">Is it a prioritized action or not</param>
    bool ActionHandler(string tag, Vector3 position, bool priority);
}

