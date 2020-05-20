using UnityEngine;

public class Selectable : MonoBehaviour
{
    private Outline _outline;
    private bool _selected;

    public bool Selected
    {
        get => _selected;
        set
        {
            if (value) 
                OnSelected();
            else 
                OnDeselected();
        }
    }


    public void OnEnable()
    {
        // Add the selectable to a list of selectable entities.
        SelectionController.selectables.Add(this);
        this._outline = GetComponent<Outline>();
        OnDeselected();
    }

    public void OnDisable()
    {
        // Remove the selectable of the list of selectable entities.
        SelectionController.selectables.Remove(this);
    }

    private void OnSelected()
    {
        _selected = true;
        _outline.enabled = true;
    }

    private void OnDeselected()
    {
        _selected = false;
        _outline.enabled = false;
    }
}