using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Selectable : MonoBehaviour
{
    public bool Selected { get; set; }
    
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
        this._outline = this.GetComponent<Outline>();
        OnDeselected();
    }

    public void OnDisable()
    {
        // Remove the selectable of the list of selectable entities.
        SelectionController.selectables.Remove(this);
    }

    public void OnSelected()
    {
        _outline.enabled = true;

    }

    public void OnDeselected()
    {
        _outline.enabled = false;
    }
}
