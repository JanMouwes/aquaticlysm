using UnityEngine;

public class Selectable : MonoBehaviour
{
    public bool isSelected;

    public void SetSelected(bool selection)
    {
        // Set if it's selected or not
        isSelected = selection;
    }

    void OnEnable()
    {
        // Add the selectable to a list of selectable entities
        SelectionController.selectables.Add(this);
    }

    void OnDisable()
    {
        // Remove the selectable of the list of selectable entities
        SelectionController.selectables.Remove(this);
    }
}
