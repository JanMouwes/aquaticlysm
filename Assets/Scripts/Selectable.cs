using UnityEngine;

public class Selectable : MonoBehaviour
{
    private bool isSelected = false;

    public void SetSelected(bool selection)
    {
        // Set if it's selected or not
        isSelected = selection;
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
