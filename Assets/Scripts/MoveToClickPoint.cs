using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        // Return true when the right mouse button is pressed
        if (Input.GetButtonDown("RightMouseButton"))
        {
            RaycastHit hit;

            // Use a raycast (range 100) to register the position of the mouse click and set the agents new destination
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                agent.destination = hit.point;
        }
    }
}
