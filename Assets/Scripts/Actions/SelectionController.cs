using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Util;

public class SelectionController : MonoBehaviour
{
    // All selectable entities.
    public static List<Selectable> Selectables = new List<Selectable>();
    
    // All selected entities.
    public static List<Selectable> SelectedEntities = new List<Selectable>();

    // public static CharacterSystem characterSystem;
    
    [Tooltip("The canvas of the selection box")]
    public Canvas canvas;

    [Tooltip("The image that will represent the selection box.")]
    public RectTransform selectionBox;

    // The start position of the mouseclick inside of the canvas.
    private Vector2 _startScreenPosition;
    private Vector2 _startWorldSpace;
    private Vector2 _endWorldSpace;

    private void Awake()
    {
        GlobalStateMachine.instance.StateChanged += ToggleEnable;
    }

    private void OnDestroy()
    {
        GlobalStateMachine.instance.StateChanged -= ToggleEnable;
    }

    private void ToggleEnable(IState state) => this.enabled = state is PlayState;

    // Start is called before the first frame update.
    private void Start()
    {
        this._startWorldSpace = new Vector2();
        this._endWorldSpace = new Vector2();

        // Initialize the selection box.
        if (this.selectionBox != null)
        {
            // We need to reset anchors and pivot to ensure proper positioning.
            this.selectionBox.pivot = Vector2.one * .5f;
            this.selectionBox.anchorMin = Vector2.one * .5f;
            this.selectionBox.anchorMax = Vector2.one * .5f;
            this.selectionBox.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame.
    private void Update()
    {
        RaycastHit raycastHit;
        
        // Return true when the left mouse button is pressed and the raycast (range 300) hits the floor.
        if (!EventSystem.current.IsPointerOverGameObject() &&
            Input.GetButtonDown("LeftMouseButton") && 
            MouseUtil.TryRaycastAtMousePosition(300, out raycastHit))
        {
            SelectSingleUnit(raycastHit);
        }

        // Return true when the left mouse button is held down and the raycast (range 1000) hits the floor.
        if (!EventSystem.current.IsPointerOverGameObject() &&
            Input.GetButton("LeftMouseButton") && 
            MouseUtil.TryRaycastAtMousePosition(1000, out raycastHit))
        {
            // Update the selection box with the new mousecoordinates.
            UpdateSelectionBox(Input.mousePosition);

            // Set the pos of the raycasthit.
            this._endWorldSpace.x = raycastHit.point.x;
            this._endWorldSpace.y = raycastHit.point.z;
        }

        // Return true when the left mouse button is released.
        if (Input.GetButtonUp("LeftMouseButton"))
        {
            SelectMultipleUnits();
        }
    }
    
    
    public static void AddSelectable(Selectable selectable)
    {
        Selectables.Add(selectable);
        // selectable.Selected = false;
    }
    
    public static void RemoveSelectable(Selectable selectable)
    {
        Selectables.Remove(selectable);
    }
    
    private void SelectSingleUnit(RaycastHit raycastHit)
    {
        // Clear all selected items.
        ClearSelected();

        // Select a single unit.
        Selectable s = raycastHit.collider.GetComponentInParent<Selectable>();
        if (s != null)
        {
            s.Selected = true;
        }

        // Register the mouse coordinates within the canvas.
        this._startScreenPosition = Input.mousePosition;

        // Set the pos of the raycasthit.
        this._startWorldSpace.x = raycastHit.point.x;
        this._startWorldSpace.y = raycastHit.point.z;
    }

    private void SelectMultipleUnits()
    {
        // Close the selection box.
        this.selectionBox.gameObject.SetActive(false);

        // Create a bounding box with the positions in the worldspace.
        Bounds boundingBox = CreateBoundingBox(this._startWorldSpace, this._endWorldSpace);

        // Foreach through all selectables and check if it's inside of the bounding box and a unit or boat.
        foreach (Selectable selectable in Selectables)
        {
            if (selectable.CompareTag("Character"))
            {
                if (boundingBox.Contains(new Vector2(selectable.gameObject.transform.position.x, selectable.gameObject.transform.position.z)))
                {
                    selectable.Selected = true;
                }
            }
        }
    }

    /// <summary>
    /// Clear all selected entities.
    /// </summary>
    private void ClearSelected() 
    {
        SelectedEntities.ForEach(s => s.Selected = false);

        SelectedEntities.Clear();
    }

    /// <summary>
    /// Update the shape of the selection box inside of the canvas.
    /// </summary>
    /// <param name="mousePosition">The current mouse position.</param>
    private void UpdateSelectionBox(Vector2 mousePosition)
    {
        // Open the selection box if its closed.
        if (!this.selectionBox.gameObject.activeInHierarchy) this.selectionBox.gameObject.SetActive(true);

        // Create a bounding box with the positions in the localspace.
        Bounds boundingBox = CreateBoundingBox(this._startScreenPosition, mousePosition);

        // Set the position of the selection box.
        this.selectionBox.position = boundingBox.center;

        // Set the size of the selection box.
        this.selectionBox.sizeDelta = this.canvas.transform.InverseTransformVector(boundingBox.size);
    }

    // Create bounds (AABB, a rectangle if your not familiar with it).
    private Bounds CreateBoundingBox(Vector2 startPos, Vector2 endPos) 
    {
        Bounds boundingBox = new Bounds();

        // Set the center of the bounding box between the start position and the mouse position.
        boundingBox.center = Vector2.Lerp(startPos, endPos, 0.5f);

        // Set the size of the bounding box.
        boundingBox.size = new Vector2(Mathf.Abs(startPos.x - endPos.x), Mathf.Abs(startPos.y - endPos.y));

        return boundingBox;
    }
}
