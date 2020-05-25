using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveTo : IGoal
{
    public Character Owner { get; private set; }
    public GoalStatus Status { get; private set; }
    public string Name { get; private set; }

    // Target position.
    private Vector3 _target;

    public MoveTo(Character owner, Vector3 position)
    {
        Owner = owner;
        Name = "MoveTo";
        _target = position;
    }
    
    public void Activate()
    {
        // Set destination.
        Owner.agent.destination = _target;
        Status = GoalStatus.Active;
    }

    public GoalStatus Process()
    {
        if (Status == GoalStatus.Inactive)
            Activate();

        // Checks if the Owner arrived at the target.
        if (Vector3.Distance(Owner.transform.position, _target) <= 1f)
           Terminate();

        // Check if the agent can't move any further.
        // TODO Make sure composition fails too
        if (Owner.agent.pathPending)
            Status = GoalStatus.Failed;

        return Status;
    }

    public void Terminate()
    {
        Status = GoalStatus.Completed;
    }
}
