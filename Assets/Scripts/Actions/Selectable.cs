using System;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    private Outline _outline;
    private bool _selected;

    public bool Selected
    {
        get => this._selected;
        set
        {
            this._selected = value;

            if (value) { OnSelected(); }
            else { OnDeselected(); }
        }
    }

    public void OnEnable()
    {
        // Add the selectable to a list of selectable entities.
        SelectionController.AddSelectable(this);
        this._outline = GetComponent<Outline>();
        OnDeselected();
    }

    public void OnDisable()
    {
        // Remove the selectable of the list of selectable entities.
        SelectionController.RemoveSelectable(this);
    }
    
    private void OnDestroy()
    {
        // Remove the selectable of the list of selectable entities.
        SelectionController.RemoveSelectable(this);
    }

    private void OnSelected()
    {
        this._outline.enabled = true;
    }

    private void OnDeselected()
    {
        this._outline.enabled = false;
    }
}