using UnityEngine;

public class Selectable : MonoBehaviour
{
    public bool isSelected;

    public void SetSelected(bool selection)
    {
        // Set if it's selected or not
        isSelected = selection;

        // Here we would set other stuff like highlighting the object
    }

    void OnEnable()
    {
        // Add the selectable to a list of selecectable entities
        SelectionManager.selectables.Add(this);
    }

    void OnDisable()
    {
        // Remove the selectable of the list of selecectable entities
        SelectionManager.selectables.Remove(this);
    }
}
