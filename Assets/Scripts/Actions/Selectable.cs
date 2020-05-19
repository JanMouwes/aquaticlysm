using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Selectable : MonoBehaviour
{
    public bool Selected
    {
        get => _selected;
        set
        {
            _selected = value;
            if (_selected)
                OnSelected();
            else
                OnDeselected();
        }
    }

    private Outline _outline;
    private bool _selected;

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

    private void OnSelected()
    {
        _outline.enabled = true;

    }

    private void OnDeselected()
    {
        _outline.enabled = false;
    }
}
