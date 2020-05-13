using UnityEngine;

public class Selectable : MonoBehaviour
{
    private Material _selectedMaterial;
    private Material _normalMaterial;
    private GameObject _owner;

    public bool IsSelected;

    public void SetSelected(bool selection, GameObject owner, Material selectedMaterial, Material normalMaterial)
    {
        this.IsSelected = selection;
        this._owner = owner;
        this._selectedMaterial = selectedMaterial;
        this._normalMaterial = normalMaterial;
    }

    public void SetSelected(bool selection)
    {
        // Set if it's selected or not
        IsSelected = selection;
    }

    void OnEnable()
    {
        // Add the selectable to a list of selectable entities
        SelectionController.selectables.Add(this);
        SwitchToMaterial(_selectedMaterial);
    }


    void OnDisable()
    {
        // Remove the selectable of the list of selectable entities
        SelectionController.selectables.Remove(this);
        SwitchToMaterial(_normalMaterial);
    }

    /// <summary>
    /// Emphasize with an orange highlighter, if currently raycasted gameObject is selected.
    /// </summary>
    /// <param name="material">Material to switch to</param>
    public void SwitchToMaterial(Material material)
    {
        MeshRenderer gameObjectRenderer = this._owner.GetComponent<MeshRenderer>();

        gameObjectRenderer.material = material;
    }
}
