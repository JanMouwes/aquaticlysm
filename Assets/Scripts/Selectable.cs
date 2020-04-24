using UnityEngine;

public class Selectable : MonoBehaviour
{
    public bool isSelected;

    public void SetSelected(bool selection)
    {
        Debug.Log(selection);
        isSelected = selection;

        // Here we would set other stuff like highlighting the object
    }
}
