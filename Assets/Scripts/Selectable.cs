using UnityEngine;

public class Selectable : MonoBehaviour
{
    private bool isSelected = false;

    public void SetSelected(bool selection)
    {
        Debug.Log(selection);
        isSelected = selection;
    }
}
