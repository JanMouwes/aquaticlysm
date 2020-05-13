using UnityEngine;
using UnityEngine.Serialization;

public class Selectable : MonoBehaviour
{
    // Material to emphasize whether a gameobject is selected or not
    public Material selectedMaterial;
    public Material normalMaterial;

    public bool isSelected;
    
    public void SetSelected(bool selection)
    {
        // Set if it's selected or not
        this.isSelected = selection;

        // If gameobject is currently selected, highlight that by switching the material.
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
    /// Switch the given material to the gameobject.
    /// </summary>
    /// <param name="material">Material to switch to</param>
    public void SwitchToMaterial(Material material)
    {
        // Get the gameobjects material through the mesh renderer to switch the material.
        MeshRenderer gameObjectRenderer = this.gameObject.GetComponent<MeshRenderer>();

        gameObjectRenderer.material = material;
    }
}
