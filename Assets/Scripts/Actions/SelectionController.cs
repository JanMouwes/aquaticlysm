using System.Collections.Generic;
using UnityEngine;
using Util;

public class SelectionController : MonoBehaviour
{
    // All selectable entities.
    public static List<Selectable> selectables = new List<Selectable>();
    
    // All selected entities.
    public static List<Selectable> selectedEntities = new List<Selectable>();

    [Tooltip("The canvas of the selection box")]
    public Canvas canvas;

    [Tooltip("The image that will represent the selection box.")]
    public RectTransform selectionBox;

    // The start position of the mouseclick inside of the canvas.
    private Vector2 startScreenPosition;
    private Vector2 startWorldSpace;
    private Vector2 endWorldSpace;
    
    // Start is called before the first frame update.
    private void Start()
    {
        startWorldSpace = new Vector2();
        endWorldSpace = new Vector2();

        // Initialize the selection box.
        if (selectionBox != null)
        {
            // We need to reset anchors and pivot to ensure proper positioning.
            selectionBox.pivot = Vector2.one * .5f;
            selectionBox.anchorMin = Vector2.one * .5f;
            selectionBox.anchorMax = Vector2.one * .5f;
            selectionBox.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame.
    private void Update()
    {
        RaycastHit raycastHit;
        
        // Return true when the left mouse button is pressed and the raycast (range 300) hits the floor.
        if (Input.GetButtonDown("LeftMouseButton") && 
            MouseUtil.TryRaycastAtMousePosition(300, out raycastHit))
        {
            // Clear all selected items.
            ClearSelected();

            // Select a single unit.
            Selectable s = raycastHit.collider.GetComponentInParent<Selectable>();
            if (s != null)
            {
                s.Selected = true;
                selectedEntities.Add(s);
            }

            // Register the mouse coordinates within the canvas.
            startScreenPosition = Input.mousePosition;

            // Set the pos of the raycasthit.
            startWorldSpace.x = raycastHit.point.x;
            startWorldSpace.y = raycastHit.point.z;
        }

        // Return true when the left mouse button is held down and the raycast (range 1000) hits the floor.
        if (Input.GetButton("LeftMouseButton") && 
            MouseUtil.TryRaycastAtMousePosition(1000, out raycastHit))
        {
            // Update the selection box with the new mousecoordinates.
            UpdateSelectionBox(Input.mousePosition);

            // Set the pos of the raycasthit.
            endWorldSpace.x = raycastHit.point.x;
            endWorldSpace.y = raycastHit.point.z;
        }

        // Return true when the left mouse button is released.
        if (Input.GetButtonUp("LeftMouseButton"))
        {
            // Close the selection box.
            selectionBox.gameObject.SetActive(false);

            // Create a bounding box with the positions in the worldspace.
            Bounds boundingBox = CreateBoundingBox(startWorldSpace, endWorldSpace);

            // Foreach through all selectables and check if it's inside of the bounding box and a unit or boat.
            foreach (Selectable selectable in selectables)
            {
                if (selectable is Character gameObject)
                {
                    if(boundingBox.Contains(new Vector2(gameObject.transform.position.x, gameObject.transform.position.z)))
                    {
                        selectable.Selected = true;
                        
                        selectedEntities.Add(selectable);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Clear all selected entities.
    /// </summary>
    private void ClearSelected() 
    {
        selectedEntities.ForEach(s => s.Selected = false);

        selectedEntities.Clear();
    }

    /// <summary>
    /// Update the shape of the selection box inside of the canvas.
    /// </summary>
    /// <param name="mousePosition">The current mouse position.</param>
    private void UpdateSelectionBox(Vector2 mousePosition)
    {
        // Open the selection box if its closed.
        if (!selectionBox.gameObject.activeInHierarchy)
            selectionBox.gameObject.SetActive(true);

        // Create a bounding box with the positions in the localspace.
        Bounds boundingBox = CreateBoundingBox(startScreenPosition, mousePosition);

        // Set the position of the selection box.
        selectionBox.position = boundingBox.center;

        // Set the size of the selection box.
        selectionBox.sizeDelta = canvas.transform.InverseTransformVector(boundingBox.size);
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
