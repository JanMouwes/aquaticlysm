using UnityEngine;
using UnityEngine.Serialization;

public class Selectable : MonoBehaviour
{
    public Material selectedMaterial;
    public Material normalMaterial;

    public bool isSelected;
    
    public void SetSelected(bool selection)
    {
        // Set if it's selected or not
        this.isSelected = selection;

        Material newMaterial = selection ? this.selectedMaterial : this.normalMaterial;

        SwitchToMaterial(newMaterial);
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

    /// <summary>
    /// Emphasize with an orange highlighter, if currently raycasted gameObject is selected.
    /// </summary>
    /// <param name="material">Material to switch to</param>
    public void SwitchToMaterial(Material material)
    {
        MeshRenderer gameObjectRenderer = this.gameObject.GetComponent<MeshRenderer>();

        gameObjectRenderer.material = material;
    }
}
