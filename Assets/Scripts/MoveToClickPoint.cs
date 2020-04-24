using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MoveToClickPoint : MonoBehaviour
{
    NavMeshAgent agent;
    void Start()
    {
        // Get the agent
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Use a raycast to register the position of the mouse click and set the agents new destination
        // Input.GetAxis... "nameofthemouseclick"
        if (Input.GetButtonDown("LeftMouseButton"))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                RaycastHit hit;
                // Range for the raycast is set to 100
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                    agent.destination = hit.point;
            }
            
        }
    }
}
