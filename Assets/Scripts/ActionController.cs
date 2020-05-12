using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActionController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("RightMouseButton"))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit hit;

                // Use a raycast (range 100) to register the position of the mouse click and set the agents new destination
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                { 
                                    Debug.Log(hit.collider.gameObject.name);

                    switch (hit.collider.gameObject.name)
                    {
                        case "Walkway":

                            break;
                        default:
                            break;
                    }


                }
            }
        }
    }
}
