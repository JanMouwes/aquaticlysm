using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelection : MonoBehaviour
{

    public RectTransform selectionBox;
    private Vector2 startPos;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        // Use a raycast to register the position of the mouse click and set the agents new destination
        // Range for the raycast is set to 100
        if (Input.GetButtonDown("LeftMouseButton") && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000))
            startPos = hit.point;

        // Mouse held down
        if (Input.GetButton("LeftMouseButton") && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000))
            UpdateSelectionBox(hit.point);

        if (Input.GetButtonUp("LeftMouseButton"))
            ReleaseSelectionBox();
    }

    // called when we are creating a selection box
    void UpdateSelectionBox(Vector2 curMousePos)
    {
        if (!selectionBox.gameObject.activeInHierarchy)
            selectionBox.gameObject.SetActive(true);

        float width = curMousePos.x - startPos.x;
        float height = curMousePos.y - startPos.y;

        selectionBox.sizeDelta = new Vector2(Mathf.Abs(width), Mathf.Abs(height));
        selectionBox.anchoredPosition = startPos + new Vector2(width / 2, height / 2);
    }

    // called when we release the selection box
    void ReleaseSelectionBox()
    {
        selectionBox.gameObject.SetActive(false);

        Vector2 min = selectionBox.anchoredPosition - (selectionBox.sizeDelta / 2);
        Vector2 max = selectionBox.anchoredPosition + (selectionBox.sizeDelta / 2);


    }
}
