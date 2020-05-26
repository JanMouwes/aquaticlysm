using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : IGoal
{
    private GameObject _owner;
    public GoalStatus Status { get; private set; }
    public string Name { get; private set; }
    public float NearRange  { get; set; }
    
    // Target position.
    private readonly Vector3 _target;

    public MoveTo(GameObject owner, Vector3 position)
    {
        _owner = owner;
        Name = "MoveTo";
        _target = position;
        NearRange = 2f;
    }
    
    public void Activate()
    {
        // Set destination.
        _owner.GetComponent<NavMeshAgent>().destination = _target;
        Status = GoalStatus.Active;
    }

    public GoalStatus Process()
    {
        if (Status == GoalStatus.Inactive)
            Activate();

        // Checks if the Owner arrived at the target.
        if (Vector3.Distance(_owner.transform.position, _target) < NearRange)
            Terminate();
        
        return Status;
    }

    public void Terminate()
    {
        Status = GoalStatus.Completed;
    }
}
