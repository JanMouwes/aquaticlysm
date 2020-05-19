using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Selectable : MonoBehaviour
{
    public bool Selected { get; set; }

    public GameObject Owner;

    private Outline _outline;

    /// <summary>
    /// The action handler processes all the selectable specific actions.
    /// </summary>
    /// <param name="tag">The tag of the clicked object</param>
    /// <param name="position">The click position</param>
    /// <param name="priority">Is it a prioritized action or not</param>
    public abstract bool ActionHandler(string tag, Vector3 position, bool priority);

    public void OnEnable()
    {
        // Add the selectable to a list of selectable entities.
        SelectionController.selectables.Add(this);
        this._outline = Owner.GetComponent<Outline>();
        _outline.enabled = true;

    }

    public void OnDisable()
    {
        // Remove the selectable of the list of selectable entities.
        SelectionController.selectables.Remove(this);
        _outline.enabled = false;
    }
}
