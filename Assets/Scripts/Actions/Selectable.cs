using UnityEngine;

public abstract class Selectable : MonoBehaviour
{
    public bool Selected { get; set; }
    public bool isSelected;

    public void SetSelected(bool selection)
    {
        // Set if it's selected or not
        isSelected = selection;
    }

    public abstract void DoAction(string tag, Vector3 position, bool priority);

    void OnEnable()
    {
        // Add the selectable to a list of selectable entities
        //SelectionController.selectables.Add(this);
    }

    void OnDisable()
    {
        // Remove the selectable of the list of selectable entities
        //SelectionController.selectables.Remove(this);
    }
}
