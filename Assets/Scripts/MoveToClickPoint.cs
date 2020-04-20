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
        // Use a raycast to register the position of the mouse click and set the agents new destination
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;

            // Range for the raycast is set to 100
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
                agent.destination = hit.point;
        }
    }
}
