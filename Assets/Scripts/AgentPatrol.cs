using UnityEngine;
using UnityEngine.AI;

public class AgentPatrol : MonoBehaviour
{
    private NavMeshAgent _agent;
    private int          _destPoint;
    private Vector3      _target;
    public  Transform[]  points;

    // Start is called before the first frame update
    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        GotoNextPoint();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!_agent.pathPending && _agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }

    private void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        _agent.destination = points[_destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        _destPoint = (_destPoint + 1) % points.Length;
    }
}