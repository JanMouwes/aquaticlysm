using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSelection : MonoBehaviour
{
    [Tooltip("The canvas of the selection box")]
    public Canvas canvas;

    [Tooltip("The image that will represent the selection box.")]
    public RectTransform selectionBox;

    // The start position of the mouseclick inside of the canvas
    private Vector2 startScreenPosition;

    // The position where the raycast hits
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
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
        if (Input.GetButtonDown("LeftMouseButton") && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 300))
        {
            startScreenPosition = Input.mousePosition;
            Debug.Log(Input.mousePosition);
            Debug.Log(hit.point);
            Debug.Log(hit);
        }

        // Return true when the left mouse button is held down and the raycast (range 300) hits the floor
        if (Input.GetButton("LeftMouseButton") && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000))
            UpdateSelectionBox(Input.mousePosition);

        // Return true when the left mouse button is released
        if (Input.GetButtonUp("LeftMouseButton"))
            selectionBox.gameObject.SetActive(false);
    }

    /// <summary>
    /// Update the shape of the selection box inside of the canvas
    /// </summary>
    /// <param name="curMousePosition">The current mouse position</param>
    private void UpdateSelectionBox(Vector2 mousePosition)
    {
        // Make the selection box active if it's not
        if (!selectionBox.gameObject.activeInHierarchy)
            selectionBox.gameObject.SetActive(true);

        // Create bounds (AABB or just a rectangle if your not familiar with it)
        Bounds boundingBox = new Bounds();

        // Set the center of the bounding box between the start position and the mouse position
        boundingBox.center = Vector2.Lerp(startScreenPosition, mousePosition, 0.5f);

        // Set the size of the bounding box
        boundingBox.size = new Vector2(Mathf.Abs(startScreenPosition.x - mousePosition.x), Mathf.Abs(startScreenPosition.y - mousePosition.y));

        // Set the position of the selection box
        selectionBox.position = boundingBox.center;

        // Set the size of the selection box
        selectionBox.sizeDelta = canvas.transform.InverseTransformVector(boundingBox.size);
    }
}
