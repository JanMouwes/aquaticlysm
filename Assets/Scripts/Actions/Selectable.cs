using UnityEngine;

public abstract class Selectable : MonoBehaviour
{
    private Outline _outline;
    private bool _selected;

    public bool Selected
    {
        get => this._selected;
        set
        {
            this._selected = value;

            if (this._selected) { OnSelected(); }
            else { OnDeselected(); }
        }
    }

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
        this._outline.enabled = true;
    }

    private void OnDeselected()
    {
        this._outline.enabled = false;
    }
}