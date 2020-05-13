using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionController : MonoBehaviour
{
    private RaycastHit _hit;
    private bool _priority;

    // Update is called once per frame.
    void Update()
    {
        if (Input.GetButtonDown("RightMouseButton"))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                // Set the priority with the shift click.
                _priority = Input.GetKey(KeyCode.LeftShift) 
                            || Input.GetKey(KeyCode.RightShift);
                
                // Use a raycast to register a game object and send the data to the selected entities.
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit, 100))
                    SelectionController.selectedEntities.ForEach(s => s.ActionHandler(_hit.collider.gameObject.tag, _hit.point, _priority));
            }
        }
    }
}
