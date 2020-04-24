using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelection : MonoBehaviour
{
    // All selected units
    public static List<Selectable> SelectedEntities = new List<Selectable>();

    [Tooltip("The canvas of the selection box")]
    public Canvas canvas;

    [Tooltip("The image that will represent the selection box.")]
    public RectTransform selectionBox;

    // The start position of the mouseclick inside of the canvas
    private Vector2 startScreenPosition;
    private Vector2 startWorldSpace;
    private Vector2 endWorldSpace;

    // The positions where the raycast hits
    private RaycastHit raycastHit;

    // Start is called before the first frame update
    void Start()
    {
        startWorldSpace = new Vector2();
        endWorldSpace = new Vector2();

        // Initialize the selection box
        if (selectionBox != null)
        {
            // We need to reset anchors and pivot to ensure proper positioning
            selectionBox.pivot = Vector2.one * .5f;
            selectionBox.anchorMin = Vector2.one * .5f;
            selectionBox.anchorMax = Vector2.one * .5f;
            selectionBox.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Return true when the left mouse button is pressed and the raycast (range 300) hits the floor
        if (Input.GetButtonDown("LeftMouseButton") && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, 300))
        {
            // Clear all selected items
            //ClearSelected();

            // Select a single unit
            Selectable s = raycastHit.collider.GetComponentInParent<Selectable>();
            if (s != null)
            {
                s.SetSelected(true);
                SelectedEntities.Add(s);
            }

            // Register the mouse coordinates within the canvas
            startScreenPosition = Input.mousePosition;

            // Set the pos of the raycasthit
            startWorldSpace.x = raycastHit.point.x;
            startWorldSpace.y = raycastHit.point.z;
        }

        // Return true when the left mouse button is held down and the raycast (range 300) hits the floor
        if (Input.GetButton("LeftMouseButton") && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out raycastHit, 1000))
        {
            // Update the seleciton box with the new mousecoordinates
            UpdateSelectionBox(Input.mousePosition);

            // Set the pos of the raycasthit
            endWorldSpace.x = raycastHit.point.x;
            endWorldSpace.y = raycastHit.point.z;
        }

        // Return true when the left mouse button is released
        if (Input.GetButtonUp("LeftMouseButton"))
        {
            // Close the selection box
            selectionBox.gameObject.SetActive(false);

            // Create a bounding box with the positions in the worldspace
            Bounds boundingBox = CreateBoudingBox(startWorldSpace, endWorldSpace);
            
            // Foreach through all selectables and check if it's inside of the bounding box
            foreach (Selectable selectable in SelectedEntities)
                if (boundingBox.Contains(new Vector2(selectable.transform.position.x, selectable.transform.position.z)))
                    selectable.SetSelected(true);
        }
    }

    /// <summary>
    /// Clear all selected entities
    /// </summary>
    private void ClearSelected() 
    {
        SelectedEntities.ForEach(s => s.SetSelected(false));
        SelectedEntities.Clear();
    }

    /// <summary>
    /// Update the shape of the selection box inside of the canvas
    /// </summary>
    /// <param name="curMousePosition">The current mouse position</param>
    private void UpdateSelectionBox(Vector2 mousePosition)
    {
        // Open the selection box if its closed
        if (!selectionBox.gameObject.activeInHierarchy)
            selectionBox.gameObject.SetActive(true);

        Bounds boundingBox = CreateBoudingBox(startScreenPosition, mousePosition);

        // Set the position of the selection box
        selectionBox.position = boundingBox.center;

        // Set the size of the selection box
        selectionBox.sizeDelta = canvas.transform.InverseTransformVector(boundingBox.size);
    }

    // Create bounds (AABB or just a rectangle if your not familiar with it)
    private Bounds CreateBoudingBox(Vector2 startPos, Vector2 endPos) 
    {
        Bounds boundingBox = new Bounds();

        // Set the center of the bounding box between the start position and the mouse position
        boundingBox.center = Vector2.Lerp(startPos, endPos, 0.5f);

        // Set the size of the bounding box
        boundingBox.size = new Vector2(Mathf.Abs(startPos.x - endPos.x), Mathf.Abs(startPos.y - endPos.y));

        return boundingBox;
    }
}
